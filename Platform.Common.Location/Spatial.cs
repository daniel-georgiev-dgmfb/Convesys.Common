namespace Platform.Common.Location
{
    public class Spatial
    {
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