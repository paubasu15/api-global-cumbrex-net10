using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace CumbrexSaaS.Infrastructure.Security;

/// <summary>
/// Service for managing authentication tokens via secure HTTP-only cookies.
/// </summary>
public sealed class CookieTokenService
{
    private const string AccessTokenCookie = "cumbrex_at";
    private const string RefreshTokenCookie = "cumbrex_rt";

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;

    /// <summary>Initializes a new instance of <see cref="CookieTokenService"/>.</summary>
    public CookieTokenService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
    {
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
    }

    /// <summary>Sets the access token as an HTTP-only secure cookie.</summary>
    public void SetAccessToken(string token, DateTimeOffset expires)
    {
        _httpContextAccessor.HttpContext?.Response.Cookies.Append(
            AccessTokenCookie,
            token,
            BuildCookieOptions(expires));
    }

    /// <summary>Sets the refresh token as an HTTP-only secure cookie.</summary>
    public void SetRefreshToken(string token, DateTimeOffset expires)
    {
        _httpContextAccessor.HttpContext?.Response.Cookies.Append(
            RefreshTokenCookie,
            token,
            BuildCookieOptions(expires));
    }

    /// <summary>Clears both authentication cookies.</summary>
    public void ClearTokens()
    {
        var context = _httpContextAccessor.HttpContext;
        context?.Response.Cookies.Delete(AccessTokenCookie);
        context?.Response.Cookies.Delete(RefreshTokenCookie);
    }

    /// <summary>Reads the access token from the cookie.</summary>
    public string? GetAccessToken() =>
        _httpContextAccessor.HttpContext?.Request.Cookies[AccessTokenCookie];

    /// <summary>Reads the refresh token from the cookie.</summary>
    public string? GetRefreshToken() =>
        _httpContextAccessor.HttpContext?.Request.Cookies[RefreshTokenCookie];

    private CookieOptions BuildCookieOptions(DateTimeOffset expires) => new()
    {
        HttpOnly = true,
        Secure = true,
        SameSite = SameSiteMode.Strict,
        Expires = expires,
        Path = "/"
    };
}
