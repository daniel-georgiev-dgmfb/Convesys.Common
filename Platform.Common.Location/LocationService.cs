using Convesys.Kernel.Spatial;

namespace Platform.Common.Location
{
    public class LocationService : ILocationService
    {
        const int R = 6371;
        public Task<Tuple<double, double>> GetLocation(Tuple<long, long, double> readings1, Tuple<long, long, double> readings2, Tuple<long, long, double> readings3)
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

        public Task<float> CalculateDistance(Tuple<float, float> coordinatesFrom, Tuple<float, float> coordinatesTo)
        {
            var ΔlatDifference = coordinatesFrom.Item1 - coordinatesTo.Item1;
            var ΔlonDifference = coordinatesFrom.Item2 - coordinatesTo.Item2;
            var a = MathF.Pow( MathF.Sin(ΔlatDifference / 2), 2) + MathF.Cos(coordinatesFrom.Item1) * MathF.Cos(coordinatesTo.Item2) * MathF.Pow( MathF.Sin(ΔlonDifference / 2), 2);
            var oneminusa = MathF.Sqrt((1 - a) * (1 - a));
            var c = 2 * MathF.Atan2(MathF.Sqrt(a), MathF.Sqrt(oneminusa));
            var d = R * c;
            return Task.FromResult(d);
        }
    }
}