using System.Linq.Expressions;

namespace SharedKernel.SeedWork;

internal interface ISpecification<T>
{
    bool IsSatisfiedBy(T item);
    Expression<Func<T, bool>> ToExpression();
}

public abstract class Specification<T> : ISpecification<T>
{
    public abstract Expression<Func<T, bool>> ToExpression();

    public virtual bool IsSatisfiedBy(T item)
    {
        var predicate = ToExpression().Compile();
        return predicate(item);
    }

    // AND operator
    public static Specification<T> operator &(Specification<T> left, Specification<T> right)
        => new AndSpecification(left, right);

    // OR operator
    public static Specification<T> operator |(Specification<T> left, Specification<T> right)
        => new OrSpecification(left, right);

    // NOT operator
    public static Specification<T> operator !(Specification<T> specification)
        => new NotSpecification(specification);

    public static bool operator true(Specification<T> specification) => true;
    public static bool operator false(Specification<T> specification) => false;

    // Helper to replace parameters in expression trees
    protected static Expression<Func<T, bool>> CombineExpressions(
        Expression<Func<T, bool>> left,
        Expression<Func<T, bool>> right,
        Func<Expression, Expression, BinaryExpression> merge)
    {
        var parameter = Expression.Parameter(typeof(T), "x");

        // Replace both left and right parameter with the new one
        var leftVisitor = new ReplaceParameterVisitor(left.Parameters[0], parameter);
        var rightVisitor = new ReplaceParameterVisitor(right.Parameters[0], parameter);

        var leftBody = leftVisitor.Visit(left.Body);
        var rightBody = rightVisitor.Visit(right.Body);

        var body = merge(leftBody!, rightBody!);
        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }

    private sealed class ReplaceParameterVisitor : ExpressionVisitor
    {
        private readonly ParameterExpression _oldParam;
        private readonly ParameterExpression _newParam;

        public ReplaceParameterVisitor(ParameterExpression oldParam, ParameterExpression newParam)
        {
            _oldParam = oldParam;
            _newParam = newParam;
        }

        protected override Expression VisitParameter(ParameterExpression node)
            => node == _oldParam ? _newParam : base.VisitParameter(node);
    }

    // AND specification
    private sealed class AndSpecification : Specification<T>
    {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;

        public AndSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override bool IsSatisfiedBy(T item)
            => _left.IsSatisfiedBy(item) && _right.IsSatisfiedBy(item);

        public override Expression<Func<T, bool>> ToExpression()
            => CombineExpressions(_left.ToExpression(), _right.ToExpression(), Expression.AndAlso);
    }

    // OR specification
    private sealed class OrSpecification : Specification<T>
    {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;

        public OrSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override bool IsSatisfiedBy(T item)
            => _left.IsSatisfiedBy(item) || _right.IsSatisfiedBy(item);

        public override Expression<Func<T, bool>> ToExpression()
            => CombineExpressions(_left.ToExpression(), _right.ToExpression(), Expression.OrElse);
    }

    // NOT specification
    private sealed class NotSpecification : Specification<T>
    {
        private readonly ISpecification<T> _specification;

        public NotSpecification(ISpecification<T> specification)
        {
            _specification = specification;
        }

        public override bool IsSatisfiedBy(T item)
            => !_specification.IsSatisfiedBy(item);

        public override Expression<Func<T, bool>> ToExpression()
        {
            var expr = _specification.ToExpression();
            var param = expr.Parameters[0];
            var body = Expression.Not(expr.Body);
            return Expression.Lambda<Func<T, bool>>(body, param);
        }
    }
}