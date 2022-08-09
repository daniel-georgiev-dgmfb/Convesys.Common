namespace Convesys.Common.Analytics
{
    using System.Diagnostics;
    using System.Linq;

    public partial class Statistics
    {
        public static Task<double> Mean(IEnumerable<double> seq)
        {
            if (seq == null)
                throw new ArgumentNullException(nameof(seq));
            if (!seq.Any())
                return Task.FromResult(double.NaN);

            var result = 0.0;
            if (seq.Any())
                result = seq.Sum() / seq.Count();

            return Task.FromResult(result);
        }

        public static Task<double> Median(IEnumerable<double> seq)
        {
            if (seq == null)
                throw new ArgumentNullException(nameof(seq));
            if (!seq.Any())
                return Task.FromResult(double.NaN);

            var result = 0.0;
            if (seq.Any())
            {

                var sorted = seq.OrderBy(x => x).ToArray();
                var len = sorted.Length;
                if (len % 2 != 0)
                    result = sorted[len / 2];
                if (len % 2 == 0)
                    result = (sorted[(len / 2) - 1] + sorted[(len / 2)]) / 2;
            }
            return Task.FromResult(result);
        }

        public static Task<double> Variance(IEnumerable<double> readings, bool sampleVariance = true)
        {
            if (readings == null)
                throw new ArgumentNullException("readings");
            var result = 0.0;
            var n = sampleVariance ? readings.Count() - 1 : readings.Count();
            var mean = Statistics.Mean(readings).Result;
            result = readings.Sum(x => (x - mean) * (x - mean)) / n;
            return Task.FromResult(result);
        }

        public static async Task<double> StandardDeviation(IEnumerable<double> readings)
        {
            if (readings == null)
                throw new ArgumentNullException("readings");
            var sampleVariance = await Statistics.Variance(readings);
            var deviation = System.Math.Sqrt(sampleVariance);
            return deviation;
        }

        public static async Task<double> Covariance(IEnumerable<double> readingsX, IEnumerable<double> readingsY, bool sampleCovariance = true)
        {
            if (readingsX == null)
                throw new ArgumentNullException("readingsX");
            if (readingsY == null)
                throw new ArgumentNullException("readingsY");
            var xCount = readingsX.Count();
            var yCount = readingsX.Count();
            if (xCount != yCount)
                throw new ArgumentException("Count differs. Readings have differnt length.");
            var n = sampleCovariance ? xCount - 1 : xCount;
            var xMeanTask =  Statistics.Mean(readingsX);
            var yMeanTask = Statistics.Mean(readingsY);
            await Task.WhenAll(xMeanTask, yMeanTask);
            var xMean = xMeanTask.Result;
            var yMean = yMeanTask.Result;
            var zipped = readingsX.Zip(readingsY).Sum(x => (x.First - xMean) * (x.Second - yMean));
            var cov = zipped / n;
            return cov;
        }

        public static async Task<double> Skewness(IEnumerable<double> readings)
        {
            if (readings == null)
                throw new ArgumentNullException("readings");
            var standardDeviation = await Statistics.StandardDeviation(readings);
            var mean = Statistics.Mean(readings).Result;
            var count = readings.Count() - 1;
            var sd = standardDeviation * standardDeviation * standardDeviation;
            var skewness = readings.Sum(x => ((x - mean) * (x - mean) * (x - mean)) / (count * sd));
            return skewness;
        }

        public static async Task<double> SkewnessExcel(IEnumerable<double> readings)
        {
            if (readings == null)
                throw new ArgumentNullException("readings");
            var standardDeviation = await Statistics.StandardDeviation(readings);
            var mean = Statistics.Mean(readings).Result;
            var count = readings.Count();
            var count_1 = count - 1;
            var count_2 = count - 2;
            double koef = ((double)count / ((double)count_1 * (double)count_2));
            var sd = standardDeviation * standardDeviation * standardDeviation;
            
            var sum = readings.Sum(x => Math.Pow((x - mean) / standardDeviation, 3));
            //var sum = readings.Sum(x => (((x - mean) / standardDeviation) * ((x - mean) / standardDeviation) * ((x - mean) / standardDeviation)));
            var skewness = sum / koef;
            //var skewness = readings.Sum(x => ((x - mean) * (x - mean) * (x - mean) / sd)) / koef;
            return skewness;
        }

        public static async Task<double> Kurtosis(IEnumerable<double> readings)
        {
            if (readings == null)
                throw new ArgumentNullException("readings");
            var standardDeviation = await Statistics.StandardDeviation(readings);
            var mean = Statistics.Mean(readings).Result;
            var count = readings.Count() - 1;
            var sd = standardDeviation * standardDeviation * standardDeviation * standardDeviation;
            var kurtosis = (readings.Sum(x => (x - mean) * (x - mean) * (x - mean) * (x - mean))/count) / sd;
            return kurtosis;
        }

        public static async Task<double> KurtosisExell(IEnumerable<double> readings)
        {
            if (readings == null)
                throw new ArgumentNullException("readings");
            var standardDeviation = await Statistics.StandardDeviation(readings);
            var mean = Statistics.Mean(readings).Result;
            var n = readings.Count();
            var count1 = n - 1;
            var count2 = n - 2;
            var count3 = n - 3;
            var sd = standardDeviation * standardDeviation * standardDeviation * standardDeviation;
            var kurtosis = (readings.Sum(x => ((x - mean) * (x - mean) * (x - mean) * (x - mean)) / sd) - ((3 * count1 * count1) / (count2 * count3)));
            return kurtosis;
        }


        public static async Task<IEnumerable<double>> Mode(IEnumerable<double>readings)
        {
            if (readings == null)
                throw new ArgumentNullException("readings");

            var maxCount = readings.GroupBy(x => x)
                .Max(x => x.Count());

            var mode = readings.GroupBy(x => x).
                Where(c => c.Count() == maxCount);


            return mode.Select(x => x.Key);
        }
    }
}