using Domain.Core.Enums;
using SharedKernel.SeedWork;

namespace Domain.Core.ValueObjects;

public readonly partial record struct SpecialRequest : IValueObject
{
    public readonly string Description = Description;
    public readonly bool IsFulFilled = IsFulFilled;
    public readonly RequestType Type = Type;
}

public readonly partial record struct SpecialRequest(
    string Description,
    bool IsFulFilled,
    RequestType Type
);

public readonly partial record struct SpecialRequest { }
