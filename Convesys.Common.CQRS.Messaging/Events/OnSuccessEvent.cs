using System;

namespace Convesys.Common.CQRS.Messaging.Events
{
    [Serializable]
    public class OnSuccessEvent : BaseEvent
    {
        public OnSuccessEvent(Guid tenantId, Guid id) : base(tenantId, id)
        {
        }
    }
}