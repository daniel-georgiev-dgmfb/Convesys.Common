using Convesys.Common.Serialisation.JSON.SettingsProviders;
using Convesys.Kernel.DependencyResolver;
using Convesys.Kernel.Serialisation;
using Newtonsoft.Json;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Convesys.Common.Serialisation.JSON.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class JsonSerializerInitialiser
    {
        public static IDependencyResolver AddJsonSerialisation(this IDependencyResolver dependencyResolver)
        {
            if (dependencyResolver == null)
                throw new ArgumentNullException(nameof(dependencyResolver));

            if (!dependencyResolver.Contains<IJsonSerialiser, NSJsonSerializer>())
                dependencyResolver.RegisterType<IJsonSerialiser, NSJsonSerializer>(Lifetime.Transient);

            if (!dependencyResolver.Contains<ISerialisationSettingsProvider<JsonSerializerSettings>, DefaultSettingsProvider>())
                dependencyResolver.RegisterType<ISerialisationSettingsProvider<JsonSerializerSettings>, DefaultSettingsProvider>(Lifetime.Transient);
            return dependencyResolver;
        }
    }
}