namespace BankRateAggregator.Application.UseCases.Currency.Queries.Models
{
    public class RatesVM
    {
        public string BankName { get; set; } = string.Empty;
        public string CurrencyName { get; set; } = string.Empty;
        public decimal? Buy { get; set; }
        public decimal? Sell { get; set; }
        public DateTimeOffset LastUpdatedDate { get; set; }
    }
}
