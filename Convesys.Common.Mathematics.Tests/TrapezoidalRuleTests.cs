using NUnit.Framework;
using System;
using System.Threading.Tasks;
using static System.Math;

namespace Convesys.Common.Mathematics.Tests
{
    internal class TrapezoidalRuleTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Calculate_integral_function_of_x_x_x_compared_to_R()
        {
            //Arrange
            var resultFromR = 0.25;
            Func<double, double> func = x => x * x * x;

            ////Execute
            var resTr = await TrapezoidalRule.Run(func, 0, 1, 10000);
            var service = new TrapezoidalRule_Third_Party(new DoubleFunctionClass.DoubleFunction(x => Task.FromResult(x)));

            var r = service.CalculateIntegratelIn(0, 1).Result;

            //Assert
            Assert.That(Round(resultFromR, 2), Is.EqualTo(Round(resTr.Item1, 2)));
            //Assert.That(Round(resS, 4), Is.EqualTo(Round(resTr, 4)));
            Assert.Pass();
        }

        [Test]
        public async Task Calculate_integral_function_of_x_x_over_2_compared_to_R()
        {
            //Arrange
            var resultFromR = 0.25;
            Func<double, double> func = x => (x * x) / 2;

            ////Execute
            var resTr = await TrapezoidalRule.Run(func, 1, 4, 10000);
            var service = new TrapezoidalRule_Third_Party(new DoubleFunctionClass.DoubleFunction(x => Task.FromResult(x)));

            var r = service.CalculateIntegratelIn(1, 4).Result;

            //Assert
            Assert.That(Round(resultFromR, 2), Is.EqualTo(Round(resTr.Item1, 2)));
            //Assert.That(Round(resS, 4), Is.EqualTo(Round(resTr, 4)));
            Assert.Pass();
        }

        [Test]
        public async Task Calculate_integral_function_of_1x__compared_to_R()
        {
            //Arrange
            var resultFromR = 0.6931;
            Func<double, double> func = x => 1 / x;

            ////Execute
            var res = await TrapezoidalRule.Run(func, 1, 2, 10000);
            var service = new TrapezoidalRule_Third_Party(new DoubleFunctionClass.DoubleFunction(x => Task.FromResult(x)));

            var r = service.CalculateIntegratelIn(1, 2).Result;

            //Assert
            Assert.That(Round(res.Item1, 4), Is.EqualTo(resultFromR));
            Assert.Pass();
        }

        [Test]
        public async Task Calculate_integral_function_of_x__compared_to_R()
        {
            //Arrange
            var resultFromR = 49.5;
            Func<double, double> func = x => x;
            
            ////Execute
            var res = await TrapezoidalRule.Run(func, 1, 10, 10000);
            var service = new TrapezoidalRule_Third_Party(new DoubleFunctionClass.DoubleFunction(x => Task.FromResult(x)));
            
            var r = service.CalculateIntegratelIn(1, 10).Result;
            
            //Assert
            Assert.That(Round(res.Item1, 1), Is.EqualTo(resultFromR));
            Assert.Pass();
        }

        [Test]
        public async Task Calculate_integral_function_of_x_plus_5__compared_to_R()
        {
            //Arrange
            Func<double, double> func = x => x + 5;
            var resultFromR = 94.50;
            
            //Execute
            var res = TrapezoidalRule.Run(func, 1, 10).Result;

            //Assert
            Assert.That(Round(res.Item1, 1), Is.EqualTo(resultFromR));
            Assert.Pass();
        }

        [Test]
        public async Task Calculate_integral_function_of_x_minus_5__compared_to_R()
        {
            //Arrange
            Func<double, double> func = x => x - 5;
            var resultFromR = 4.50;

            //Execute
            var res = TrapezoidalRule.Run(func, 1, 10).Result;

            //Assert
            Assert.That(Round(res.Item1, 1), Is.EqualTo(resultFromR));
            Assert.Pass();
        }

        [Test]
        public async Task Calculate_integral_function_of_x_plus_5_sqr_compared_to_R()
        {
            //Arrange
            var resultFromR = 28.93;
            Func<double, double> func = x => System.Math.Sqrt(x + 5);
            //Execute
            var res = TrapezoidalRule.Run(func, 1, 10).Result;
            //Assert
            Assert.That(Round(res.Item1, 2), Is.EqualTo(resultFromR));
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
            var res = TrapezoidalRule.Run(func, -1.96, 1.96).Result;
            
            //Assert
            Assert.That(Round(res.Item1, 2), Is.EqualTo(Round(resultFromR, 2)));
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