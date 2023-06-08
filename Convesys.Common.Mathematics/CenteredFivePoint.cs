using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twilight.Platform.Common.Mathematics
{
    public class CenteredFivePoint
    {
        public static Task<double> Run(Func<double, double> f, double x)
        {
            double h = 10e-6;
            double h2 = h * 2;
            return Task.FromResult((f(x - h2) - 8 * f(x - h) + 8 * f(x + h) - f(x + h2)) / (h2 * 6));
        }
    }
}
