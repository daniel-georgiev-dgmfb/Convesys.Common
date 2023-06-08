using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twilight.Platform.Common.Mathematics.Tests
{
    internal class CenteredFivePointTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task CenteredFivePointsTestX2()
        {
            //Arrange
            Func<double, double> func = x => x * x;

            ////Execute
            var res = await CenteredFivePoint.Run(func, 2);

            //Assert
            Assert.That(Math.Round(res, 4), Is.EqualTo(4));
            Assert.Pass();
        }
    }
}
