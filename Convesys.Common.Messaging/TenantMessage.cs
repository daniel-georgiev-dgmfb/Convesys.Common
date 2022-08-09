using System;
using Convesys.Kernel.Messaging;

namespace Convesys.Common.Messaging
{
    [Serializable]
    public class TenantMessage : Message
    {
        public Guid TenantId { get; }

        public TenantMessage(Guid transactionId, Guid tenantId) : base(transactionId)
        {
            TenantId = tenantId;
        }
    }
}