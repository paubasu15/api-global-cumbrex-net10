using CumbrexSaaS.Domain.Entities.Identity;
using CumbrexSaaS.Domain.Enums;
using CumbrexSaaS.Infrastructure.Persistence;
using CumbrexSaaS.Infrastructure.Security;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace CumbrexSaaS.Features.Auth.RequestVerificationCode;

/// <summary>
/// Endpoint: POST /api/auth/request-code
/// Initiates the passwordless login flow by sending a verification code to the user's email.
/// </summary>
public sealed class RequestVerificationCodeEndpoint
    : Endpoint<RequestVerificationCodeRequest, RequestVerificationCodeResponse>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly VerificationCodeService _verificationCodeService;

    /// <summary>Initializes a new instance of <see cref="RequestVerificationCodeEndpoint"/>.</summary>
    public RequestVerificationCodeEndpoint(
        ApplicationDbContext dbContext,
        VerificationCodeService verificationCodeService)
    {
        _dbContext = dbContext;
        _verificationCodeService = verificationCodeService;
    }

    /// <inheritdoc />
    public override void Configure()
    {
        Post("/api/auth/request-code");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Request a verification code";
            s.Description = "Initiates passwordless login by sending a one-time code to the provided email address.";
            s.Response<RequestVerificationCodeResponse>(200, "Code sent successfully");
            s.Response(400, "Invalid request");
            s.Response(429, "Too many requests");
        });
    }

    /// <inheritdoc />
    public override async Task HandleAsync(
        RequestVerificationCodeRequest req,
        CancellationToken ct)
    {
        // Find or create user
        var user = await _dbContext.Users
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(u => u.Email == req.Email.ToLowerInvariant(), ct);

        if (user is null)
        {
            user = new User
            {
                Email = req.Email.ToLowerInvariant(),
                FirstName = string.Empty,
                LastName = string.Empty,
                IsActive = true
            };
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync(ct);
        }

        if (!user.IsActive)
        {
            await HttpContext.Response.SendOkAsync(new RequestVerificationCodeResponse
            {
                Success = false,
                Message = "Account is inactive. Please contact support.",
                ExpiresInSeconds = 0
            }, cancellation: ct);
            return;
        }

        // Generate and persist the code (actual sending via notification service in production)
        _ = await _verificationCodeService.GenerateAsync(
            user.Id,
            user.Email,
            VerificationChannel.Email,
            ct);

        await HttpContext.Response.SendOkAsync(new RequestVerificationCodeResponse
        {
            Success = true,
            Message = "A verification code has been sent to your email address.",
            ExpiresInSeconds = 600
        }, cancellation: ct);
    }
}
