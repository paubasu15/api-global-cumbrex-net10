using FastEndpoints;
using FluentValidation;

namespace CumbrexSaaS.Features.Auth.RequestVerificationCode;

/// <summary>Validator for <see cref="RequestVerificationCodeRequest"/>.</summary>
public sealed class RequestVerificationCodeValidator : Validator<RequestVerificationCodeRequest>
{
    /// <summary>Initializes a new instance of <see cref="RequestVerificationCodeValidator"/>.</summary>
    public RequestVerificationCodeValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email address is required.")
            .EmailAddress().WithMessage("A valid email address is required.")
            .MaximumLength(256).WithMessage("Email address must not exceed 256 characters.");
    }
}
