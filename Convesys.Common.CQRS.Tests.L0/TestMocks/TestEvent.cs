using System;
using Pirina.Common.CQRS.Messaging.Events;

namespace Pirina.Common.CQRS.Tests.L0.TestMocks
{
    public class TestEvent : BaseEvent
    {
        public Action TestAction { get; set; }

        public TestEvent(Guid tenantId, Guid id, Action testAction) 
            : base(tenantId, id)
        {
            TestAction = testAction;
        }
    }
}
