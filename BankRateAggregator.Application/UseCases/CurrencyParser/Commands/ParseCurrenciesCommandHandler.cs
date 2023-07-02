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
                    // if bank has XML url to get currency rates
                    if (bank.ApiUrl.EndsWith("xml"))
                    {
                        // getting specific bank type from Factory method
                        BaseApiXMLModel type = BaseApiXMLFactory.GetApiModel(bank.ApiUrl);
                        // calling API that will return XML result and parsing to Db Entity model
                        bankRates = await _currencyParser.ApiCallXmlAsync(type, bank.ApiUrl, bank.Id, currencies, cancellationToken);
                    }
                    else //if bank has API with JSON return type
                    {
                        // getting specific bank type from Factory method
                        BaseApiModel type = BaseApiFactory.GetApiModel(bank.ApiUrl);
                        // calling API that will return JSON result and parsing to Db Entity model
                        bankRates = await _currencyParser.ApiCallJsonAsync(type, bank.ApiUrl, bank.Id, currencies, cancellationToken);
                    }
                }
                else
                {
                    //if there are no APIs we will directly go to the website and by doing Web Scrapping we will get rates and parse that into Db Entity model
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
