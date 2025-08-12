using Domain.Core.Entities;
using Domain.Core.Repositories;
using Port.Driven.NHibernate.Persistence;
using Port.Driven.Shared.Events;
using SharedKernel.ValueObjects;

namespace Application.Commands.CreateUserInfo;

public class CreateUserInfoCommandHandler : ICommandHandler<CreateUserInfoCommand, UserInfo>
{
    private readonly INhibernateUnitOfWork _nhibernateUnitOfWork;
    private readonly IUserInfoRepository _userInfoRepository;

    public CreateUserInfoCommandHandler(IUserInfoRepository userInfoRepository,
        INhibernateUnitOfWork nhibernateUnitOfWork)
    {
        _userInfoRepository = userInfoRepository;
        _nhibernateUnitOfWork = nhibernateUnitOfWork;
    }

    public async Task<UserInfo> Handle(CreateUserInfoCommand request, CancellationToken cancellationToken)
    {
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

        return await Task.FromResult(userInfo);
    }
}