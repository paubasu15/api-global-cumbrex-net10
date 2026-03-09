using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CumbrexSaaS.Domain.Enums;
using CumbrexSaaS.Infrastructure.Persistence;
using CumbrexSaaS.Infrastructure.Security;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CumbrexSaaS.Features.Auth.VerifyCode;

/// <summary>
/// Endpoint: POST /api/auth/verify-code
/// Validates the submitted verification code and issues a JWT access token on success.
/// </summary>
public sealed class VerifyCodeEndpoint : Endpoint<VerifyCodeRequest, VerifyCodeResponse>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly VerificationCodeService _verificationCodeService;
    private readonly CookieTokenService _cookieTokenService;
    private readonly IConfiguration _configuration;

    /// <summary>Initializes a new instance of <see cref="VerifyCodeEndpoint"/>.</summary>
    public VerifyCodeEndpoint(
        ApplicationDbContext dbContext,
        VerificationCodeService verificationCodeService,
        CookieTokenService cookieTokenService,
        IConfiguration configuration)
    {
        _dbContext = dbContext;
        _verificationCodeService = verificationCodeService;
        _cookieTokenService = cookieTokenService;
        _configuration = configuration;
    }

    /// <inheritdoc />
    public override void Configure()
    {
        Post("/api/auth/verify-code");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Verify a one-time code";
            s.Description = "Validates the submitted code and returns a JWT access token.";
            s.Response<VerifyCodeResponse>(200, "Authentication successful");
            s.Response(400, "Invalid request or code");
            s.Response(401, "Authentication failed");
        });
    }

    /// <inheritdoc />
    public override async Task HandleAsync(VerifyCodeRequest req, CancellationToken ct)
    {
        var user = await _dbContext.Users
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(u => u.Email == req.Email.ToLowerInvariant(), ct);

        if (user is null || !user.IsActive)
        {
            await HttpContext.Response.SendUnauthorizedAsync(cancellation: ct);
            return;
        }

        var isValid = await _verificationCodeService.ValidateAsync(
            user.Id,
            req.Code,
            VerificationChannel.Email,
            ct);

        if (!isValid)
        {
            await HttpContext.Response.SendAsync(new VerifyCodeResponse
            {
                Success = false,
                ErrorMessage = "Invalid or expired verification code."
            }, statusCode: 401, cancellation: ct);
            return;
        }

        if (user.EmailVerifiedAt is null)
        {
            user.EmailVerifiedAt = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync(ct);
        }

        var token = GenerateJwtToken(user.Id, user.Email);
        var expiry = DateTimeOffset.UtcNow.AddHours(1);
        _cookieTokenService.SetAccessToken(token, expiry);

        await HttpContext.Response.SendOkAsync(new VerifyCodeResponse
        {
            Success = true,
            AccessToken = token,
            UserId = user.Id,
            Email = user.Email
        }, cancellation: ct);
    }

    private string GenerateJwtToken(Guid userId, string email)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secret = jwtSettings["Secret"] ?? throw new InvalidOperationException("JWT secret not configured.");
        var issuer = jwtSettings["Issuer"] ?? "CumbrexSaaS";
        var audience = jwtSettings["Audience"] ?? "CumbrexSaaS";

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
