using SharedKernel.SeedWork;

namespace Domain.Identity.ValueObjects;

public readonly record struct UserStatus(UserStatusType Value) : IValueObject
{
    public static readonly UserStatus Active = new(UserStatusType.Active);
    public static readonly UserStatus Inactive = new(UserStatusType.Inactive);
    public static readonly UserStatus Suspended = new(UserStatusType.Suspended);
    public static readonly UserStatus Deleted = new(UserStatusType.Deleted);

    public bool CanTransitionTo(UserStatus target)
    {
        return this != Deleted;
    }
}