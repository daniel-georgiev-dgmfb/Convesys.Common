using Convesys.Common.Analytics.PythonDotNet;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Convesys.Common.Analytics.Tests
{
    internal class StatisticFunctionTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task SampleVarianceTests()
        {
            //Arrange
            var readings = new List<double> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //Execute
            var variance = await Statistics.Variance(readings);
            var variance1 = MathNet.Numerics.Statistics.Statistics.Variance(readings);
            //Assert
            Assert.AreEqual(variance, variance1);
            Assert.That(System.Math.Round(variance,3), Is.EqualTo(System.Math.Round(9.16666667,3)));
        }

        [Test]
        public async Task PopulationVarianceTests()
        {
            //Arrange
            var readings = new List<double> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //Execute
            var variance = await Statistics.Variance(readings, false);
            var variance1 = MathNet.Numerics.Statistics.Statistics.PopulationVariance(readings);
            //Assert
            Assert.That(System.Math.Round(variance, 3), Is.EqualTo(System.Math.Round(8.25, 3)));
            Assert.AreEqual(variance, variance1);
        }

        [Test]
        public async Task StandardDeviationTests()
        {
            //Arrange
            var readings = new List<double> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //Execute
            var standardDeviation = await Statistics.StandardDeviation(readings);
            var dev = MathNet.Numerics.Statistics.Statistics.StandardDeviation(readings);
            //Assert
            Assert.AreEqual(standardDeviation, dev);
            Assert.That(System.Math.Round(standardDeviation, 3), Is.EqualTo(System.Math.Round(3.0276504, 3)));
        }

        [Test]
        public async Task SkewnessTests()
        {
            //Arrange
            var readings = new List<double> { 3, 4, 5, 2, 3, 4, 5, 6, 4, 7 };
            //var readings = new List<double> { 3, 3, 5 };
            //Execute
            var skewness = await Statistics.Skewness(readings);
            var skewnessExcel = await Statistics.SkewnessExcel(readings);
            var m = MathNet.Numerics.Statistics.Statistics.Mean(readings);
            var m1 = await Statistics.Mean(readings);
            var sd = await Statistics.StandardDeviation(readings);
            var sd1 = MathNet.Numerics.Statistics.Statistics.StandardDeviation(readings);
            var rs = new Convesys.Common.Analytics.Tests.RunningStatistics(readings);
            var s = rs.Skewness;
            var skewness1 = MathNet.Numerics.Statistics.Statistics.Skewness(readings);
            //Assert
            Assert.AreEqual(skewness1, skewness);
            Assert.That(System.Math.Round(skewness, 3), Is.EqualTo(System.Math.Round(2.495117, 3)));
        }

        [Test]
        public async Task SkewnessTests1()
        {
            //Arrange
            var readings = new List<double> { 400, 400, 400, 400, 400, 400, 400, 400, 400, 400, 400, 400, 500, 500, 500, 500, 500, 500, 500, 500, 700, 700, 700, 700, 700, 850, 850, 850, 1000, 1000 };
            //Execute
            var skewness = await Statistics.Skewness(readings);

            var mean1 = MathNet.Numerics.Statistics.Statistics.Mean(readings);
            var median1 = MathNet.Numerics.Statistics.Statistics.Median(readings);

            var mean = await Statistics.Mean(readings);
            var median = await Statistics.Median(readings);
            
            var sd = await Statistics.StandardDeviation(readings);
            var sd1 = MathNet.Numerics.Statistics.Statistics.StandardDeviation(readings);

            var rs = new Convesys.Common.Analytics.Tests.RunningStatistics(readings);
            var s = rs.Skewness;
            var skewness1 = MathNet.Numerics.Statistics.Statistics.Skewness(readings);
            //Assert
            //Assert.AreEqual(skewness1, skewness);
            Assert.That(System.Math.Round(skewness, 3), Is.EqualTo(System.Math.Round(2.495117, 3)));
        }

        [Test]
        public async Task KurtosisTests()
        {
            //Arrange
            //var readings = new List<double> { 2, 7, 15, 4, 8 };
            var readings = new List<double> { 3, 4, 5, 2, 3, 4, 5, 6, 4, 7 };
            //Execute
            var kurtosis = await Statistics.Kurtosis(readings);
            var kurtosis1 = MathNet.Numerics.Statistics.Statistics.Kurtosis(readings);
            //Assert
            Assert.AreEqual(kurtosis, kurtosis1);
            Assert.That(System.Math.Round(kurtosis, 3), Is.EqualTo(System.Math.Round(1.85954, 3)));
        }

        [Test]
        public async Task KurtosisExellTests()
        {
            //Arrange
            var readings = new List<double> { 2, 7, 15, 4, 8 };
            //Execute
            var kurtosis = await Statistics.KurtosisExell(readings);
            var kurtosis1 = MathNet.Numerics.Statistics.Statistics.Kurtosis(readings);
            //Assert
            Assert.AreEqual(kurtosis, kurtosis1);
            Assert.That(System.Math.Round(kurtosis, 3), Is.EqualTo(System.Math.Round(1.85954, 3)));
        }

        [Test]
        public async Task SampleCovarianceTests()
        {
            //Arrange
            var readings1 = new List<double> { 65.21, 64.75, 65.26, 65.76, 65.96 };
            var readings2 = new List<double> { 67.25, 66.39, 66.12, 65.70, 66.64 };
            //Execute
            var covariance = await Statistics.Covariance(readings1, readings2);
            var covariance1 = MathNet.Numerics.Statistics.Statistics.Covariance(readings1, readings2);
            //Assert
            Assert.AreEqual(System.Math.Round(covariance, 3), System.Math.Round(covariance1, 3));
            Assert.That(System.Math.Round(covariance, 3), Is.EqualTo(System.Math.Round(-0.058050, 3)));
        }

        [Test]
        public async Task PopulationCovarianceTests()
        {
            //Arrange
            var readings1 = new List<double> { 65.21, 64.75, 65.26, 65.76, 65.96 };
            var readings2 = new List<double> { 67.25, 66.39, 66.12, 65.70, 66.64 };
            //Execute
            var covariance = await Statistics.Covariance(readings1, readings2, false);
            var covariance1 = MathNet.Numerics.Statistics.Statistics.PopulationCovariance(readings1, readings2);
            //Assert
            Assert.AreEqual(System.Math.Round(covariance,3 ), System.Math.Round(covariance1, 3));
            Assert.That(System.Math.Round(covariance, 2), Is.EqualTo(System.Math.Round(-0.046440, 2)));
        }

        [Test]
        public async Task MeanAbsoluteErrorTests()
        {
            //Arrange
            var readings1 = new List<double> { 5, 20, 40, 80, 100 };
            var estimated = 45;
            //Execute
            var error = await Statistics.MeanAbsoluteError(estimated, readings1);
            //Assert
            Assert.That(System.Math.Round(error, 2), Is.EqualTo(System.Math.Round(32.0, 2)));
        }

        [Test]
        public async Task RootMeanSquaredErrorTests()
        {
            //Arrange
            var readings1 = new List<double> { 5, 20, 40, 80, 100 };
            var estimated = 45;
            //Execute
            var error = await Statistics.MeanRootSquaredError(estimated, readings1);
            //Assert
            Assert.That(System.Math.Round(error, 2), Is.EqualTo(System.Math.Round(5.656854, 2)));
        }

        [Test]
        public async Task MeanBiasErrorTests()
        {
            //Arrange
            var readings1 = new List<double> { 5, 20, 40, 80, 100 };
            var estimated = 45;
            //Execute
            var error = await Statistics.MeanBiasError(estimated, readings1);
            //Assert
            Assert.That(System.Math.Round(error, 2), Is.EqualTo(System.Math.Round(-4.00, 2)));
        }

        [Test]
        public async Task ModeTests()
        {
            //Arrange
            var readings = new List<double> { 5, 20, 40, 40, 40, 80, 100 };

            //Execute
            var mean = await Statistics.Mean(readings);
            var median = await Statistics.Median(readings);
            var mode = await Statistics.Mode(readings);
            //Assert
            Assert.AreEqual(40, mode.First());
        }

        [Test]
        public async Task ModeTests_set1_one_mode()
        {
            //Arrange
            var readings = new List<double> { 1, 3, 3, 4, 7, 8 };

            //Execute
            var mean = await Statistics.Mean(readings);
            var median = await Statistics.Median(readings);
            var mode = await Statistics.Mode(readings);
            //Assert
            Assert.AreEqual(3, mode.First());
        }

        [Test]
        public async Task ModeTests_2_modes()
        {
            //Arrange
            var readings = new List<double> { 5, 20, 20, 80, 80, 100 };

            //Execute
            var mode = (await Statistics.Mode(readings)).ToArray();
     
            //Assert
            Assert.AreEqual(20, mode[0]);
            Assert.AreEqual(80, mode[1]);
        }

        [Test]
        public async Task ModeTests_set2_two_modes()
        {
            //Arrange
            var readings = new List<double> { 1, 3, 3, 4, 7, 7, 8 };

            //Execute
            var mode = (await Statistics.Mode(readings)).ToArray();
            //Assert
            Assert.AreEqual(3, mode[0]);
            Assert.AreEqual(7, mode[1]);
        }

        [Test]
        public async Task ModeTests_no_mode()
        {
            //Arrange
            var readings = new List<double> { 5, 20, 80, 100 };

            //Execute
            var mode = (await Statistics.Mode(readings)).ToArray();
            //Assert
            Assert.AreEqual(5, mode[0]);
            Assert.AreEqual(20, mode[1]);
            Assert.AreEqual(80, mode[2]);
            Assert.AreEqual(100, mode[3]);
        }

        [Test]
        public async Task RunPython_script_through_iron_framework()
        {
            //Arrange
            var readings = new List<double> { 5, 10, 20, 40, 80, 90, 100 };
            var res = Convesys.Common.Analytics.PythonDotNet.IronPython.KolmogorovSmirnovTest(@"D:\Dan\Software\Convesys\Convesys\AI\Python_lib.py", "1", "5, 20, 80, 100");
            //Execute

            //Assert

        }
        
        [Test]
        public async Task RunPython_script_through_iron_framework_as_a_process() //can't run it through a process
        {
            //Arrange
            var readings = new List<double> { 5, 10, 20, 40, 80, 90, 100 };
            
            //Execute
            try
            {
                
                //try to run it as a process. Platform doesn't support it
                var file = @"D:\Dan\Software\Convesys\Convesys\AI\Python_lib.py"; ;
                var args = "";
                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = file;
                start.Arguments = string.Format("{0} {1}", file, args);
                start.UseShellExecute = false;
                start.RedirectStandardOutput = true;
                using (Process process = Process.Start(start))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        string result = reader.ReadToEnd();
                    }
                }
            }
            catch(Exception e)
            {

            }
            //Assert

        }
    }
}