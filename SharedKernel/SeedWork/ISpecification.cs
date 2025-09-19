namespace SharedKernel.SeedWork;

public interface ISpecification<in T>
{
    bool IsSatisfiedBy(T item);
}

public abstract class Specification<T> : ISpecification<T>
{
    public abstract bool IsSatisfiedBy(T item);

    // AND operator
    public static Specification<T> operator &(Specification<T> left, Specification<T> right)
    {
        return new AndSpecification<T>(left, right);
    }

    // OR operator
    public static Specification<T> operator |(Specification<T> left, Specification<T> right)
    {
        return new OrSpecification<T>(left, right);
    }

    // NOT operator
    public static Specification<T> operator !(Specification<T> specification)
    {
        return new NotSpecification<T>(specification);
    }
}

internal sealed class AndSpecification<T>(ISpecification<T> left, ISpecification<T> right)
    : Specification<T>
{
    private readonly ISpecification<T> _left = left;
    private readonly ISpecification<T> _right = right;

    public override bool IsSatisfiedBy(T item)
    {
        return _left.IsSatisfiedBy(item) && _right.IsSatisfiedBy(item);
    }
}

internal sealed class OrSpecification<T>(ISpecification<T> left, ISpecification<T> right)
    : Specification<T>
{
    private readonly ISpecification<T> _left = left;
    private readonly ISpecification<T> _right = right;

    public override bool IsSatisfiedBy(T item)
    {
        return _left.IsSatisfiedBy(item) || _right.IsSatisfiedBy(item);
    }
}

internal sealed class NotSpecification<T>(ISpecification<T> specification) : Specification<T>
{
    private readonly ISpecification<T> _specification = specification;

    public override bool IsSatisfiedBy(T item)
    {
        return !_specification.IsSatisfiedBy(item);
    }
}
