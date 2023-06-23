using BankRateAggregator.Domain.Entities.Identity;

namespace BankRateAggregator.Application.Security
{
    public interface IJwtTokenValidator
    {
        public string CreateToken(User user);
    }
}
