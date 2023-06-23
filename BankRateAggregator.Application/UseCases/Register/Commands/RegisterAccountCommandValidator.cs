using BankRateAggregator.Application.Resources.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BankRateAggregator.Application.UseCases.Register.Commands;

public class RegisterAccountCommandValidator : AbstractValidator<RegisterAccountCommand>
{
    public RegisterAccountCommandValidator(IStringLocalizer<ValidationMessages> localizer)
    {
        RuleFor(c => c.Email)
             .EmailAddress().WithMessage(localizer["InvalidEmailFormat"].Value)
             .MaximumLength(256);

        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage(localizer["UsernameIsRequired"].Value)
            .MaximumLength(36);

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(localizer["NameIsRequired"].Value)
            .MaximumLength(64);

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage(localizer["LastNameIsRequired"].Value)
            .MaximumLength(128);

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(16);

        RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage(localizer["BirthDateIsRequired"].Value)
            .Must(BeAValidBirthDate).WithMessage(localizer["InvalidBirthDate"].Value);

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(localizer["PasswordIsRequired"].Value)
            .MinimumLength(8).WithMessage(localizer["InvalidPasswordLength"].Value)
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$")
            .WithMessage(localizer["PasswordComplexityIssue"].Value);

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password).WithMessage(localizer["PasswordMissMatch"].Value);
    }

    private static bool BeAValidBirthDate(DateTimeOffset birthDate)
    {
        return birthDate < DateTimeOffset.Now && birthDate > DateTimeOffset.Now.AddYears(-100);
    }

}
