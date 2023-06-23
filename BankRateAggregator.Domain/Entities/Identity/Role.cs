using BankRateAggregator.Domain.Common;

namespace BankRateAggregator.Domain.Entities.Identity;

public class Role : BaseEntity<int>
{
    public Role()
    {
        UserRoles = new List<UserRole>();
        RoleClaims = new List<RoleClaim>();
    }
    public string? Name { get; set; }
    public IList<UserRole>? UserRoles { get; set; }
    public IList<RoleClaim>? RoleClaims { get; set; }
}
