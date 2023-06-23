using BankRateAggregator.Application.Interfaces;
using BankRateAggregator.Application.Resources.Localization;
using BankRateAggregator.Application.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using ValidationException = BankRateAggregator.Application.Exceptions.ValidationException;

namespace BankRateAggregator.Application.UseCases.Account.Commands.LoginAccount;

public class LoginAccountCommandHandler : IRequestHandler<LoginAccountCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IJwtTokenValidator _jwtTokenValidator;
    private readonly IStringLocalizer<ValidationMessages> _localizer;

    public LoginAccountCommandHandler(IApplicationDbContext context, IStringLocalizer<ValidationMessages> localizer, IJwtTokenValidator jwtTokenValidator)
    {
        _context = context;
        _localizer = localizer;
        _jwtTokenValidator = jwtTokenValidator;
    }

    public async Task<string> Handle(LoginAccountCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Users.Include(x => x.UserRoles)?.ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken) ?? throw new ValidationException(_localizer["LoginFailure"].Value);

        var isValid = PasswordHasher.VerifyPassword(request.Password, entity.PasswordHash);

        if (isValid)
        {
            return _jwtTokenValidator.CreateToken(entity);
        }
        throw new ValidationException(_localizer["LoginFailure"].Value);
    }
}
