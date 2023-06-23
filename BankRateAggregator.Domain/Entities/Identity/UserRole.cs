using BankRateAggregator.Domain.Common;

namespace BankRateAggregator.Domain.Entities.Identity;

public class UserRole : BaseEntity<int>
{
    public Guid UserId { get; set; }
    public int RoleId { get; set; }
    public Role? Role { get; set; }
    public User? User { get; set; }
}
