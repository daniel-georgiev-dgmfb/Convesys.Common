using System;
using System.Threading.Tasks;
using Convesys.Common.Tenancy.Tenancy;
using Convesys.Kernel.Tenancy;

namespace Convesys.Common.Tenancy.Tests.L0.MockData
{
    internal class MockTenantResolver : TenantResolver<MockSource>
    {
        private readonly MockSource _source;
        private readonly Func<TenantResolutionContext, Task> _func;

        public MockTenantResolver(MockSource source, Func<TenantResolutionContext, Task> func)
        {
            this._source = source;
            this._func = func;
        }

        public override TenantId<Guid> ResolveTenant(MockSource source)
        {
            throw new NotImplementedException();
        }

        protected override MockSource ResolveSource()
        {
            return this._source;
        }
    }
}