using BankRateAggregator.Application.Services.Banks.Models;

namespace BankRateAggregator.Application.Services.Banks.Interfaces
{
    public interface IBankService
    {
        Task<List<BankVM>> GetBanksAsync(CancellationToken cancellationToken);
    }
}
