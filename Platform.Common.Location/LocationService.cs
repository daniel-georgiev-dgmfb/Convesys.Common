using Twilight.Kernel.Spatial;

namespace Twilight.Platform.Common.Location
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

        /// <summary>
        /// https://www.igismap.com/haversine-formula-calculate-geographic-distance-earth/
        /// </summary>
        /// <param name="coordinatesFrom"></param>
        /// <param name="coordinatesTo"></param>
        /// <returns></returns>
        public Task<float> CalculateDistance(Tuple<float, float> coordinatesFrom, Tuple<float, float> coordinatesTo)
        {
            var ΔlatDifference = MathF.PI * (coordinatesFrom.Item1 - coordinatesTo.Item1) / 180;
            var ΔlonDifference = MathF.PI * (coordinatesFrom.Item2 - coordinatesTo.Item2) / 180;
            var a = MathF.Pow( MathF.Sin(ΔlatDifference / 2), 2) + (MathF.Cos(this.DegreesToRadians(coordinatesFrom.Item1)) * MathF.Cos(this.DegreesToRadians(coordinatesTo.Item2)) * MathF.Pow( MathF.Sin(ΔlonDifference / 2), 2));
            var oneminusa = MathF.Sqrt((1 - a) * (1 - a));
            var c = 2 * MathF.Atan2(MathF.Sqrt(a), MathF.Sqrt(oneminusa));
            var d = R * c;
            return Task.FromResult(d);
        }

        /// <summary>
        /// https://stackoverflow.com/questions/365826/calculate-distance-between-2-gps-coordinates
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lon1"></param>
        /// <param name="lat2"></param>
        /// <param name="lon2"></param>
        /// <returns></returns>
        public Task<float> DistanceInKmBetweenEarthCoordinates(float lat1, float lon1, float lat2, float lon2)
        {
            var from = Tuple.Create(lat1, lon1);
            var to = Tuple.Create(lat2, lon2);
            return this.CalculateDistance(from, to);
            //var earthRadiusKm = 6371;

            //var dLat = DegreesToRadians(lat2 - lat1);
            //var dLon = DegreesToRadians(lon2 - lon1);

            //lat1 = DegreesToRadians(lat1);
            //lat2 = DegreesToRadians(lat2);

            //var a = MathF.Sin(dLat / 2) * MathF.Sin(dLat / 2) + MathF.Sin(dLon / 2) * MathF.Sin(dLon / 2) * MathF.Cos(lat1) * MathF.Cos(lat2);
            //var c = 2 * MathF.Atan2(MathF.Sqrt(a), MathF.Sqrt(1 - a));
            //return Task.FromResult(earthRadiusKm * c);
        }

        public Task<float> DistanceInKmBetweenEarthCoordinates(Tuple<float, float> from, Tuple<float, float> to)
        {
            var earthRadiusKm = 6371;

            var dLat = DegreesToRadians(to.Item1 - from.Item1);
            var dLon = DegreesToRadians(to.Item2 - from.Item2);

            var lat1 = DegreesToRadians(from.Item1);
            var lat2 = DegreesToRadians(to.Item1);

            var a = MathF.Sin(dLat / 2) * MathF.Sin(dLat / 2) + MathF.Sin(dLon / 2) * MathF.Sin(dLon / 2) * MathF.Cos(lat1) * MathF.Cos(lat2);
            var c = 2 * MathF.Atan2(MathF.Sqrt(a), MathF.Sqrt(1 - a));
            return Task.FromResult(earthRadiusKm * c);
        }

        private float DegreesToRadians(float degrees)
        {
            return degrees * (float)Math.PI / 180;
        }
    }
}