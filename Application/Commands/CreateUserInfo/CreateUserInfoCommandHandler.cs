using Domain.Core.Entities;
using Port.Driven.NHibernate.Repositories;
using Port.Driven.Shared.Events;

namespace Application.Commands.CreateUserInfo;

public class CreateUserInfoCommandHandler : ICommandHandler<CreateUserInfoCommand, UserInfo>
{
    private readonly IUserInfoRepository _userInfoRepository;

    public CreateUserInfoCommandHandler(IUserInfoRepository userInfoRepository)
    {
        _userInfoRepository = userInfoRepository;
    }

    public async Task<UserInfo> Handle(CreateUserInfoCommand request, CancellationToken cancellationToken)
    {
        var userInfo = new UserInfo
        {
            FullName = request.FullName,
            Age = request.Age,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber
        };

        _userInfoRepository.Save(userInfo);

        return await Task.FromResult(userInfo);
    }
}