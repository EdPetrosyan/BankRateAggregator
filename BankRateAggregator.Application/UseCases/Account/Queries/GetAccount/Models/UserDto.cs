namespace BankRateAggregator.Application.UseCases.Account.Queries.GetAccount.Models
{
    public class UserDto
    {
        public string? Name { get; init; }
        public string? LastName { get; init; }
        public string? Email { get; init; }
        public string? PhoneNumber { get; init; }
        public DateTimeOffset BirthDate { get; init; }

    }
}
