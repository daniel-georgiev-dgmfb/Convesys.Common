using System;
using System.Diagnostics.CodeAnalysis;
using Twilight.Kernel.DependencyResolver;

namespace Convesys.Common.Logging
{
    [ExcludeFromCodeCoverage]
    public class LoggingInitialiser
    {
        [Obsolete("The ConsoleLoggingExtensions should be used instead", true)]
        protected IDependencyResolver InitialiseInternal(IDependencyResolver dependencyResolver)
        {
            return dependencyResolver;
        }
    }
}
