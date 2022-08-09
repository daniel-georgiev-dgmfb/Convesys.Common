namespace Convesys.Common.Analytics.Verification
{
    public class TTest
    {
        public static Task<double> Run(IEnumerable<double> estimated, IEnumerable<double> actual)
        {
            if(estimated == null)
                throw new ArgumentNullException(nameof(estimated));
            if (actual == null)
                throw new ArgumentNullException(nameof(actual));

            var estimatedCount = estimated.Count();
            var actualCount = actual.Count();
            
            if (estimatedCount != actualCount)
                throw new ArgumentException(string.Format("Parameter count should match."));
            if (Enumerable.SequenceEqual(estimated, actual))
                return Task.FromResult(0.00);

            var zipped = Enumerable.Zip(actual, estimated);
            var sum1 = zipped.Sum(x => x.First - x.Second);
            var tt1 = sum1 / actualCount;
            var sum2 = zipped.Sum(x => (x.First - x.Second - tt1) * (x.First - x.Second - tt1));
            var tt2 = Math.Sqrt(sum2 / (actualCount - 1));
            return Task.FromResult(tt1 / tt2);
        }
    }
}