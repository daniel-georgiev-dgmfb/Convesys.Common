using System;

namespace Convesys.Common.Tenancy.Tenancy
{
    /// <summary>
    /// Base class for tenant resolver
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    public abstract class TenantResolver<TSource> : ITenantResolver<TSource, TenantId<Guid>>
    {
        public TenantId<Guid> ResolveTenant(Func<TenantId<Guid>> next)
        {
            var source = this.ResolveSource();
            if (source == null)
                return next();
            var tenant = this.ResolveTenant(source);
            if (tenant != null)
                return tenant;
            return next();
        }

        public abstract TenantId<Guid> ResolveTenant(TSource source);

        protected abstract TSource ResolveSource();
    }
}