using System;
using System.Diagnostics.CodeAnalysis;
using Convesys.Kernel.DependencyResolver;
using Convesys.Kernel.Messaging.Dispatching;

namespace Convesys.Common.CQRS.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class CQRSExtensions
    {
        public static IDependencyResolver AddCQRSDispatcher(this IDependencyResolver dependencyResolver)
        {
            if (dependencyResolver == null)
                throw new ArgumentNullException(nameof(dependencyResolver));

            if(!dependencyResolver.Contains<IMessageDispatcher, MessageDispatcher>())
                dependencyResolver.RegisterType<IMessageDispatcher, MessageDispatcher>(Lifetime.Transient);

            return dependencyResolver;
        }
    }
}