using BankRateAggregator.Domain.Entities.Identity;
using System.Security.Claims;

namespace BankRateAggregator.Infrastructure.Common.Identity;

public static class IdentityClaims
{
    public static IList<Claim> GetClaims(User user)
    {
        List<Claim> result = new()
        {
            new Claim(ClaimTypes.Email, user?.Email?? ""),
            new Claim(ClaimTypes.GivenName, user?.Name ?? ""),
            new Claim(ClaimTypes.NameIdentifier, user?.Id.ToString() ?? "")
        };

        foreach (var role in user?.UserRoles?.Select(x => x.Role))
        {
            result.Add(new Claim(ClaimTypes.Role, role?.Name?.Trim()));
        }

        return result;
    }
}
