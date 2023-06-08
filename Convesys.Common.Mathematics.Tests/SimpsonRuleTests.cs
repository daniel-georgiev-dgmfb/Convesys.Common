using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Twilight.Platform.Common.Mathematics;
using static System.Math;

namespace Twilight.Common.Mathematics.Tests
{
    internal class SimpsonRuleTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Calculate_integral_function_of_x_3_compared_to_R()
        {
            //Arrange
            var resultFromR = 0.25;
            Func<double, double> func = x => x * x * x;

            ////Execute
            var resS = await SimpsonRule.Run(func, 0, 1, 10000);
            var resTr = await TrapezoidalRule.Run(func, 0, 1, 10000);
            var service = new TrapezoidalRule_Third_Party(new DoubleFunctionClass.DoubleFunction(x => Task.FromResult(x)));

            var r = service.CalculateIntegratelIn(0, 1).Result;

            //Assert
            Assert.That(Round(resultFromR, 4), Is.EqualTo(Round(resTr.Item1, 4)));
            //Assert.That(Round(resS, 4), Is.EqualTo(Round(resTr, 4)));
            Assert.Pass();
        }

        [Test]
        public async Task Calculate_integral_function_of_x_x_over_2_compared_to_R()
        {
            //Arrange
            var resultFromR = 8.14594;
            Func<double, double> func = x => (x * x) / 2;

            ////Execute
            var resS = await SimpsonRule.Run(func, 1, 4, 10000);
            var resTr = await TrapezoidalRule.Run(func, 1, 4, 10000);
            var resTrAsync = await TrapezoidalRule.Run(func, 1, 4, 10000);
            var service = new TrapezoidalRule_Third_Party(new DoubleFunctionClass.DoubleFunction(x => Task.FromResult(x)));

            var r = service.CalculateIntegratelIn(1, 4).Result;

            //Assert
            Assert.That(Round(resultFromR, 4), Is.EqualTo(Round(resTr.Item1, 4)));
            //Assert.That(Round(resS, 4), Is.EqualTo(Round(resTr, 4)));
            Assert.Pass();
        }

        [Test]
        public async Task Calculate_integral_function_of_1_over_x_compared_to_R()
        {
            //Arrange
            var resultFromR = 0.6931472;
            Func<double, double> func = x => 1 / x;

            ////Execute
            var resS = await SimpsonRule.Run(func, 1, 2, 10000);
            var resTr = await TrapezoidalRule.Run(func, 1, 2, 10000);
            var service = new TrapezoidalRule_Third_Party(new DoubleFunctionClass.DoubleFunction(x => Task.FromResult(x)));

            var r = service.CalculateIntegratelIn(1, 2).Result;

            //Assert
            Assert.That(Round(resultFromR, 4), Is.EqualTo(Round(resTr.Item1, 4)));
            //Assert.That(Round(resS, 4), Is.EqualTo(Round(resTr, 4)));
            Assert.Pass();
        }

        [Test]
        public async Task Calculate_integral_function_of_x__compared_to_R()
        {
            //Arrange
            var resultFromR = 0.69314;
            Func<double, double> func = x => 1 / x;
            
            ////Execute
            //var resS = await SimpsonRule.Run(func, 1, 2, 10000);
            var resTr = await TrapezoidalRule.Run(func, 1, 2, 10000);
            var service = new TrapezoidalRule_Third_Party(new DoubleFunctionClass.DoubleFunction(x => Task.FromResult(x)));
            
            var r = service.CalculateIntegratelIn(1, 4).Result;

            //Assert
            Assert.That(Round(resultFromR, 4), Is.EqualTo(Round(resTr.Item1, 4)));
            //Assert.That(Round(resS, 4), Is.EqualTo(Round(resTr, 4)));
            Assert.Pass();
        }

        [Test]
        public async Task Calculate_integral_function_of_x_plus_5__compared_to_R()
        {
            //Arrange
            Func<double, double> func = x => x + 5;
            var resultFromR = 94.50;
            
            //Execute
            var res = SimpsonRule.Run(func, 1, 10).Result;

            //Assert
            Assert.That(Round(res, 1), Is.EqualTo(resultFromR));
            Assert.Pass();
        }

