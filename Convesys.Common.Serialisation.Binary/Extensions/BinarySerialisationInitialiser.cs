﻿using System;
using System.Diagnostics.CodeAnalysis;
using Convesys.Kernel.DependencyResolver;
using Convesys.Kernel.Serialisation;

namespace Convesys.Common.Serialisation.Binary.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class BinarySerialisationInitialiser
    {
        public static IDependencyResolver AddBinarySerialisation(this IDependencyResolver dependencyResolver)
        {
            if (dependencyResolver == null)
                throw new ArgumentNullException(nameof(dependencyResolver));

            if (!dependencyResolver.Contains<ISerialiser, BinarySerialiser>())
                dependencyResolver.RegisterType<ISerialiser, BinarySerialiser>(Lifetime.Transient);

            return dependencyResolver;
        }
    }
}
