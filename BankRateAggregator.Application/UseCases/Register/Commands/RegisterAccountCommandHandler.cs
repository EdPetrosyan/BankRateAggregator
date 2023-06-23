using BankRateAggregator.Application.Exceptions;
using BankRateAggregator.Application.Interfaces;
using BankRateAggregator.Application.Resources.Localization;
using BankRateAggregator.Application.Security;
using BankRateAggregator.Domain.Entities.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace BankRateAggregator.Application.UseCases.Register.Commands;

public class RegisterAccountCommandHandler : IRequestHandler<RegisterAccountCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IJwtTokenValidator _jwtTokenValidator;
    private readonly IStringLocalizer<ValidationMessages> _localizer;


    public RegisterAccountCommandHandler(IApplicationDbContext context, IStringLocalizer<ValidationMessages> localizer, IJwtTokenValidator jwtTokenValidator)
    {
        _context = context;
        _localizer = localizer;
        _jwtTokenValidator = jwtTokenValidator;
    }

    public async Task<string> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
    {
        await ValidateRequest(request, cancellationToken);

        var role = await _context.Roles.FirstOrDefaultAsync(x => x.Name == RoleNames.User, cancellationToken);

        User newUser = new()
        {
            Email = request.Email,
            Name = request.Name,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            BirthDate = request.BirthDate,
            PasswordHash = PasswordHasher.CreatePassword(request.Password),
            UserName = request.UserName,
            UserRoles = new List<UserRole>
            {
                new UserRole()
                {
                    Role = role
                }
            }
        };

        await _context.Users.AddAsync(newUser, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var token = _jwtTokenValidator.CreateToken(user: newUser);

        return token;

    }

    private async Task ValidateRequest(RegisterAccountCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Users
            .Where(x => x.Email == request.Email || x.UserName == request.UserName)
            .Select(x => new
            {
                x.Email,
                x.UserName
            }).FirstOrDefaultAsync(cancellationToken);


        if (entity?.Email is not null)
        {
            throw new ValidationException(_localizer["EmailExists"].Value);
        }

        if (entity?.UserName is not null)
        {
            throw new ValidationException(_localizer["UserNameExists"].Value);
        }
    }
}
