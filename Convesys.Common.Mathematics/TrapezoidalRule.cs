using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twilight.Platform.Common.Mathematics
{
    /// <summary>
    ///Approximate an area under a curve.
    /// https://math.libretexts.org/Courses/Mount_Royal_University/MATH_2200%3A_Calculus_for_Scientists_II/2%3A_Techniques_of_Integration/2.5%3A_Numerical_Integration_-_Midpoint%2C_Trapezoid%2C_Simpson's_rule
    /// </summary>
    public class TrapezoidalRule
    {
        public static Task<Tuple<double, double>> Run(Func<double, double> func, double from, double to, int iterations = 10000)
        {
            var result = 0.0;
            var dx = (to - from) / iterations;
            var dxhalf = dx / 2;
            var f0 = func(from);
            var fn = func(to);
            for (var i = 1; i < iterations - 1; i++)
            {
                var xi = from + (i * dx);
                result = result + (2 * func(xi));
            }
            result += (f0 + fn);
            result *= dxhalf;
            var error = 0.0;
            return Task.FromResult(Tuple.Create(result, error));
        }

        //ToDo: Refactor intergral interpolation in parallel.
        //public static Task<Tuple<double, double>> RunInParallel(Func<double, double> func, double from, double to, int iterations = 10000)
        //{
        //    var result = 0.0;
        //    var dx = (to - from) / iterations;
        //    var dxhalf = dx / 2;
        //    var f0 = func(from);
        //    var fn = func(to);
        //    Parallel.For(1, iterations -1, i => {
        //        var xi = from + (i * dx);
        //        Interlocked.ex
        //        //Interlocked.Add(ref result,  (2 * func(xi)));
        //        result = result + (2 * func(xi));
        //    });
        //    result += (f0 + fn);
        //    result *= dxhalf;
        //    var error = 0.0;
        //    return Task.FromResult(Tuple.Create(result, error));
        //}
    }
}