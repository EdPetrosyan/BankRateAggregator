using BankRateAggregator.Application.Resources.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BankRateAggregator.Application.UseCases.Rate.Queries
{
    public class GetRatesByCurrencyQueryValidator : AbstractValidator<GetRatesByCurrencyQuery>
    {
        public GetRatesByCurrencyQueryValidator(IStringLocalizer<ValidationMessages> localizer)
        {
            RuleFor(x => x.CurrencyId)
                .NotEmpty().WithMessage(string.Format(localizer["IsRequired"].Value, nameof(GetRatesByCurrencyQuery.CurrencyId)));
        }
    }
}
