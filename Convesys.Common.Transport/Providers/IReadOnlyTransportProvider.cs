using System.Threading.Tasks;
using Convesys.Common.Transport.Context;

namespace Convesys.Common.Transport.Providers
{
    public interface IReadOnlyTransportProvider
    {
        Task Setup<TContext>(TContext context) where TContext : TransportProviderContext;
    }
}