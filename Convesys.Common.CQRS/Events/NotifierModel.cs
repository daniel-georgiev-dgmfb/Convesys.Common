using System;
using System.Collections.Generic;
using System.Text;

namespace Convesys.Common.CQRS.Events
{
    public class NotifierModel
    {
        public Guid TenantId { get; set; }
        public Guid RequestId { get; set; }
        public bool Success { get; set; }
    }
}
