using System;

namespace Convesys.Common.Tenancy.Tenancy
{
    public interface ITenantResolver<TSource,T> : ITenantResolver<T>
    {
        T ResolveTenant(TSource source);
    }

    public interface ITenantResolver<T>
    {
        T ResolveTenant(Func<T> next);
    }
}