using Domain.Identity.Enums;
using SharedKernel.SeedWork;

namespace Domain.Identity.ValueObjects;

public readonly partial record struct UserStatus(UserStatusType Value) : IValueObject
{
    public static readonly UserStatus Active = new(UserStatusType.Active);
    public static readonly UserStatus Inactive = new(UserStatusType.Inactive);
    public static readonly UserStatus Suspended = new(UserStatusType.Suspended);
    public static readonly UserStatus Deleted = new(UserStatusType.Deleted);
}

public readonly partial record struct UserStatus
{
    public bool CanTransitionTo(UserStatus target)
    {
        return this != Deleted;
    }
}