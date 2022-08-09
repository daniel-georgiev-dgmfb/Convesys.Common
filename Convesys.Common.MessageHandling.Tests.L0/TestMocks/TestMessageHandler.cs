using System;
using System.Threading;
using System.Threading.Tasks;
using Convesys.Common.MessageHandling.MessageHandling;
using Convesys.Kernel.Logging;

namespace Convesys.Common.MessageHandling.Tests.L0.TestMocks
{
    public class TestMessageHandler : BaseMessageHandler<TestMessage>
    {
        private readonly Action _action;
        private readonly bool _throwException;

        public TestMessage ReceivedMessage { get; private set; }

        public TestMessageHandler(Action action, IEventLogger<BaseMessageHandler<TestMessage>> logger, bool throwException)
            : base(logger)
        {
            _action = action;
            _throwException = throwException;
        }

        protected override Task InvokeInternal(TestMessage message, CancellationToken cancellationToken)
        {
            ReceivedMessage = message;
            _action();
            return Task.CompletedTask;
        }

        protected override async Task<bool> OnError(Exception e, TestMessage message)
        {
            await base.OnError(e, message);
            return _throwException;
        }
    }
}