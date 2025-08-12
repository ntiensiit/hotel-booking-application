using System.Data;
using System.Data.Common;
using NHibernate;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;

namespace Adapter.Driven.NHibernate.Converters;

public class DateOnlyUserType : IUserType
{
    public new bool Equals(object x, object y)
    {
        return ReferenceEquals(x, y) || x.Equals(y);
    }

    public int GetHashCode(object x)
    {
        return x.GetHashCode();
    }

    public object NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner)
    {
        var obj = NHibernateUtil.Date.NullSafeGet(rs, names[0], session, owner);
        return obj == null ? default : DateOnly.FromDateTime((DateTime)obj);
    }

    public void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
    {
        var parameter = cmd.Parameters[index];

        switch (value)
        {
            case null:
            case DBNull:
                parameter.Value = DBNull.Value;
                break;
            default:
                var dateOnly = (DateOnly)value;
                parameter.Value = dateOnly.ToDateTime(TimeOnly.MinValue);
                break;
        }
    }

    public object DeepCopy(object value)
    {
        return value;
    }

    public object Replace(object original, object target, object owner)
    {
        return original;
    }

    public object Assemble(object cached, object owner)
    {
        return cached;
    }

    public object Disassemble(object value)
    {
        return value;
    }

    public SqlType[] SqlTypes => new[] { new SqlType(DbType.Date) };
    public Type ReturnedType => typeof(DateOnly);
    public bool IsMutable => false;
}