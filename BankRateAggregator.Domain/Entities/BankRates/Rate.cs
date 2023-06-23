using BankRateAggregator.Domain.Common;

namespace BankRateAggregator.Domain.Entities.BankRates
{
    public class Rate : BaseEntity<int>
    {
        public int BankId { get; set; }
        public int CurrencyId { get; set; }
        public decimal? Buy { get; set; }
        public decimal? Sell { get; set; }
        public Currency Currency { get; set; }
        public Bank Bank { get; set; }
    }
}
