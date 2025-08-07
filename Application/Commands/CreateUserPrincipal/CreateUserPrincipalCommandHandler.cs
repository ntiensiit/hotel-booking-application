using Domain.Identity.Entities;
using Port.Driven.EFCore.Repositories;
using Port.Driven.Shared.Events;

namespace Application.Commands.CreateUserPrincipal;

public class CreateUserPrincipalCommandHandler : ICommandHandler<CreateUserPrincipalCommand, UserPrincipal>
{
    private readonly IUserPrincipalRepository _userPrincipalRepository;

    public CreateUserPrincipalCommandHandler(IUserPrincipalRepository userPrincipalRepository)
    {
        _userPrincipalRepository = userPrincipalRepository;
    }

    public async Task<UserPrincipal> Handle(CreateUserPrincipalCommand request, CancellationToken cancellationToken)
    {
        var userPrincipal = new UserPrincipal
        {
            Id = request.Id,
            IsActive = request.IsActive,
            LastLogin = request.LastLogin,
            UserName = request.UserName,
            Email = request.Email,
            PasswordHash = request.Password,
            PhoneNumber = request.PhoneNumber
        };

        return await _userPrincipalRepository.SaveOrUpdate(userPrincipal, cancellationToken);
    }
}