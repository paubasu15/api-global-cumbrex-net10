using CumbrexSaaS.Infrastructure;
using CumbrexSaaS.Infrastructure.MultiTenancy;
using FastEndpoints;
using FastEndpoints.Swagger;
using QuestPDF.Infrastructure;
using Scalar.AspNetCore;
using Serilog;

QuestPDF.Settings.License = LicenseType.Community;

var builder = WebApplication.CreateBuilder(args);

// Serilog
builder.Host.UseSerilog((ctx, lc) => lc
    .ReadFrom.Configuration(ctx.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.Seq(ctx.Configuration["Serilog:SeqUrl"] ?? "http://localhost:5341"));

// Validate JWT secret is not the default placeholder in non-development environments
var jwtSecret = builder.Configuration["JwtSettings:Secret"];
if (!builder.Environment.IsDevelopment())
{
    const string placeholder = "CHANGE_ME_USE_A_LONG_SECRET_IN_PRODUCTION_AT_LEAST_32_CHARS";
    if (string.IsNullOrWhiteSpace(jwtSecret) || jwtSecret == placeholder || jwtSecret.Length < 32)
    {
        throw new InvalidOperationException(
            "JwtSettings:Secret must be configured with a cryptographically secure value of at least 32 characters. " +
            "Use environment variables, Azure Key Vault, or another secure configuration provider.");
    }
}

// Infrastructure
builder.Services.AddInfrastructure(builder.Configuration);

// FastEndpoints
builder.Services.AddFastEndpoints()
    .SwaggerDocument(o =>
    {
        o.DocumentSettings = s =>
        {
            s.Title = "CumbrexSaaS API";
            s.Version = "v1";
            s.Description = "Multi-Tenant SaaS B2B Platform API";
        };
    });

// Authentication / Authorization
builder.Services.AddAuthentication()
    .AddJwtBearer();
builder.Services.AddAuthorization();

// Health checks
builder.Services.AddHealthChecks()
    .AddSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty,
        name: "database",
        tags: ["db", "sql", "ready"])
    .AddRedis(
        builder.Configuration.GetConnectionString("Redis") ?? "localhost",
        name: "redis",
        tags: ["cache", "ready"]);

var app = builder.Build();

// Middleware pipeline
app.UseHttpsRedirection();
app.UseMiddleware<TenantResolutionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

// FastEndpoints
app.UseFastEndpoints(c =>
{
    c.Errors.UseProblemDetails();
});

// OpenAPI / Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerGen();
    app.MapScalarApiReference();
}

// Health checks
app.MapHealthChecks("/health");
app.MapHealthChecks("/health/ready", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    Predicate = check => check.Tags.Contains("ready")
});

app.Run();

/// <summary>Entry point marker for integration test WebApplicationFactory.</summary>
public partial class Program { }
