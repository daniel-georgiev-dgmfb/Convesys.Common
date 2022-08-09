using System;
using Pirina.Common.CQRS.Messaging.Commands;

namespace Pirina.Common.CQRS.Tests.L0.TestMocks
{
    public class TestCommand : BaseCommand
    {
        public Action TestAction { get; set; }

        public TestCommand(Guid tenantId, Guid id, Guid correlationId, Action testAction) 
            : base(tenantId, id, correlationId)
        {
            TestAction = testAction;
        }
    }
}
