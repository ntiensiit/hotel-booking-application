using Domain.Core.Entities;
using Port.Driven.NHibernate.Repositories;
using Port.Driven.Shared.Events;
using Port.Driven.Shared.Persistence;

namespace Adapter.Driving.ResourceServer.Commands.V1.CreateCommands.CreateUser;

public class CreateUserCommandHandlerV1 : ICommandHandler<CreateUserCommandV1, object>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInfoRepository _userInfoRepository;

    public CreateUserCommandHandlerV1(IUserInfoRepository userInfoRepository, IUnitOfWork unitOfWork)
    {
        _userInfoRepository = userInfoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<object> Handle(CreateUserCommandV1 request, CancellationToken cancellationToken)
    {
        if (request.RequestBody.Password != request.RequestBody.PasswordConfirmed)
            throw new ArgumentException("Password and confirmed password do not match.");

        var userInfo = new UserInfo<int>
        {
            FullName = request.RequestBody.FullName,
            DateOfBirth = request.RequestBody.DateOfBirth,
            Email = request.RequestBody.Email,
            PhoneNumber = request.RequestBody.PhoneNumber
        };

        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        _userInfoRepository.Add(userInfo);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        await _unitOfWork.CommitTransactionAsync(cancellationToken);

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