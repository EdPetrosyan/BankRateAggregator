using BankRateAggregator.Application.Resources.Localization;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BankRateAggregator.Application.UseCases.Account.Queries.GetAccount
{
    public class GetAccountQueryValidator : AbstractValidator<GetAccountQuery>
    {
        public GetAccountQueryValidator(IStringLocalizer<ValidationMessages> localizer)
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(string.Format(localizer["IsRequired"].Value, nameof(GetAccountQuery.Id)));
        }
    }
}
