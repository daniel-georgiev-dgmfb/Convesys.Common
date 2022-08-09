namespace Convesys.Common.Analytics
{
    public partial class Statistics
    {
        public static async Task<double> MeanAbsoluteError(double estimated, IEnumerable<double> observations)
        {
            var error = await Task.FromResult(observations.Sum(o => System.Math.Sqrt((o - estimated) * (o - estimated))) / observations.Count());
            return error;
        }

        public static async Task<double> MeanRootSquaredError(double estimated, IEnumerable<double> observations)
        {
            var error = await Statistics.MeanAbsoluteError(estimated, observations);
            return System.Math.Sqrt(error);
        }

        public static async Task<double> MeanBiasError(double estimated, IEnumerable<double> observations)
        {
            var sum = observations.Sum(o => estimated - o);
            var error = await Task.FromResult(sum / observations.Count());
            return error;
        }
    }
}
