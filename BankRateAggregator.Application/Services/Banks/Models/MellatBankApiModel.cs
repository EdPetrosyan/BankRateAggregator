using BankRateAggregator.Application.Services.Currency.Models;
using BankRateAggregator.Domain.Entities.BankRates;

namespace BankRateAggregator.Application.Services.Banks.Models
{
    public class MellatBankApiModel : BaseApiModel
    {
        public Result result { get; set; }

        public override List<Rate>? DeserializeObject(string responseBody, List<CurrencyIdValuePair> currencies, int bankId)
        {
            List<Rate>? rates = new();
            var model = System.Text.Json.JsonSerializer.Deserialize<MellatBankApiModel>(responseBody);
            rates.AddRange(model.result.data.Select(item => new Rate
            {
                Buy = Convert.ToDecimal(item.buy),
                Sell = Convert.ToDecimal(item.sell),
                CurrencyId = currencies.First(x => x.Code == item.currency).Id,
                BankId = bankId
            }));
            return rates;
        }
    }
    public class MellatBankRate
    {
        public double buy { get; set; }
        public double sell { get; set; }
        public string currency { get; set; }
    }

    public class Result
    {
        public List<MellatBankRate> data { get; set; }
    }

}
