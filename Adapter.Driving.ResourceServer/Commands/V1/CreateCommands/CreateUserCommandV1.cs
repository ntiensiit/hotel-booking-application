using Adapter.Driving.ResourceServer.DTOs.V1.Requests.User;
using Domain.Core.Entities;
using Port.Driven.NHibernate;
using Port.Driven.Shared.Events;
using Port.Driven.Shared.Persistence;

namespace Adapter.Driving.ResourceServer.Commands.V1.CreateCommands;

public record CreateUserCommandV1(UserCreateRequestBodyV1 RequestBody) : ICommand<object>;

public class CreateUserCommandHandlerV1(
    IUserInfoRepository userInfoRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<CreateUserCommandV1, object>
{
    public async Task<object> HandleAsync(CreateUserCommandV1 request, CancellationToken cancellationToken)
    {
        var userInfo = new UserInfo
        {
            FullName = request.RequestBody.FullName,
            DateOfBirth = request.RequestBody.DateOfBirth,
            Email = request.RequestBody.Email,
            PhoneNumber = request.RequestBody.PhoneNumber,
        };

        await unitOfWork.BeginTransactionAsync(cancellationToken);
        userInfoRepository.Add(userInfo);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await unitOfWork.CommitTransactionAsync(cancellationToken);

        return new
        {
            User = new
            {
                userInfo.FullName,
                userInfo.DateOfBirth,
                Email = (string)userInfo.Email,
                PhoneNumber = (string)userInfo.PhoneNumber,
                userInfo.Id,
            },
        };
    }
}
