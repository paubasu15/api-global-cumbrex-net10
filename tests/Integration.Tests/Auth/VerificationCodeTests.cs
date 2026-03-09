using System.Net;
using System.Net.Http.Json;
using CumbrexSaaS.Features.Auth.RequestVerificationCode;
using FluentAssertions;
using Integration.Tests.Fixtures;

namespace Integration.Tests.Auth;

/// <summary>
/// Integration tests for the verification code authentication flow.
/// Uses a real SQL Server container via Testcontainers.
/// </summary>
[Collection("Integration")]
public sealed class VerificationCodeTests : IAsyncLifetime
{
    private readonly IntegrationTestFixture _fixture;

    /// <summary>Initializes a new instance of <see cref="VerificationCodeTests"/>.</summary>
    public VerificationCodeTests(IntegrationTestFixture fixture)
    {
        _fixture = fixture;
    }

    /// <inheritdoc />
    public Task InitializeAsync() => _fixture.ResetDatabaseAsync();

    /// <inheritdoc />
    public Task DisposeAsync() => Task.CompletedTask;

    [Fact(DisplayName = "POST /api/auth/request-code returns 200 for valid email")]
    public async Task RequestCode_ValidEmail_Returns200()
    {
        // Arrange
        var request = new RequestVerificationCodeRequest { Email = "test@example.com" };

        // Act
        var response = await _fixture.Client.PostAsJsonAsync("/api/auth/request-code", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var body = await response.Content.ReadFromJsonAsync<RequestVerificationCodeResponse>();
        body.Should().NotBeNull();
        body!.Success.Should().BeTrue();
        body.ExpiresInSeconds.Should().Be(600);
    }

    [Fact(DisplayName = "POST /api/auth/request-code returns 400 for invalid email")]
    public async Task RequestCode_InvalidEmail_Returns400()
    {
        // Arrange
        var request = new { Email = "not-an-email" };

        // Act
        var response = await _fixture.Client.PostAsJsonAsync("/api/auth/request-code", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact(DisplayName = "POST /api/auth/verify-code returns 401 for invalid code")]
    public async Task VerifyCode_InvalidCode_Returns401()
    {
        // Arrange - first request a code
        var requestCodePayload = new RequestVerificationCodeRequest { Email = "test2@example.com" };
        await _fixture.Client.PostAsJsonAsync("/api/auth/request-code", requestCodePayload);

        var verifyPayload = new { Email = "test2@example.com", Code = "000000" };

        // Act
        var response = await _fixture.Client.PostAsJsonAsync("/api/auth/verify-code", verifyPayload);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}

/// <summary>Defines the shared integration test collection using <see cref="IntegrationTestFixture"/>.</summary>
[CollectionDefinition("Integration")]
public sealed class IntegrationTestCollection : ICollectionFixture<IntegrationTestFixture> { }
