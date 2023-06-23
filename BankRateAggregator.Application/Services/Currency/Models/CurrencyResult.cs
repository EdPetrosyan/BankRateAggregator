namespace BankRateAggregator.Application.Services.Currency.Models
{
    public class CurrencyResult
    {
        public string Currency { get; set; }
        public int CurrencyId { get; set; }
        public List<decimal> Rates { get; set; }
    }

    public class CurrencyIdValuePair
    {
        public int Id { get; set; }
        public string Code { get; set; }
    }
}
