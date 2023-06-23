using BankRateAggregator.Application.Services.Currency.Models;
using BankRateAggregator.Domain.Entities.BankRates;

namespace BankRateAggregator.Application.Services.Banks.Models
{
    internal class InecoBankApiModel : BaseApiModel
    {
        public bool success { get; set; }
        public List<InecoBankRate> items { get; set; }

        public override List<Rate>? DeserializeObject(string responseBody, List<CurrencyIdValuePair> currencies, int bankId)
        {
            List<Rate>? rates = new();
            var model = System.Text.Json.JsonSerializer.Deserialize<InecoBankApiModel>(responseBody);
            rates.AddRange(model.items.Select(item => new Rate
            {
                Buy = Convert.ToDecimal(item.cash.buy),
                Sell = Convert.ToDecimal(item.cash.sell),
                CurrencyId = currencies.First(x => x.Code == item.code).Id,
                BankId = bankId
            }));
            return rates;
        }
    }
    public class Cash
    {
        public double? buy { get; set; }
        public double? sell { get; set; }
    }

    public class InecoBankRate
    {
        public string code { get; set; }
        public Cash cash { get; set; }
    }
}
