using System;

namespace Convesys.Common.CQRS.Messaging.Events
{
    [Serializable]
    public class OnExceptionEvent : BaseEvent
    {
        public OnExceptionEvent(Guid tenantId, Guid id) : base(tenantId, id)
        {
        }
    }
}