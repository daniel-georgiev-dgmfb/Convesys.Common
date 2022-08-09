using System;
using System.Threading;
using System.Threading.Tasks;
using Pirina.Common.CQRS.Commands;
using Pirina.Common.CQRS.Messaging.Events;
using Pirina.Common.MessageHandling.MessageHandling;
using Pirina.Kernel.Data.ORM;
using Pirina.Kernel.Logging;
using Pirina.Kernel.Messaging.Dispatching;

namespace Pirina.Common.CQRS.Tests.L0.TestMocks
{
    public class TestCommandHandler : BaseCommandHandler<TestCommand>
    {
        public bool CreateOnSuccessEvent { get; set; }

        public TestCommandHandler(IDbContext dbContext, IMessageDispatcher dispatcher, IGWLogger<BaseMessageHandler<TestCommand>> logger) 
            : base(dbContext, dispatcher, logger)
        {
        }

        protected override Task HandleInternal(TestCommand command, CancellationToken cancellationToken)
        {
            command.TestAction.Invoke();
            return Task.CompletedTask;
        }

        protected override BaseEvent BuildEvent(TestCommand message)
        {
            if (CreateOnSuccessEvent)
                return base.BuildEvent(message);
            return null;
        }
    }
}
