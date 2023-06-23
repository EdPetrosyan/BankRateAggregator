using BankRateAggregator.Application.Resources.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BankRateAggregator.Application.UseCases.Account.Commands.LoginAccount
{
    public class LoginAccountCommandValidator : AbstractValidator<LoginAccountCommand>
    {
        public LoginAccountCommandValidator(IStringLocalizer<ValidationMessages> localizer)
        {

            RuleFor(x => x.Email)
             .NotEmpty().WithMessage(localizer["EmailIsRequired"].Value)
             .EmailAddress().WithMessage(localizer["InvalidEmailFormat"].Value)
             .MaximumLength(256);

            RuleFor(x => x.Password)
             .NotEmpty().WithMessage(localizer["PasswordIsRequired"].Value);
        }
    }
}
