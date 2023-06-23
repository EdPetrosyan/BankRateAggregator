using BankRateAggregator.Domain.Common;

namespace BankRateAggregator.Domain.Entities.BankRates
{
    public class Bank : BaseEntity<int>
    {
        public Bank()
        {
            Rates = new List<Rate>();
        }
        public string Name { get; set; } = string.Empty;
        public string WebSiteUrl { get; set; } = string.Empty;
        public string? RateApiUrl { get; set; }
        public string? RateXPath { get; set; }
        public IList<Rate> Rates { get; set; }
    }
}
