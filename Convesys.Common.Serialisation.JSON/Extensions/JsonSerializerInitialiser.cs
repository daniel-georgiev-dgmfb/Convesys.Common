
using Newtonsoft.Json;
using System;
using System.Diagnostics.CodeAnalysis;
using Twilight.Common.Serialisation.JSON.SettingsProviders;
using Twilight.Kernel.DependencyResolver;
using Twilight.Kernel.Serialisation;

namespace Twilight.Common.Serialisation.JSON.Extensions
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