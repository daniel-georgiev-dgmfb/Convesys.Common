﻿using System;

namespace Convesys.Common.Tenancy.Tests.L0.MockData
{
    internal class MockSource
    {
        public Guid Id { get; }
        public MockSource(Guid id)
        {
            this.Id = id;
        }
    }
}