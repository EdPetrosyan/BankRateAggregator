using BankRateAggregator.Application.Services.Banks.Models;
using BankRateAggregator.Application.Services.Currency.Models;
using BankRateAggregator.Domain.Entities.BankRates;

namespace BankRateAggregator.Application.Services.Currency.Interfaces
{
    public interface ICurrencyParserService
    {
        Task<List<Rate>?> ApiCallJsonAsync(BaseApiModel model, string url, int bankId, List<CurrencyIdValuePair> currencies, CancellationToken cancellationToken);
        Task<List<Rate>?> ApiCallXmlAsync(BaseApiXMLModel model, string url, int bankId, List<CurrencyIdValuePair> currencies, CancellationToken cancellationToken);
        Task<List<CurrencyIdValuePair>> GetCurrencies(CancellationToken cancellationToken);
        Task<List<Rate>?> WebScrappingAsync(string url, string xPath, int bankId, List<CurrencyIdValuePair> currencies, CancellationToken cancellationToken);
    }
}
