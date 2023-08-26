using BankRateAggregator.Application.Services.Currency.Models;
using BankRateAggregator.Domain.Entities.BankRates;

namespace BankRateAggregator.Application.Services.Banks.Models
{
    public class FastBankApiModel : BaseApiModel
    {
        public List<FastBankRate> Rates { get; set; }
        public override List<Rate>? DeserializeObject(string responseBody, List<CurrencyIdValuePair> currencies, int bankId)
        {
            List<Rate>? rates = new();
            var model = System.Text.Json.JsonSerializer.Deserialize<FastBankApiModel>(responseBody);
            rates.AddRange(model.Rates.Where(x => currencies.Select(s => s.Code).Contains(x.Id)).Select(item => new Rate
            {
                Buy = Convert.ToDecimal(item.Buy),
                Sell = Convert.ToDecimal(item.Sale),
                CurrencyId = currencies.First(x => x.Code == item.Id).Id,
                BankId = bankId
            }));
            return rates;
        }
    }
    public class FastBankRate
    {
        public double Buy { get; set; }
        public string Id { get; set; }
        public double Sale { get; set; }
    }
}
