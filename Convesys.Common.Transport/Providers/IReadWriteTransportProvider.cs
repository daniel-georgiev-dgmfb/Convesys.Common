using System.Threading.Tasks;
using Convesys.Common.Transport.Context;
using Convesys.Kernel.Transport;

namespace Convesys.Common.Transport.Providers
{
    public interface IReadWriteTransportProvider
    {
        Task Setup<TContext>(TContext context) where TContext : TransportProviderContext;
        Task<ITransportDispatcher> GetDispatcher<TContext>(TContext context) where TContext : TransportProviderContext;
    }
}