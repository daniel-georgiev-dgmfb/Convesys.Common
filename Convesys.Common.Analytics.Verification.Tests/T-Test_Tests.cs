using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Convesys.Common.Analytics.Verification.Tests
{
    public class T_Test_Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ShouldThrow_argument_null_exception_actual()
        {
            //Arrange
            var estimated = new List<double> { 1.1, 1.2, 1.0, 1.4 };
            var actual = (List<double>)null;
            //Execute
            Assert.ThrowsAsync<ArgumentNullException>(new AsyncTestDelegate(async () => await TTest.Run(estimated, actual)));
            //Assert
        }

        [Test]
        public async Task ShouldThrow_argument_null_exception_estimated()
        {
            //Arrange
            var actual = new List<double> { 1.1, 1.2, 1.0, 1.4 };
            var estimated = (List<double>)null;
            //Execute
            Assert.ThrowsAsync<ArgumentNullException>(new AsyncTestDelegate(async () => await TTest.Run(estimated, actual)));
            //Assert
        }

        [Test]
        public async Task NoDifference_should_be_0()
        {
            //Arrange
            var estimated = new List<double> { 1.1, 1.2, 1.0, 1.4 };
            var actual = new List<double> { 1.1, 1.2, 1.0, 1.4 };
            //Execute
            var result = await TTest.Run(estimated, actual);
            //Assert
            Assert.AreEqual(0, result);
            Assert.Pass();
        }

        [Test]
        public async Task Difference_should_be_negative()
        {
            //Arrange
            var estimated = new List<double> { 1.1, 1.2, 1.9, 1.4 };
            var actual = new List<double> { 1.1, 1.2, 1.0, 1.4 };
            //Execute
            var result = await TTest.Run(estimated, actual);
            //Assert
            Assert.Less(result, 0);
            Assert.Pass();
        }

        [Test]
        public async Task Difference_should_be_positive()
        {
            //Arrange
            var estimated = new List<double> { 1.1, 1.2, 1.0, 1.4 };
            var actual = new List<double> { 1.1, 1.2, 1.9, 1.4 };
            //Execute
            var result = await TTest.Run(estimated, actual);
            //Assert
            Assert.Greater(result, 0);
            Assert.Pass();
        }

        [Test]
        public async Task Count_mismatch_should_throw_an_exception()
        {
            //Arrange
            var estimated = new List<double> { 1.1, 1.2, 1.0, 1.4 };
            var actual = new List<double> { 1.1, 1.2, 190, 1.4, 1 };
            //Execute
            Assert.ThrowsAsync<ArgumentException>(async() => await TTest.Run(estimated, actual));
            //Assert
        }
    }
}