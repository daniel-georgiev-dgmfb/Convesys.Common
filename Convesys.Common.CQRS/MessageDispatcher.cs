using System;
using System.Threading;
using System.Threading.Tasks;
using Convesys.Common.Transport.Context;
using Convesys.Common.Transport.Providers;
using Convesys.Kernel.Messaging;
using Convesys.Kernel.Messaging.Dispatching;
using Convesys.Kernel.Transport;

namespace Convesys.Common.CQRS
{
    public class MessageDispatcher : IMessageDispatcher
    {
        private ITransportDispatcher _dispatcher;
        private readonly Func<TransportProviderContext> _queueFactory;
        private readonly IWriteOnlyTransportProvider _writeOnlyTransportProvider;

        public MessageDispatcher(IWriteOnlyTransportProvider writeOnlyTransportProvider, Func<TransportProviderContext> queueFactory)
        {
            _writeOnlyTransportProvider = writeOnlyTransportProvider ?? throw new ArgumentNullException(nameof(writeOnlyTransportProvider));
            _queueFactory = queueFactory ?? throw new ArgumentNullException(nameof(queueFactory));
        }

        public async Task SendMessage(Message message, CancellationToken cancallationToken)
        {
            var dispatcher = await GetDispatcher();
            await dispatcher.SendMessage(message, cancallationToken);
        }

        private async Task<ITransportDispatcher> GetDispatcher()
        {
            return _dispatcher ?? (_dispatcher = await _writeOnlyTransportProvider.GetDispatcher(_queueFactory()));
        }
    }
}