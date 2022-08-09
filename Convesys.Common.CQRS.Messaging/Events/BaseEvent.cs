using System;
using Convesys.Common.Messaging;

namespace Convesys.Common.CQRS.Messaging.Events
{
    [Serializable]
    public abstract class BaseEvent : TenantMessage
    {
        public BaseEvent(Guid tenantId, Guid id) : base(tenantId, id)
        {
        }
    }
}
