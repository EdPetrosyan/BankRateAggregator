namespace BankRateAggregator.Application.Services.Banks.Models
{
    public class BankVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string? XPath { get; set; }
        public string? ApiUrl { get; set; }
    }
}
