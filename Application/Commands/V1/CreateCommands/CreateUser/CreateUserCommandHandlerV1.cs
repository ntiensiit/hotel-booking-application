using Domain.Core.Entities;
using Domain.Identity.Entities;
using Domain.Identity.Repositories;
using Port.Driven.EFCore.Persistence;
using Port.Driven.NHibernate.Persistence;
using Port.Driven.NHibernate.Repositories;
using Port.Driven.Shared.Events;
using SharedKernel.ValueObjects;

namespace Application.Commands.V1.CreateCommands.CreateUser;

public class CreateUserCommandHandlerV1 : ICommandHandler<CreateUserCommandV1, object>
{
    private readonly IEfCoreUnitOfWork _efCoreUnitOfWork;
    private readonly INhibernateUnitOfWork _nhibernateUnitOfWork;
    private readonly IUserInfoRepository _userInfoRepository;
    private readonly IUserPrincipalRepository _userPrincipalRepository;

    public CreateUserCommandHandlerV1(IUserInfoRepository userInfoRepository,
        INhibernateUnitOfWork nhibernateUnitOfWork,
        IUserPrincipalRepository userPrincipalRepository, IEfCoreUnitOfWork efCoreUnitOfWork)
    {
        _userInfoRepository = userInfoRepository;
        _nhibernateUnitOfWork = nhibernateUnitOfWork;
        _userPrincipalRepository = userPrincipalRepository;
        _efCoreUnitOfWork = efCoreUnitOfWork;
    }

    public async Task<object> Handle(CreateUserCommandV1 request, CancellationToken cancellationToken)
    {
        if (request.RequestBody.Password != request.RequestBody.PasswordConfirmed)
            throw new ArgumentException("Password and confirmed password do not match.");

        var userInfo = new UserInfo<int>(request.RequestBody.FullName, request.RequestBody.DateOfBirth,
            request.RequestBody.Email,
            new PhoneNumber(request.RequestBody.PhoneNumber));

        await _nhibernateUnitOfWork.BeginTransactionAsync(cancellationToken);
        _userInfoRepository.Add(userInfo);
        await _nhibernateUnitOfWork.SaveChangesAsync(cancellationToken);
        await _nhibernateUnitOfWork.CommitTransactionAsync(cancellationToken);

        var userPrincipal = new UserPrincipal
        {
            Id = userInfo.Id,
            IsActive = true,
            LastLogin = DateTime.UtcNow,
            UserName = request.RequestBody.UserName,
            Email = request.RequestBody.Email,
            PasswordHash = request.RequestBody.Password,
            PhoneNumber = request.RequestBody.PhoneNumber
        };

        await _efCoreUnitOfWork.BeginTransactionAsync(cancellationToken);
        _userPrincipalRepository.Add(userPrincipal);
        await _efCoreUnitOfWork.SaveChangesAsync(cancellationToken);
        await _efCoreUnitOfWork.CommitTransactionAsync(cancellationToken);

        return new
        {
            User = new
            {
                userInfo.FullName,
                userInfo.DateOfBirth,
                Email = (string)userInfo.Email,
                PhoneNumber = (string)userInfo.PhoneNumber,
                userInfo.Id
            }
        };
    }
}