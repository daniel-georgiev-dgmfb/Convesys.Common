using System.Diagnostics;
using Twilight.Platform.Common.Location;

namespace Platform.Common.Mathematics.Tests
{
    public class LocationTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task LocationTest_L0()
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
            var location = await new LocationService().GetLocation(tuple1, tuple2, tuple3);
            
            //Assert
            Assert.That(-10.00 == Math.Round(location.Item1, 2));
            Assert.That(30.00 == Math.Round(location.Item2, 2));
        }

        [Test]
        public async Task DistanceTest_L0()
        {
            //Arrange
            var tuple1 = Tuple.Create(41.507483f, -99.436554f);
            var tuple2 = Tuple.Create(38.504048f, -98.315949f);
            var service = new LocationService();
            //Execute
            var distance = await service.CalculateDistance(tuple1, tuple2);
            var distance1 = await service.DistanceInKmBetweenEarthCoordinates(41.507483f, -99.436554f, 38.504048f, -98.315949f);
            Debug.Print(distance.ToString());
            //Assert
            Assert.That(347.3 == Math.Round(distance, 2));
        }

        [Test]
        public async Task DistanceTest_L0_v1()
        {
            //Arrange
            var tuple1 = Tuple.Create(41.507483f, -99.436554f);
            var tuple2 = Tuple.Create(38.504048f, -98.315949f);
            var service = new LocationService();
            //Execute
            var distance = await service.CalculateDistance(tuple1, tuple2);
            var distance1 = await service.DistanceInKmBetweenEarthCoordinates(tuple1.Item1, tuple1.Item2, tuple2.Item1, tuple2.Item2);
            var distance1_1 = await service.DistanceInKmBetweenEarthCoordinates(tuple1, tuple2);
            Debug.Print(distance.ToString());
            //Assert
            Assert.That(347.3 == Math.Round(distance, 2));
        }

        [Test]
        public async Task DistanceTest1_L0()
        {
            //Arrange
            var tuple1 = Tuple.Create(51.5f, 0f);
            var tuple2 = Tuple.Create(38.8f, -77.1f);
            var service = new LocationService();
            //Execute
            var distance = await service.CalculateDistance(tuple1, tuple2);
            var distance1 = await service.DistanceInKmBetweenEarthCoordinates(tuple1.Item1, tuple1.Item2, tuple2.Item1, tuple2.Item2);
            Debug.Print(distance.ToString());
            //Assert
            Assert.That(347.3 == Math.Round(distance, 2));
        }
    }
}