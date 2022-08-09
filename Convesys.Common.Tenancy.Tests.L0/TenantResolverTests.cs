using System;
using System.Threading.Tasks;
using Convesys.Common.Tenancy.Tests.L0.MockData;
using Convesys.Kernel.Tenancy;
using Convesys.Kernel.Web.Authorisation;
using Moq;
using NUnit.Framework;

namespace Convesys.Common.Tenancy.Tests.L0
{
    [TestFixture]
    [Category("Convesys.Common.Tenancy.Tests.L0")]
    public class TenantResolverTests
    {
        [Test]
        public async Task Resolve_tenant()
        {
            throw new NotImplementedException();
            ////ARRANGE
            //var clientCredenials = new Mock<IBearerTokenContext>();
            //var context = new TenantResolutionContext(new Kernel.Web.Endpoint("https://localhost/"), clientCredenials.Object);
            //var tenantId = Guid.NewGuid();
            //var source = new MockSource(tenantId);
            //Func<TenantResolutionContext, Task> contextFunc = c =>
            //{
            //    c.Resolved(new TenantDescriptor(tenantId));
            //    return Task.CompletedTask;
            //};
            //var resolver = new MockTenantResolver(source, contextFunc);
            ////ACT
            //await resolver.ResolveTenant(context, c => Task.CompletedTask);
            ////ASSERT
            
            //Assert.IsTrue(context.IsResolved);
            //Assert.IsNotNull(context.TenantDescriptor);
            //Assert.AreEqual(tenantId, context.TenantDescriptor.TenantId);
        }

        [Test]
        public async Task Cant_Resolve_tenant()
        {
            throw new NotImplementedException();
            ////ARRANGE
            //var clientCredenials = new Mock<IBearerTokenContext>();
            //var context = new TenantResolutionContext(new Kernel.Web.Endpoint("https://localhost/"), clientCredenials.Object);
            //var tenantId = Guid.NewGuid();
            //var source = new MockSource(tenantId);
            //Func<TenantResolutionContext, Task> contextFunc = c =>
            //{
            //    return Task.CompletedTask;

            //};
            //var resolver = new MockTenantResolver(source, contextFunc);
            
            ////ACT
            //await resolver.ResolveTenant(context, c => Task.CompletedTask);
            
            ////ASSERT
            //Assert.IsFalse(context.IsResolved);
            //Assert.IsNull(context.TenantDescriptor);
        }
    }
}
