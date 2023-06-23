using BankRateAggregator.Application.Resources.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BankRateAggregator.Application.UseCases.Rate.Commands
{
    public class AddRateCommandValidator : AbstractValidator<AddRateCommand>
    {
        public AddRateCommandValidator(IStringLocalizer<ValidationMessages> localizer)
        {
            RuleFor(x => x.CurrencyId)
                .NotEmpty().WithMessage(string.Format(localizer["IsRequired"].Value, nameof(AddRateCommand.CurrencyId)));

            RuleFor(x => x.BankId)
                .NotEmpty().WithMessage(string.Format(localizer["IsRequired"].Value, nameof(AddRateCommand.BankId)));

            RuleFor(x => x.Buy)
                .NotEmpty().WithMessage(string.Format(localizer["IsRequired"].Value, nameof(AddRateCommand.Buy)));

            RuleFor(x => x.Sell)
                .NotEmpty().WithMessage(string.Format(localizer["IsRequired"].Value, nameof(AddRateCommand.Sell)));
        }
    }
}
