namespace CumbrexSaaS.Infrastructure.Security;

/// <summary>Provides access to the currently authenticated user's context.</summary>
public interface ICurrentUserContext
{
    /// <summary>Gets the current user's identifier. Returns <see cref="Guid.Empty"/> if not authenticated.</summary>
    Guid UserId { get; }

    /// <summary>Gets the current user's email address.</summary>
    string? Email { get; }

    /// <summary>Gets a value indicating whether the current request is authenticated.</summary>
    bool IsAuthenticated { get; }
}
