using Domain.Identity.Entities;
using Domain.Identity.Repositories;
using Port.Driven.EFCore.Persistence;
using Port.Driven.Shared.Events;

namespace Application.Commands.CreateUserPrincipal;

public class CreateUserPrincipalCommandHandler : ICommandHandler<CreateUserPrincipalCommand, UserPrincipal>
{
    private readonly IEfCoreUnitOfWork _efCoreUnitOfWork;
    private readonly IUserPrincipalRepository _userPrincipalRepository;

    public CreateUserPrincipalCommandHandler(IUserPrincipalRepository userPrincipalRepository,
        IEfCoreUnitOfWork efCoreUnitOfWork)
    {
        _userPrincipalRepository = userPrincipalRepository;
        _efCoreUnitOfWork = efCoreUnitOfWork;
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

        await _efCoreUnitOfWork.BeginTransactionAsync(cancellationToken);
        _userPrincipalRepository.Add(userPrincipal);
        await _efCoreUnitOfWork.SaveChangesAsync(cancellationToken);
        await _efCoreUnitOfWork.CommitTransactionAsync(cancellationToken);

        return await Task.FromResult(userPrincipal);
    }
}