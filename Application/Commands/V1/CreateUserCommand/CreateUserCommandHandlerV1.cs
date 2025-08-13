using Domain.Core.Entities;
using Domain.Core.Repositories;
using Domain.Identity.Entities;
using Domain.Identity.Repositories;
using Port.Driven.EFCore.Persistence;
using Port.Driven.NHibernate.Persistence;
using Port.Driven.Shared.Events;
using Port.Driving.Shared.DTOs.V1.Responses;
using SharedKernel.ValueObjects;

namespace Application.Commands.V1.CreateUserCommand;

public class CreateUserCommandHandlerV1 : ICommandHandler<CreateUserCommandV1, UserInfoResponseDtoV1>
{
    private readonly IEfCoreUnitOfWork _efCoreUnitOfWork;
    private readonly INhibernateUnitOfWork _nhibernateUnitOfWork;
    private readonly IUserInfoRepository _userInfoRepository;
    private readonly IUserPrincipalRepository _userPrincipalRepository;

    public CreateUserCommandHandlerV1(IUserInfoRepository userInfoRepository, INhibernateUnitOfWork nhibernateUnitOfWork,
        IUserPrincipalRepository userPrincipalRepository, IEfCoreUnitOfWork efCoreUnitOfWork)
    {
        _userInfoRepository = userInfoRepository;
        _nhibernateUnitOfWork = nhibernateUnitOfWork;
        _userPrincipalRepository = userPrincipalRepository;
        _efCoreUnitOfWork = efCoreUnitOfWork;
    }

    public async Task<UserInfoResponseDtoV1> Handle(CreateUserCommandV1 request, CancellationToken cancellationToken)
    {
        if (request.Password != request.PasswordConfirmed)
            throw new ArgumentException("Password and confirmed password do not match.");
        
        var userInfo = UserInfo.Create(
            request.FullName,
            request.DateOfBirth,
            request.Email,
            new PhoneNumber(request.PhoneNumber)
        );

        await _nhibernateUnitOfWork.BeginTransactionAsync(cancellationToken);
        _userInfoRepository.Add(userInfo);
        await _nhibernateUnitOfWork.SaveChangesAsync(cancellationToken);
        await _nhibernateUnitOfWork.CommitTransactionAsync(cancellationToken);

        var userPrincipal = new UserPrincipal
        {
            Id = userInfo.Id,
            IsActive = true,
            LastLogin = DateTime.UtcNow,
            UserName = request.UserName,
            Email = request.Email,
            PasswordHash = request.Password,
            PhoneNumber = request.PhoneNumber
        };

        await _efCoreUnitOfWork.BeginTransactionAsync(cancellationToken);
        _userPrincipalRepository.Add(userPrincipal);
        await _efCoreUnitOfWork.SaveChangesAsync(cancellationToken);
        await _efCoreUnitOfWork.CommitTransactionAsync(cancellationToken);

        return new UserInfoResponseDtoV1(userInfo.FullName, userInfo.DateOfBirth, userInfo.Email, userInfo.PhoneNumber);
    }
}