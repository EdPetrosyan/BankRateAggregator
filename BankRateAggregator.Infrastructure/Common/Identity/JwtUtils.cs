using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankRateAggregator.Infrastructure.Common.Identity;

public static class JwtUtils
{
    public static string GenerateJwtToken(IList<Claim> claims, IConfiguration configuration)
    {
        var key = Encoding.ASCII.GetBytes(configuration.GetRequiredSection("Identity:SigninCredential").Value ?? throw new InvalidOperationException());
        JwtSecurityTokenHandler tokenHandler = new();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = configuration.GetRequiredSection("Identity:Issuer").Value,
            Audience = configuration.GetRequiredSection("Identity:Audience").Value,
            Expires = DateTime.UtcNow.AddMinutes(double.Parse(configuration.GetRequiredSection("Identity:ExpirationDuration").Value ?? "")),
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var newToken = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(newToken);

        return tokenString;
    }
}
