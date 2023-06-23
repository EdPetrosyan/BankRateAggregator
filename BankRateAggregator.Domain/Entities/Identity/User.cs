using BankRateAggregator.Domain.Common;

namespace BankRateAggregator.Domain.Entities.Identity;

public class User : BaseEntity<Guid>
{
    public User()
    {
        UserRoles = new List<UserRole>();
    }
    public string? UserName { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTimeOffset BirthDate { get; set; }
    public int AccessFailedCount { get; set; }
    public bool IsLocked { get; set; }
    public IList<UserRole>? UserRoles { get; set; }
}
