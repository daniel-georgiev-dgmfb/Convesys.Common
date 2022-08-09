using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convesys.Common.Mathematics
{
    /// <summary>
    ///Approximate an area under a curve.
    /// https://math.libretexts.org/Courses/Mount_Royal_University/MATH_2200%3A_Calculus_for_Scientists_II/2%3A_Techniques_of_Integration/2.5%3A_Numerical_Integration_-_Midpoint%2C_Trapezoid%2C_Simpson's_rule
    /// </summary>
    public class SimpsonRule
    {
        public static Task<double> Run(Func<double, double> func, double from, double to, int iterations = 4)
        {
            var result = 0.0;
            var h = 1 / (double)iterations;
            //var dx = (to - from) / iterations;
            var hthird = h / 3;
            var y0 = func(from);
            var yn = func(to);
            for (var i = 1; i < iterations - 1; i++)
            {
                var xi = from + (i * hthird);
                var mod = (i % 2);
                var foo = (mod == 0) ?
                    (2 * func(xi)) :
                    (4 * func(xi));
                result = result + foo;
            }
            result += (y0 + yn);
            result *= hthird;
            return Task.FromResult(result);
        }
    }
}