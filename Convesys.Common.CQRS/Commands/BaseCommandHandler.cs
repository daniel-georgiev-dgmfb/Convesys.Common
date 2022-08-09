using Convesys.Common.CQRS.Messaging.Commands;
using Convesys.Common.CQRS.Messaging.Events;
using Convesys.Common.MessageHandling.MessageHandling;
using Convesys.Kernel.Data.ORM;
using Convesys.Kernel.Logging;
using Convesys.Kernel.Messaging.Dispatching;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Convesys.Common.CQRS.Commands
{
    public abstract class BaseCommandHandler<TMessage> : BaseMessageHandler<TMessage>
        where TMessage : BaseCommand
    {
        protected IDbContext _context;
        private readonly IMessageDispatcher _dispatcher;

        public BaseCommandHandler(IDbContext dbContext, IMessageDispatcher dispatcher, IEventLogger<BaseMessageHandler<TMessage>> logger) : base(logger)
        {
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
        }

        protected override async Task InvokeInternal(TMessage command, CancellationToken cancellationToken)
        {

            await this.HandleInternal(command, cancellationToken);
            var eventToPublish = this.BuildEvent(command);
            if (eventToPublish == null)
                throw new ArgumentNullException(nameof(eventToPublish));
            await this.PublishEvent(eventToPublish, cancellationToken);
        }
        
        protected virtual BaseEvent BuildEvent(TMessage message)
        {
            return new OnSuccessEvent(message.TenantId, Guid.NewGuid());
        }

        protected async Task PublishEvent(BaseEvent processedEvent, CancellationToken cancellationToken)
        {
            await this._dispatcher.SendMessage(processedEvent, cancellationToken);
        }

        protected override async Task<bool> OnError(Exception e, TMessage message)
        {
            try
            {
                var errorEvent = new OnExceptionEvent(message.TenantId, Guid.NewGuid());
                await this._dispatcher.SendMessage(errorEvent, CancellationToken.None);
                return true;
            }
            catch (Exception ex)
            {
                await base.OnError(ex, message);
                return true;
            }
        }

        protected abstract Task HandleInternal(TMessage command, CancellationToken cancellationToken);
    }
}