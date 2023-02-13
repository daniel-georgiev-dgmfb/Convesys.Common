using Convesys.Common.Mathematics;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Platform.Common.Mathematics.Tests
{
    public class LocationTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task LcationTest_L0()
        {
            //Arrange
            //var x1 = Random.Shared.NextInt64(-150, -80);
            //var y1 = Random.Shared.NextInt64(-150, 150);
            //var x2 = Random.Shared.NextInt64(80, 150);
            //var y2 = Random.Shared.NextInt64(20, 150);
            //var x3 = Random.Shared.NextInt64(80, 150);
            //var y3 = Random.Shared.NextInt64(-150, -20);
            //var x = Random.Shared.NextInt64(-60, 60);
            //var y = Random.Shared.NextInt64(-60, 60);
            var x1 = -100L;
            var y1 = 110L;
            var x2 = 110L;
            var y2 = 60L;
            var x3 = 90L;
            var y3 = -120L;
            var x = -10L;
            var y = 30L;
            var r1 = Math.Pow((x - x1) * (x - x1) + (y - y1) * (y - y1), 0.5);
            var r2 = Math.Pow((x - x2) * (x - x2) + (y - y2) * (y - y2), 0.5);
            var r3 = Math.Pow((x - x3) * (x - x3) + (y - y3) * (y- y3), 0.5);
            var tuple1 = Tuple.Create(x1, y1, r1);
            var tuple2 = Tuple.Create(x2, y2, r2);
            var tuple3 = Tuple.Create(x3, y3, r3);
            
            //Execute
            var location = await Spatial.GetLocation(tuple1, tuple2, tuple3);
            
            //Assert
            Assert.AreEqual(-10.00, Math.Round(location.Item1, 2));
            Assert.AreEqual(30.00, Math.Round(location.Item2, 2));
        }
    }
}