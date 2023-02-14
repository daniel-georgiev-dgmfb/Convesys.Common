namespace Convesys.Common.Mathematics
{
    public class Spatial
    {
        public static Task<double> EuclideanDistance(IEnumerable<double> point1, IEnumerable<double> point2)
        {
            if (point1 == null)
                throw new ArgumentNullException(nameof(point1));
            if (point2 == null)
                throw new ArgumentNullException(nameof(point2));
            if (point1.Count() != point2.Count())
                throw new ArgumentException("The arrays supplied are with a different length.");
            var distance = 0.00;
            var zipped = point1.Zip(point2);
            var aggregated = zipped.Aggregate(distance, (a, b) =>
            {
                var sq = (b.First - b.Second) * (b.First - b.Second);
                distance = distance + sq;
                return distance;
            });
            return Task.FromResult(System.Math.Sqrt(distance));
        }
    }
}