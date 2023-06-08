using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using Twilight.Platform.Common.Mathematics;

namespace Twilight.Common.Mathematics.Tests
{
    public class EuclideanDistanceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task TwoPoint2DCalucalteEuclideanDistanceTest()
        {
            //Arrange
            var point1 = new List<double>() { 1, 2 };
            var point2 = new List<double>() { 3, 4 };
            //Execute
            var ed = await Spatial.EuclideanDistance(point1, point2);
            //Assert
            Assert.That(ed, Is.EqualTo(System.Math.Sqrt(8)));
            Assert.Pass();
        }

        [Test]
        public async Task TwoPoint3DCalucalteEuclideanDistanceTest()
        {
            //Arrange
            var point1 = new List<double>() { 7, 4, 3 };
            var point2 = new List<double>() { 17, 6, 2 };
            //Execute
            var ed = await Spatial.EuclideanDistance(point1, point2);
            //Assert
            Assert.That(ed, Is.EqualTo(System.Math.Sqrt(105)));
            Assert.Pass();
        }

        [Test]
        public async Task TwoPoint4DCalucalteEuclideanDistanceTest()
        {
            //Arrange
            var point1 = new List<double>() { 7, 4, 3, 2 };
            var point2 = new List<double>() { 17, 6, 2, 1 };
            //Execute
            var ed = await Spatial.EuclideanDistance(point1, point2);
            //Assert
            Assert.That(ed, Is.EqualTo(System.Math.Sqrt(106)));
            Assert.Pass();
        }

        [Test]
        public async Task TwoPoint2DCalucalteEuclideanDistanceTestNegatives()
        {
            //Arrange
            var point1 = new List<double>() { 1, 2 };
            var point2 = new List<double>() { -3, 4 };
            //Execute
            var ed = await Spatial.EuclideanDistance(point1, point2);
            //Assert
            Assert.That(System.Math.Round(ed,3), Is.EqualTo(4.472));
            Assert.Pass();
        }

        [Test]
        public async Task TwoPoint2DCalucalteEuclideanDistanceTestTwoNegatives()
        {
            //Arrange
            var point1 = new List<double>() { 1, 2 };
            var point2 = new List<double>() { -3, -4 };
            //Execute
            var ed = await Spatial.EuclideanDistance(point1, point2);
            //Assert
            Assert.That(System.Math.Round(ed, 3), Is.EqualTo(7.211));
            Assert.Pass();
        }

        [Test]
        public async Task TwoPoint2DCalucalteEuclideanDistanceTestTwoNegatives1()
        {
            //Arrange
            var point1 = new List<double>() { -1, -2 };
            var point2 = new List<double>() { 3, 4 };
            //Execute
            var ed = await Spatial.EuclideanDistance(point1, point2);
            //Assert
            Assert.That(System.Math.Round(ed, 3), Is.EqualTo(7.211));
            Assert.Pass();
        }

        [Test]
        public async Task TwoPoint2DCalucalteEuclideanDistanceTestNegativesAll()
        {
            //Arrange
            var point1 = new List<double>() { -1, -2 };
            var point2 = new List<double>() { -3, -4 };
            //Execute
            var ed = await Spatial.EuclideanDistance(point1, point2);
            //Assert
            Assert.That(System.Math.Round(ed, 3), Is.EqualTo(2.828));
            Assert.Pass();
        }
    }
}