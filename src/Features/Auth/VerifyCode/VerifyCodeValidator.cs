using FastEndpoints;
using FluentValidation;

namespace CumbrexSaaS.Features.Auth.VerifyCode;

/// <summary>Validator for <see cref="VerifyCodeRequest"/>.</summary>
public sealed class VerifyCodeValidator : Validator<VerifyCodeRequest>
{
    /// <summary>Initializes a new instance of <see cref="VerifyCodeValidator"/>.</summary>
    public VerifyCodeValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email address is required.")
            .EmailAddress().WithMessage("A valid email address is required.");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Verification code is required.")
            .Length(6).WithMessage("Verification code must be exactly 6 digits.")
            .Matches(@"^\d{6}$").WithMessage("Verification code must contain only digits.");
    }
}
