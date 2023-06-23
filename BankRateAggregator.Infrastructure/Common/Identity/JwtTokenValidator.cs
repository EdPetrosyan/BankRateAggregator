using BankRateAggregator.Application.Exceptions;
using BankRateAggregator.Application.Security;
using BankRateAggregator.Domain.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BankRateAggregator.Infrastructure.Common.Identity;

public class JwtTokenValidator : ISecurityTokenValidator, IJwtTokenValidator
{
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
    private readonly IConfiguration _configuration;

    public JwtTokenValidator(IConfiguration configuration)
    {
        _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        _configuration = configuration;
    }

    public bool CanValidateToken => true;

    public int MaximumTokenSizeInBytes { get; set; } = TokenValidationParameters.DefaultMaximumTokenSizeInBytes;

    public bool CanReadToken(string securityToken)
    {
        return _jwtSecurityTokenHandler.CanReadToken(securityToken);
    }

    public string CreateToken(User user)
    {
        var claims = IdentityClaims.GetClaims(user);
        var token = JwtUtils.GenerateJwtToken(claims, _configuration);
        return token;
    }

    public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
    {
        ClaimsPrincipal claimsPrincipal;
        try
        {
            claimsPrincipal = _jwtSecurityTokenHandler.ValidateToken(securityToken, validationParameters, out validatedToken);
        }
        catch (SecurityTokenException)
        {
            throw new ForbiddenException();
        }
        catch (Exception)
        {
            throw new ForbiddenException();
        }
        return claimsPrincipal;
    }
}
