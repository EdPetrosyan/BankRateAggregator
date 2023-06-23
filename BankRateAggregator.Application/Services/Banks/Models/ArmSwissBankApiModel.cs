using BankRateAggregator.Application.Services.Currency.Models;
using BankRateAggregator.Domain.Entities.BankRates;

namespace BankRateAggregator.Application.Services.Banks.Models
{
    public class ArmSwissBankApiModel : BaseApiModel
    {
        public List<ArmSwissBankRate> lmasbrate { get; set; }
        public override List<Rate>? DeserializeObject(string responseBody, List<CurrencyIdValuePair> currencies, int bankId)
        {
            List<Rate>? rates = new();
            var model = System.Text.Json.JsonSerializer.Deserialize<ArmSwissBankApiModel>(responseBody);
            rates.AddRange(model.lmasbrate.Select(item => new Rate
            {
                Buy = Convert.ToDecimal(item.BID_cash),
                Sell = Convert.ToDecimal(item.OFFER_cash),
                CurrencyId = currencies.First(x => x.Code == item.ISO).Id,
                BankId = bankId,
            }));
            return rates;
        }
    }
    public class ArmSwissBankRate
    {
        public string ISO { get; set; }
        public string BID_cash { get; set; }
        public string OFFER_cash { get; set; }
    }

}
