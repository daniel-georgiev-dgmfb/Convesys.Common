using System;
using Convesys.Kernel.Messaging;

namespace Convesys.Common.MessageHandling.Tests.L0.TestMocks
{
    public class TestMessage : Message
    {
        public TestMessage(Guid id, Guid correlationId)
            : base(id)
        {
        }
    }
}