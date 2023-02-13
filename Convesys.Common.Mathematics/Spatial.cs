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

        public static Task<Tuple<double, double>> GetLocation(Tuple<long, long, double> readings1, Tuple<long, long, double> readings2, Tuple<long, long, double> readings3)
        {
            var A = 2 * readings2.Item1 - 2 * readings1.Item1;
            var B = 2 * readings2.Item2 - 2 * readings1.Item2;
            var C = readings1.Item3 * readings1.Item3 - readings2.Item3 * readings2.Item3 - readings1.Item1 * readings1.Item1 + readings2.Item1 * readings2.Item1 - readings1.Item2 * readings1.Item2 + readings2.Item2 * readings2.Item2;
            var D = 2 * readings3.Item1 - 2 * readings2.Item1;
            var E = 2 * readings3.Item2 - 2 * readings2.Item2;
            var F = readings2.Item3 * readings2.Item3 - readings3.Item3 * readings3.Item3 - readings2.Item1 * readings2.Item1 + readings3.Item1 * readings3.Item1 - readings2.Item2 * readings2.Item2 + readings3.Item2 * readings3.Item2;
            var x = (C * E - F * B) / (E * A - B * D);
            var y = (C * D - A * F) / (B * D - A * E);
            return Task.FromResult(Tuple.Create(x, y));
        }
    }
}