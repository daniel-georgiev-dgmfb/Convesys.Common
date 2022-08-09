using System;
using Convesys.Common.Messaging;

namespace Convesys.Common.CQRS.Messaging.Commands
{
    [Serializable]
    public class BaseCommand : TenantMessage
    {
        public BaseCommand(Guid tenantId, Guid id, Guid correlationId) : base(id, tenantId)
        {
        }
    }
}