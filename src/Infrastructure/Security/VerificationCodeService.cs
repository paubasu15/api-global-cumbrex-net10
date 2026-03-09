using System.Security.Cryptography;
using System.Text;
using CumbrexSaaS.Domain.Entities.Security;
using CumbrexSaaS.Domain.Enums;
using CumbrexSaaS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CumbrexSaaS.Infrastructure.Security;

/// <summary>
/// Service for generating, persisting and validating one-time verification codes.
/// </summary>
public sealed class VerificationCodeService
{
    private const int CodeLength = 6;
    private const int ExpiryMinutes = 10;
    private const int MaxAttempts = 5;

    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<VerificationCodeService> _logger;

    /// <summary>Initializes a new instance of <see cref="VerificationCodeService"/>.</summary>
    public VerificationCodeService(ApplicationDbContext dbContext, ILogger<VerificationCodeService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    /// <summary>
    /// Generates a new verification code for a user and persists it.
    /// Any previous unused codes for the same user and channel are invalidated.
    /// </summary>
    /// <returns>The plain-text code to be sent to the user.</returns>
    public async Task<string> GenerateAsync(
        Guid userId,
        string destination,
        VerificationChannel channel,
        CancellationToken cancellationToken = default)
    {
        // Invalidate existing unused codes for this user/channel
        var existing = await _dbContext.VerificationCodes
            .Where(c => c.UserId == userId && c.Channel == channel && !c.IsUsed && c.ExpiresAt > DateTime.UtcNow)
            .ToListAsync(cancellationToken);

        foreach (var old in existing)
        {
            old.IsUsed = true;
        }

        var plainCode = GenerateNumericCode();
        var hashed = HashCode(plainCode);

        var entity = new VerificationCode
        {
            UserId = userId,
            CodeHash = hashed,
            Channel = channel,
            Destination = destination,
            ExpiresAt = DateTime.UtcNow.AddMinutes(ExpiryMinutes),
            IsUsed = false,
            AttemptCount = 0
        };

        _dbContext.VerificationCodes.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Verification code generated for user {UserId} via {Channel}", userId, channel);
        return plainCode;
    }

    /// <summary>
    /// Validates a verification code submitted by the user.
    /// </summary>
    /// <returns><c>true</c> if the code is valid; otherwise <c>false</c>.</returns>
    public async Task<bool> ValidateAsync(
        Guid userId,
        string plainCode,
        VerificationChannel channel,
        CancellationToken cancellationToken = default)
    {
        var hashed = HashCode(plainCode);

        var entity = await _dbContext.VerificationCodes
            .Where(c =>
                c.UserId == userId &&
                c.Channel == channel &&
                !c.IsUsed &&
                c.ExpiresAt > DateTime.UtcNow &&
                c.AttemptCount < MaxAttempts)
            .OrderByDescending(c => c.CreatedAt)
            .FirstOrDefaultAsync(cancellationToken);

        if (entity is null)
        {
            _logger.LogWarning("No valid verification code found for user {UserId}", userId);
            return false;
        }

        entity.AttemptCount++;

        if (entity.CodeHash != hashed)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
            _logger.LogWarning("Invalid verification code attempt for user {UserId}", userId);
            return false;
        }

        entity.IsUsed = true;
        entity.UsedAt = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Verification code validated for user {UserId}", userId);
        return true;
    }

    private static string GenerateNumericCode()
    {
        var bytes = RandomNumberGenerator.GetBytes(4);
        var value = BitConverter.ToUInt32(bytes, 0) % 1_000_000u;
        return value.ToString("D6");
    }

    private static string HashCode(string plainCode)
    {
        var bytes = Encoding.UTF8.GetBytes(plainCode);
        var hash = SHA256.HashData(bytes);
        return Convert.ToHexString(hash);
    }
}
