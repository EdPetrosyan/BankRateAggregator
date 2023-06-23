using BankRateAggregator.Application.Services.Currency.Models;
using BankRateAggregator.Domain.Entities.BankRates;

namespace BankRateAggregator.Application.Services.Banks.Models
{
    public class ConverseBankApiModel : BaseApiModel
    {

        public ConverseBankRate[] Cash { get; set; }

        public override List<Rate>? DeserializeObject(string responseBody, List<CurrencyIdValuePair> currencies, int bankId)
        {
            List<Rate>? rates = new();
            var model = System.Text.Json.JsonSerializer.Deserialize<ConverseBankApiModel>(responseBody);
            rates.AddRange(model.Cash.Select(item => new Rate
            {
                Buy = Convert.ToDecimal(item.buy),
                Sell = Convert.ToDecimal(item.sell),
                CurrencyId = currencies.First(x => x.Code == item.currency.iso).Id,
                BankId = bankId
            }));
            return rates;
        }
    }

    public class ConverseBankRate
    {

        public double buy { get; set; }

        public double sell { get; set; }

        public Currency currency { get; set; }
    }

    public class Currency
    {
        public string iso { get; set; }
    }
}
