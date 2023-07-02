using BankRateAggregator.Application.Interfaces;
using BankRateAggregator.Application.Services.Banks.Interfaces;
using BankRateAggregator.Application.Services.Banks.Models;
using BankRateAggregator.Application.Services.Currency.Interfaces;
using MediatR;

namespace BankRateAggregator.Application.UseCases.CurrencyParser.Commands
{
    public class ParseCurrenciesCommandHandler : IRequestHandler<ParseCurrenciesCommand>
    {
        private readonly ICurrencyParserService _currencyParser;
        private readonly IBankService _bankService;
        private readonly IApplicationDbContext _dbContext;

        public ParseCurrenciesCommandHandler(ICurrencyParserService currencyParser, IBankService bankService, IApplicationDbContext dbContext)
        {
            _currencyParser = currencyParser;
            _bankService = bankService;
            _dbContext = dbContext;
        }

        public async Task Handle(ParseCurrenciesCommand request, CancellationToken cancellationToken)
        {
            var banks = await _bankService.GetBanksAsync(cancellationToken);
            var currencies = await _currencyParser.GetCurrencies(cancellationToken);
            List<BankRateAggregator.Domain.Entities.BankRates.Rate> rates = new();
            foreach (var bank in banks)
            {
                List<BankRateAggregator.Domain.Entities.BankRates.Rate> bankRates;
                if (bank.ApiUrl is not null)
                {
                    if (bank.ApiUrl.EndsWith("xml"))
                    {
                        BaseApiXMLModel type = BaseApiXMLFactory.GetApiModel(bank.ApiUrl);
                        bankRates = await _currencyParser.ApiCallXmlAsync(type, bank.ApiUrl, bank.Id, currencies, cancellationToken);
                    }
                    else
                    {
                        BaseApiModel type = BaseApiFactory.GetApiModel(bank.ApiUrl);
                        bankRates = await _currencyParser.ApiCallJsonAsync(type, bank.ApiUrl, bank.Id, currencies, cancellationToken);
                    }
                }
                else
                {
                    bankRates = await _currencyParser.WebScrappingAsync(bank.Url, bank.XPath, bank.Id, currencies, cancellationToken);
                }

                if (bankRates != null)
                {
                    rates.AddRange(bankRates);
                }
            }

            await _dbContext.Rates.AddRangeAsync(rates, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

        }
    }
}
