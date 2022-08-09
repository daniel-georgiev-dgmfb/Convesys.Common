using System.Threading.Tasks;
using static Convesys.Common.Mathematics.DoubleFunctionClass;

namespace Convesys.Common.Mathematics
{
    /// <summary>
    /// Simple implementation of the trapezoid rule. Maximum number of iterations 
    /// and/or tolerance can be changed.  Not thread safe. Adapted from source 
    /// code in "Numerical Recipes in C.", 2nd Edition, by Press, Teulosky, 
    /// Vetterling and Flannery.
    /// </summary>
    public class TrapezoidalRule_Third_Party
    {
        private int maximumIterations_ = 20;
        private double tolerance_ = 1e-6;
        private double trap_s;
        private DoubleFunction function;

        public TrapezoidalRule_Third_Party(DoubleFunction function, int maxIteration = 20, double tolerance = 1e-6)
        {
            this.function = function;
            this.maximumIterations_ = maxIteration;
            this.tolerance_ = tolerance;
        }
        /// <summary>
        /// Gets or sets the maximum number of iterations.
        /// </summary>
        public int MaximumIterations
        {
            get
            {
                return maximumIterations_;
            }
        }

        /// <summary>
        /// Gets or sets the desired tolerance.
        /// </summary>
        public double Tolerance
        {
            get
            {
                return tolerance_;
            }
        }

        /// <summary>
        /// Repeatedly calls the trapezoidal rule on finer and finer intervals until
        /// either the maximum number of iterations is reached or the desired tolerance
        /// is reached. If the maximum number of iterations does not produce a result
        /// within the tolerance, an exception is thrown.
        /// </summary>
        /// <param name="function">A delegate to a function that takes a double and
        /// returns a double.</param>
        /// <param name="lower">Lower bound.</param>
        /// <param name="upper">Upper bound.</param>
        /// <exception>Throws an exception when the rule does not converge.</exception>
        /// <returns></returns>
        public async Task<double> CalculateIntegratelIn(double lower, double upper)
        {
            double olds = -1.0e30;
            double s;

            for (int i = 1; i <= maximumIterations_; i++)
            {
                //var s1 = await trapzd(this.function, lower, upper, i);
                s = await trapzd((Func<double, Task<double>>)(_ => this.function(_)), lower, upper, i);
                if (Math.Abs(s - olds) < tolerance_ * Math.Abs(olds))
                {
                    return s;
                }
                olds = s;
            }
            throw new Exception("Fail to converge");
        }

        private async Task<double> trapzd(Func<double, Task<double>> function, double lower, double upper, int iterations)
        {
            double x, tnm, sum, del;
            int it, j;
            trap_s = 0;
            if (iterations == 1)
            {
                trap_s = 0.5 * (upper - lower) * (await function(lower) + await function(upper));
                return trap_s;
            }

            it = 1;
            for (int i = 1; i < iterations - 1; i++)
            {
                it <<= 1;
            }
            tnm = it;
            del = (upper - lower) / tnm;
            x = lower + 0.5 * del;
            sum = 0.0;
            for (j = 1; j <= it; j++)
            {
                sum += await function(x);
                x += del;
            }
            trap_s = 0.5 * (trap_s + (upper - lower) * sum / tnm);
            return trap_s;
        }

        //private async Task<double> trapzd(DoubleFunction function, double lower, double upper, int iterations)
        //{
        //    double x, tnm, sum, del;
        //    int it, j;
        //    trap_s = 0;
        //    if (iterations == 1)
        //    {
        //        trap_s = 0.5 * (upper - lower) * (await function(lower) + await function(upper));
        //        return trap_s;
        //    }

        //    it = 1;
        //    for (int i = 1; i < iterations - 1; i++)
        //    {
        //        it <<= 1;
        //    }
        //    tnm = it;
        //    del = (upper - lower) / tnm;
        //    x = lower + 0.5 * del;
        //    sum = 0.0;
        //    for (j = 1; j <= it; j++)
        //    {
        //        sum += await function(x);
        //        x += del;
        //    }
        //    trap_s = 0.5 * (trap_s + (upper - lower) * sum / tnm);
        //    return trap_s;
        //}
    }
}