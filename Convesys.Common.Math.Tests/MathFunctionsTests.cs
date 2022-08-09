using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Convesys.Common.Analytics.Tests
{
    internal class MathFunctionsTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task MeanTestSet1()
        {
            //Arrange
            var values = new List<double>() { 1, 2, 3, 4, 5, 6, 7 };
            
            //Execute
            var mean = await Statistics.Mean(values);
            var mean1 = MathNet.Numerics.Statistics.Statistics.Mean(values);
            //Assert
            Assert.AreEqual(mean, mean1);
            Assert.That(mean, Is.EqualTo(4));
            Assert.Pass();
        }

        [Test]
        public async Task MeanTestSet2()
        {
            //Arrange
            var values = new List<double> { 2, 7, 15, 4, 8 };

            //Execute
            var mean = await Statistics.Mean(values);
            var mean1 = MathNet.Numerics.Statistics.Statistics.Mean(values);
            //Assert
            Assert.AreEqual(mean, mean1);
            Assert.That(mean, Is.EqualTo(7.2));
            Assert.Pass();
        }

        [Test]
        public async Task MeanWhenNoneTest()
        {
            //Arrange
            var values = new List<double>();

            //Execute
            var mean = await Statistics.Mean(values);
            var mean1 = MathNet.Numerics.Statistics.Statistics.Mean(values);
            //Assert
            Assert.AreEqual(mean, mean1);
            Assert.That(mean, Is.EqualTo(double.NaN));
            Assert.Pass();
        }

        [Test]
        public async Task MeanWhenNullTest()
        {
            //Arrange
            var values = (IEnumerable<double>)null;

            //Execute
            
            //Assert
            Assert.ThrowsAsync<ArgumentNullException>(async() => await Statistics.Mean(values));
            Assert.Pass();
        }

        [Test]
        public async Task MedianWhenNullTest()
        {
            //Arrange
            var values = (IEnumerable<double>)null;

            //Execute

            //Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await Statistics.Median(values));
            Assert.Pass();
        }

        [Test]
        public async Task MedianWhenOddTest()
        {
            //Arrange
            var values = new List<double>() { 1, 2, 3, 4, 5, 6, 7 };

            //Execute
            var meadian = await Statistics.Median(values);
            var meadian1 = MathNet.Numerics.Statistics.Statistics.Median(values);
            //Assert
            Assert.AreEqual(meadian, meadian1);
            Assert.That(meadian, Is.EqualTo(4));
            Assert.Pass();
        }

        [Test]
        public async Task MedianWhenEvenTest()
        {
            //Arrange
            var values = new List<double>() { 1, 2, 3, 4, 5, 6 };

            //Execute
            var meadian = await Statistics.Median(values);
            var meadian1 = MathNet.Numerics.Statistics.Statistics.Median(values);
            //Assert
            Assert.AreEqual(meadian, meadian1);
            Assert.That(meadian, Is.EqualTo(3.5));
            Assert.Pass();
        }

        [Test]
        public async Task MedianWhenNoneTest()
        {
            //Arrange
            var values = new List<double>();

            //Execute
            var median = await Statistics.Median(values);
            var median1 = MathNet.Numerics.Statistics.Statistics.Median(values);
            //Assert
            Assert.AreEqual(median, median1);
            Assert.That(median, Is.EqualTo(double.NaN));
            Assert.Pass();
        }
    }
}
