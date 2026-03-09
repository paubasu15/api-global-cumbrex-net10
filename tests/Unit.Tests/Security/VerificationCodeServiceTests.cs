using CumbrexSaaS.Domain.Enums;
using CumbrexSaaS.Infrastructure.MultiTenancy;
using CumbrexSaaS.Infrastructure.Persistence;
using CumbrexSaaS.Infrastructure.Security;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace Unit.Tests.Security;

/// <summary>
/// Unit tests for <see cref="VerificationCodeService"/>.
/// Uses EF Core's InMemory provider to avoid SQL Server dependencies.
/// </summary>
public sealed class VerificationCodeServiceTests : IDisposable
{
    private readonly ApplicationDbContext _dbContext;
    private readonly VerificationCodeService _sut;

    /// <summary>Initializes a new instance of <see cref="VerificationCodeServiceTests"/>.</summary>
    public VerificationCodeServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _dbContext = new ApplicationDbContext(options, new TenantContext());
        _sut = new VerificationCodeService(_dbContext, NullLogger<VerificationCodeService>.Instance);
    }

    [Fact(DisplayName = "GenerateAsync creates a verification code entity")]
    public async Task GenerateAsync_ShouldCreateCode()
    {
        // Arrange
        var userId = Guid.NewGuid();

        // Act
        var code = await _sut.GenerateAsync(userId, "user@example.com", VerificationChannel.Email);

        // Assert
        code.Should().HaveLength(6);
        code.Should().MatchRegex(@"^\d{6}$");

        var entity = await _dbContext.VerificationCodes.FirstOrDefaultAsync();
        entity.Should().NotBeNull();
        entity!.UserId.Should().Be(userId);
        entity.IsUsed.Should().BeFalse();
    }

    [Fact(DisplayName = "ValidateAsync returns true for correct code")]
    public async Task ValidateAsync_CorrectCode_ReturnsTrue()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var plainCode = await _sut.GenerateAsync(userId, "user@example.com", VerificationChannel.Email);

        // Act
        var result = await _sut.ValidateAsync(userId, plainCode, VerificationChannel.Email);

        // Assert
        result.Should().BeTrue();

        var entity = await _dbContext.VerificationCodes.FirstOrDefaultAsync();
        entity!.IsUsed.Should().BeTrue();
        entity.UsedAt.Should().NotBeNull();
    }

    [Fact(DisplayName = "ValidateAsync returns false for incorrect code")]
    public async Task ValidateAsync_IncorrectCode_ReturnsFalse()
    {
        // Arrange
        var userId = Guid.NewGuid();
        await _sut.GenerateAsync(userId, "user@example.com", VerificationChannel.Email);

        // Act
        var result = await _sut.ValidateAsync(userId, "000000", VerificationChannel.Email);

        // Assert
        result.Should().BeFalse();
    }

    [Fact(DisplayName = "GenerateAsync invalidates previous codes for the same user and channel")]
    public async Task GenerateAsync_InvalidatesPreviousCodes()
    {
        // Arrange
        var userId = Guid.NewGuid();
        await _sut.GenerateAsync(userId, "user@example.com", VerificationChannel.Email);

        // Act - generate second code
        await _sut.GenerateAsync(userId, "user@example.com", VerificationChannel.Email);

        // Assert - first code is invalidated
        var codes = await _dbContext.VerificationCodes.ToListAsync();
        codes.Should().HaveCount(2);
        codes.Count(c => c.IsUsed).Should().Be(1);
        codes.Count(c => !c.IsUsed).Should().Be(1);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        _dbContext.Dispose();
    }
}
