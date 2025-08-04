namespace SharedKernel.SeedWork;

public interface ISpecification<T>
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

internal sealed class AndSpecification<T> : Specification<T>
{
    private readonly ISpecification<T> _left;
    private readonly ISpecification<T> _right;

    public AndSpecification(ISpecification<T> left, ISpecification<T> right)
    {
        _left = left;
        _right = right;
    }

    public override bool IsSatisfiedBy(T item)
    {
        return _left.IsSatisfiedBy(item) && _right.IsSatisfiedBy(item);
    }
}

internal sealed class OrSpecification<T> : Specification<T>
{
    private readonly ISpecification<T> _left;
    private readonly ISpecification<T> _right;

    public OrSpecification(ISpecification<T> left, ISpecification<T> right)
    {
        _left = left;
        _right = right;
    }

    public override bool IsSatisfiedBy(T item)
    {
        return _left.IsSatisfiedBy(item) || _right.IsSatisfiedBy(item);
    }
}

internal sealed class NotSpecification<T> : Specification<T>
{
    private readonly ISpecification<T> _specification;

    public NotSpecification(ISpecification<T> specification)
    {
        _specification = specification;
    }

    public override bool IsSatisfiedBy(T item)
    {
        return !_specification.IsSatisfiedBy(item);
    }
}