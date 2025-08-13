using Domain.Core.Enums;
using SharedKernel.SeedWork;

namespace Domain.Core.ValueObjects;

public readonly partial record struct Money : IValueObject
{
    public readonly Currency Currency = Currency;
    public decimal Amount { get; private init; } = Amount;
}

public readonly partial record struct Money(decimal Amount, Currency Currency);

public readonly partial record struct Money
{
    public static implicit operator decimal(Money value)
    {
        return value.Amount;
    }

    public static implicit operator Money(decimal amount)
    {
        return new Money
        {
            Amount = amount
        };
    }

    public static Money operator +(Money left, Money right)
    {
        if (left.Currency != right.Currency)
            throw new ArgumentException($"{left.Amount} + {right.Amount} - {left.Currency}");

        return new Money(left.Amount + right.Amount, left.Currency);
    }

    public static Money operator -(Money left, Money right)
    {
        if (left.Currency != right.Currency)
            throw new ArgumentException($"{left.Amount} - {right.Amount} - {left.Currency}");

        return new Money(left.Amount - right.Amount, left.Currency);
    }

    public static Money operator *(Money left, decimal right)
    {
        return new Money(left.Amount * right, left.Currency);
    }
}