        [Test]
        public async Task Calculate_integral_function_of_x_minus_5__compared_to_R()
        {
            //Arrange
            Func<double, double> func = x => x - 5;
            var resultFromR = 4.50;

            //Execute
            var res = SimpsonRule.Run(func, 1, 10).Result;

            //Assert
            Assert.That(Round(res, 1), Is.EqualTo(resultFromR));
            Assert.Pass();
        }

        [Test]
        public async Task Calculate_integral_function_of_x_plus_5_sqr_compared_to_R()
        {
            //Arrange
            var resultFromR = 28.93;
            Func<double, double> func = x => System.Math.Sqrt(x + 5);
            //Execute
            var res = SimpsonRule.Run(func, 1, 10).Result;
            //Assert
            Assert.That(Round(res, 2), Is.EqualTo(resultFromR));
            Assert.Pass();
        }

        [Test]
        public async Task Calculate_integral_function_of_x_exponential_compared_to_R()
        {
            //Arrange
            var resultFromR = 0.9500042;
            Func<double, double> func = x => (1 / Sqrt(2 * Math.PI)) * Exp((x * x) * -1 / 2);
            
            //Execute
            var service = new TrapezoidalRule_Third_Party(new DoubleFunctionClass.DoubleFunction(x => Task.FromResult(1 / Sqrt(2 * Math.PI) * Exp((x * x) / 2))));
            var res = SimpsonRule.Run(func, -1.96, 1.96).Result;
            
            //Assert
            Assert.That(Round(res, 2), Is.EqualTo(Round(resultFromR, 2)));
            Assert.Pass();
        }

        [Test]
        public async Task Cumulative_distribution_function_compared_to_R()
        {
            //Arrange
            var resultFromR = 0.9500042;
            //Func<double, double> func = x => (1 / Sqrt(2 * Math.PI)) * Exp((x * x) * -1 / 2);
            Func<double, double> func = x => Exp((x * x) / (double)-2) / Sqrt(2 * Math.PI);

            //Execute
            //var service = new TrapezoidalRule_Third_Party(new DoubleFunctionClass.DoubleFunction(x => Task.FromResult(1 / Sqrt(2 * Math.PI) * Exp((x * x) / 2))));
            var res1 = TrapezoidalRule.Run(func, Int32.MinValue, -1.96, 10000000).Result;
            var res = SimpsonRule.Run(func, -1.96, Int32.MinValue, 10000000).Result;

            //Assert
            Assert.That(Round(res, 2), Is.EqualTo(Round(resultFromR, 2)));
            Assert.Pass();
        }

        #region Obsolete
        [Test]
        public async Task Calculate_integral_TrapezoidalRule_Test_f_x()
        {
            //Arrange
            var result = 0d;
            var a = 0d;
            var b = 100d;
            var n = 10000;
            var f = (Func<double, double>)(x => x);
            //Execute
            var dx = (b - a) / n;
            var dxhalf = dx / 2;
            var f0 = f(a);
            var fn = f(b);
            for (var i = 1; i < n - 1; i++)
            {
                var xi = a + (i * dx);
                result = result + (2 * f(xi));
            }
            result += (f0 + fn);

            result *= dxhalf;
        }

        [Test]
        public async Task Calculate_integral_TrapezoidalRule_Test_f_x_plus5()
        {
            //Arrange
            var result = 0d;
            var a = 1d;
            var b = 100d;
            var n = 1;
            var f = (Func<double, double>)(x => x + 5);
            //Execute
            var dx = (b - a) / n;
            var dxhalf = dx / 2;
            var f0 = f(a);
            var fn = f(b);
            for (var i = 1; i < n - 1; i++)
            {
                var xi = a + (i * dx);
                result += f(xi);
            }
            result += (f0 + fn);
            result *= dxhalf;
            //var func = new DoubleFunctionClass();
            //var func = new DoubleFunctionClass.DoubleFunction(x => Task.FromResult(x + 5.00));
            //var tr = new TrapezoidalRule(func);

            //var r1 = tr.CalculateIntegratelIn(1, 100).Result;
        }
        #endregion
    }
}