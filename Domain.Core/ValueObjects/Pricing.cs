using SharedKernel.SeedWork;

namespace Domain.Core.ValueObjects;

public readonly partial record struct Pricing : IValueObject
{
    public readonly Money BasePrice = BasePrice;
    public readonly IEnumerable<Discount> Discounts = Discounts;
    public readonly IEnumerable<SeasonalRate> SeasonalRates = SeasonalRates;
}

public readonly partial record struct Pricing(
    Money BasePrice,
    IEnumerable<Discount> Discounts,
    IEnumerable<SeasonalRate> SeasonalRates);

public readonly partial record struct Pricing
{
    public static Money CalculatePrice(Pricing pricing)
    {
        return new Money();
    }
}