using BankRateAggregator.Application.Resources.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BankRateAggregator.Application.UseCases.Rate.Queries
{
    public class GetRatesByBankQueryValidator : AbstractValidator<GetRatesByBankQuery>
    {
        public GetRatesByBankQueryValidator(IStringLocalizer<ValidationMessages> localizer)
        {
            RuleFor(x => x.BankId)
                .NotEmpty().WithMessage(string.Format(localizer["IsRequired"].Value, nameof(GetRatesByBankQuery.BankId)));
        }
    }
}
