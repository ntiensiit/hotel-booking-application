using Domain.Core.Entities;
using Port.Driven.NHibernate.Persistence;
using Port.Driven.NHibernate.Repositories;
using Port.Driven.Shared.Events;
using SharedKernel.ValueObjects;

namespace Application.Commands.V1.CreateCommands.CreateUser;

public class CreateUserCommandHandlerV1 : ICommandHandler<CreateUserCommandV1, object>
{
    private readonly INhibernateUnitOfWork _nhibernateUnitOfWork;
    private readonly IUserInfoRepository _userInfoRepository;

    public CreateUserCommandHandlerV1(IUserInfoRepository userInfoRepository,
        INhibernateUnitOfWork nhibernateUnitOfWork)
    {
        _userInfoRepository = userInfoRepository;
        _nhibernateUnitOfWork = nhibernateUnitOfWork;
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