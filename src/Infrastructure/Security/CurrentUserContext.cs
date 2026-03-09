using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace CumbrexSaaS.Infrastructure.Security;

/// <summary>
/// HTTP context-based implementation of <see cref="ICurrentUserContext"/>.
/// Reads claims from the current principal set by JWT authentication.
/// </summary>
public sealed class CurrentUserContext : ICurrentUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>Initializes a new instance of <see cref="CurrentUserContext"/>.</summary>
    public CurrentUserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ClaimsPrincipal? Principal => _httpContextAccessor.HttpContext?.User;

    /// <inheritdoc />
    public bool IsAuthenticated => Principal?.Identity?.IsAuthenticated ?? false;

    /// <inheritdoc />
    public Guid UserId
    {
        get
        {
            var claim = Principal?.FindFirstValue(ClaimTypes.NameIdentifier);
            return Guid.TryParse(claim, out var id) ? id : Guid.Empty;
        }
    }

    /// <inheritdoc />
    public string? Email => Principal?.FindFirstValue(ClaimTypes.Email);
}
