using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convesys.Common.Analytics.Verification
{
    /// <summary>
    /// Class ProbabilityDistribution is the abstract base class for classes that
    /// represent distributions of random variables.
    /// </summary>
    [Serializable]
    public abstract class ProbabilityDistribution : ICloneable
    {
        /// <summary>
        /// Maximum number of iterations to execute while computing the various
        /// forms of the gamma function.
        /// </summary>
        protected const int GAMMA_MAX_ITER = 2000;
        /// <summary>A small number sufficeintly close to zero.</summary>
        protected const double MACHINE_EPSILON = 1E-12;

        /// <summary>
        /// Computes the cumulative distribution function at a given point.
        /// </summary>
        /// <param name="x">A position on the x-axis.</param>
        /// <returns>The cumulative distribution function evaluated at <c>x</c>.</returns>
        public abstract double CDF(double x);

        /// <summary>
        /// Computes the inverse of the cumulative distribution function at a
        /// given probability.
        /// </summary>
        /// <param name="p">A probability.</param>
        /// <returns>The value <c>x</c> such that <c>CDF(x) = p</c>.</returns>
        public abstract double InverseCDF(double p);

        /// <summary>
        /// Computes the probability density function at a given point.
        /// </summary>
        /// <param name="x">A position on the x-axis.</param>
        /// <returns>The probablility density function evaluated at <c>x</c>.</returns>
        public abstract double PDF(double x);

        /// <summary>
        /// Uses a bracketing method to evaluate the inverse of cumulative
        /// distribution functions.
        /// </summary>
        /// <param name="p">A probability.</param>
        /// <param name="lowerBound">A lower bound for the inverse value.</param>
        /// <param name="upperBound">An upper bound for the inverse value.</param>
        /// <returns>The value <c>x</c> such that <c>CDF(x) = p</c>.</returns>
        protected double InverseCdfUsingBracket(double p, double lowerBound, double upperBound) => ProbabilityDistribution.InverseCdfUsingBracket(new Func<double, double>(this.CDF), p, lowerBound, upperBound);

        /// <summary>
        /// Uses a bracketing method to evaluate the inverse of cumulative
        /// distribution functions for discrete distributions.
        /// </summary>
        /// <param name="p">A probability.</param>
        /// <param name="lowerBound">A lower bound for the inverse value.</param>
        /// <param name="upperBound">An upper bound for the inverse value.</param>
        /// <returns>The value <c>x</c> such that <c>CDF(x) = p</c>.</returns>
        protected double InverseDiscreteCdfUsingBracket(double p, int lowerBound, int upperBound)
        {
            //check the lisense//Hudr.threads();
            double num1 = (double)lowerBound;
            double num2 = (double)upperBound;
            double x = Math.Floor((num1 + num2) / 2.0);
            double num3 = double.NaN;
            double num4 = this.CDF(x);
            int num5;
            for (num5 = 0; x != num3 && num5 <= 100000; ++num5)
            {
                if (num4 > p)
                    num2 = x;
                else
                    num1 = x;
                num3 = x;
                x = Math.Floor((num1 + num2) / 2.0);
                num4 = this.CDF(x);
            }
            if (num5 == 100000)
                throw new Exception("Failure to converge after " + 100000.ToString() + " iterations in inverse CDF.");
            return x;
        }

        internal static double InverseCdfUsingBracket(
          Func<double, double> cdf,
          double p,
          double lowerBound,
          double upperBound)
        {
            double num1 = lowerBound;
            double num2 = upperBound;
            double num3 = double.MaxValue;
            double num4 = 0.5 * (num1 + num2);
            double num5 = cdf(num4);
            int num6;
            for (num6 = 0; Math.Abs(num3 - num4) > 1E-15 && num6 <= 10000; ++num6)
            {
                if (num5 > p)
                    num2 = num4;
                else
                    num1 = num4;
                num3 = num4;
                num4 = (num2 + num1) * 0.5;
                num5 = cdf(num4);
            }
            if (num6 >= 10000)
                throw new Exception("MAX ITERATIONS EXCEEDED IN InverseCdfUsingBracket");
            return num4;
        }

        /// <summary>Creates a deep copy of this distribution.</summary>
        /// <returns>A deep copy of this distribution.</returns>
        public abstract object Clone();

        /// <summary>
        /// Returns a formatted string representation of this distribution.
        /// </summary>
        /// <returns>A formatted string representation of this distribution.</returns>
        public abstract override string ToString();
    }
}
