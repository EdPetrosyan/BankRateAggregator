using BankRateAggregator.Domain.Common;

namespace BankRateAggregator.Domain.Entities.Identity;

public class RoleClaim : BaseEntity<int>
{
    public int RoleId { get; set; }
    public string? ClaimType { get; set; }
    public string? ClaimValue { get; set; }
    public Role? Role { get; set; }
}
