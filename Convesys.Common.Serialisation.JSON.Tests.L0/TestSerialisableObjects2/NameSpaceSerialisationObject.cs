﻿using System;

namespace Convesys.Common.Serialisation.JSON.Tests.L0.TestSerialisableObjects2
{
    [Serializable]
    public class NameSpaceSerialisationObject
    {
        public string NameSpace => "TestMocks2";
        public string Extra => "Extra";
    }
}
