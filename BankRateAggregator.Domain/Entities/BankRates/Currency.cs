using BankRateAggregator.Domain.Common;

namespace BankRateAggregator.Domain.Entities.BankRates
{
    public class Currency : BaseEntity<int>
    {
        public Currency()
        {
            Rates = new List<Rate>();
        }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? Alias { get; set; }
        public IList<Rate> Rates { get; set; }
    }
}
