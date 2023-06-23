using BankRateAggregator.Application.Services.Currency.Models;
using BankRateAggregator.Domain.Entities.BankRates;

namespace BankRateAggregator.Application.Services.Banks.Models
{
    public abstract class BaseApiXMLModel
    {
        public string Url { get; set; } = "UnKnown";

        public abstract List<Rate>? DeserializeObject(Stream responseStream, List<CurrencyIdValuePair> currencies, int bankId);
    }
}
