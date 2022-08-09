using System;
using System.Threading;
using System.Threading.Tasks;
using Convesys.Common.CQRS.Messaging.Events;
using Convesys.Common.MessageHandling.MessageHandling;
using Convesys.Kernel.Logging;

namespace Convesys.Common.CQRS.Events
{
    public abstract class BaseEventMessageHandler<TMessage> : BaseMessageHandler<TMessage>
        where TMessage : BaseEvent
    {
        private readonly INotifier _notifier;
        
        protected BaseEventMessageHandler(INotifier notifier, IEventLogger<BaseMessageHandler<TMessage>> logger):base(logger)
        {
            _notifier = notifier ?? throw new ArgumentNullException(nameof(notifier));
        }

        protected override async Task InvokeInternal(TMessage message, CancellationToken cancellationToken)
        {
            await _notifier.Notify();
        }
    }
}