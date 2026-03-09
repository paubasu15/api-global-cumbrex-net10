using System.Reflection;
using FluentAssertions;
using NetArchTest.Rules;

namespace Architecture.Tests;

/// <summary>
/// Architecture tests that enforce the layered architecture of the CumbrexSaaS solution.
/// These tests verify that dependency rules are respected across all layers.
/// </summary>
public sealed class ArchitectureTests
{
    private static readonly Assembly DomainAssembly =
        typeof(CumbrexSaaS.Domain.Common.BaseEntity).Assembly;

    private static readonly Assembly InfrastructureAssembly =
        typeof(CumbrexSaaS.Infrastructure.Persistence.ApplicationDbContext).Assembly;

    private static readonly Assembly FeaturesAssembly =
        typeof(CumbrexSaaS.Features.Auth.VerifyCode.VerifyCodeEndpoint).Assembly;

    private static readonly Assembly ApiAssembly =
        typeof(Program).Assembly;

    [Fact(DisplayName = "Domain should not reference Infrastructure")]
    public void Domain_Should_Not_Reference_Infrastructure()
    {
        var result = Types.InAssembly(DomainAssembly)
            .ShouldNot()
            .HaveDependencyOn("CumbrexSaaS.Infrastructure")
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            because: "Domain is the innermost layer and must not depend on Infrastructure.");
    }

    [Fact(DisplayName = "Domain should not reference Features")]
    public void Domain_Should_Not_Reference_Features()
    {
        var result = Types.InAssembly(DomainAssembly)
            .ShouldNot()
            .HaveDependencyOn("CumbrexSaaS.Features")
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            because: "Domain must not depend on Features.");
    }

    [Fact(DisplayName = "Domain should not reference Api")]
    public void Domain_Should_Not_Reference_Api()
    {
        var result = Types.InAssembly(DomainAssembly)
            .ShouldNot()
            .HaveDependencyOn("CumbrexSaaS.Api")
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            because: "Domain must not depend on the Api layer.");
    }

    [Fact(DisplayName = "Infrastructure should not reference Features")]
    public void Infrastructure_Should_Not_Reference_Features()
    {
        var result = Types.InAssembly(InfrastructureAssembly)
            .ShouldNot()
            .HaveDependencyOn("CumbrexSaaS.Features")
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            because: "Infrastructure must not depend on Features.");
    }

    [Fact(DisplayName = "Infrastructure should not reference Api")]
    public void Infrastructure_Should_Not_Reference_Api()
    {
        var result = Types.InAssembly(InfrastructureAssembly)
            .ShouldNot()
            .HaveDependencyOn("CumbrexSaaS.Api")
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            because: "Infrastructure must not depend on the Api layer.");
    }

    [Fact(DisplayName = "Features should not reference Api")]
    public void Features_Should_Not_Reference_Api()
    {
        var result = Types.InAssembly(FeaturesAssembly)
            .ShouldNot()
            .HaveDependencyOn("CumbrexSaaS.Api")
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            because: "Features must not depend on the Api layer.");
    }

    [Fact(DisplayName = "Domain entities should reside in correct namespace")]
    public void Domain_Entities_Should_Be_In_Domain_Namespace()
    {
        var result = Types.InAssembly(DomainAssembly)
            .That()
            .Inherit(typeof(CumbrexSaaS.Domain.Common.BaseEntity))
            .Should()
            .ResideInNamespaceStartingWith("CumbrexSaaS.Domain")
            .GetResult();

        result.IsSuccessful.Should().BeTrue(
            because: "All domain entities must live in the CumbrexSaaS.Domain namespace.");
    }

    [Fact(DisplayName = "Infrastructure configurations should implement IEntityTypeConfiguration")]
    public void Infrastructure_Configurations_Should_Implement_IEntityTypeConfiguration()
    {
        var configTypes = InfrastructureAssembly.GetTypes()
            .Where(t => t.Name.EndsWith("Configuration") && !t.IsAbstract)
            .ToList();

        configTypes.Should().NotBeEmpty(
            because: "Infrastructure must define EF Core entity configurations.");

        foreach (var configType in configTypes)
        {
            var implementsInterface = configType.GetInterfaces()
                .Any(i => i.IsGenericType &&
                          i.GetGenericTypeDefinition() == typeof(Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<>));

            implementsInterface.Should().BeTrue(
                because: $"{configType.Name} must implement IEntityTypeConfiguration<T>.");
        }
    }
}
