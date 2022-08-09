using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convesys.Common.Analytics.Verification
{
    [Serializable]
    public class OneSampleKSTest : ICloneable
    {
        private int N_;
        private double ksConst_;
        private double pValue_;
        private double statistic_;
        private double alpha_ = OneSampleKSTest.DEFAULT_ALPHA;
        private static double DEFAULT_ALPHA = 0.01;

        /// <summary>
        /// Default constructor. Constructs an empty OneSampleKSTest instance.
        /// </summary>
        public OneSampleKSTest()
        {
        }

        public OneSampleKSTest(DoubleVector data, Func<double, double> cdf) => this.Update(data, cdf);

        /// <summary>
        /// Constructs a OneSampleKSTest from the given sample data and specified
        /// distribution.
        /// </summary>
        /// <param name="data">The sample data.</param>
        /// <param name="dist">The hypothesized distribution.</param>
        /// <remarks>
        /// The distribution of <c>data</c> is compared to the hypothesized
        /// distribution defined by <c>dist.CDF</c>.
        /// </remarks>
        /// <remarks>
        /// The alpha level is set to the current value specified by the static
        /// DefaultAlpha property; the form of the hypothesis test is set to the
        /// current DefaultType. Missing values are ignored.
        /// </remarks>
        /// <exception cref="T:CenterSpace.NMath.Core.InvalidArgumentException">
        /// Thrown if <c>data</c> has length of zero, after missing values are
        /// ignored.
        /// </exception>
        public OneSampleKSTest(DoubleVector data, ProbabilityDistribution dist)
          : this(data, new Func<double, double>(dist.CDF))
        {
        }

        public OneSampleKSTest(int[] data, Func<double, double> cdf) => this.Update(data, cdf);

        /// <summary>
        /// Constructs a OneSampleKSTest from the given sample data and specified
        /// distribution.
        /// </summary>
        /// <param name="data">The sample data.</param>
        /// <param name="dist">The hypothesized distribution.</param>
        /// <remarks>
        /// The distribution of <c>data</c> is compared to the hypothesized
        /// distribution defined by <c>dist.CDF</c>.
        /// </remarks>
        /// <remarks>
        /// The alpha level is set to the current value specified by the static
        /// DefaultAlpha property; the form of the hypothesis test is set to the
        /// current DefaultType.
        /// </remarks>
        /// <remarks>Missing values are ignored.</remarks>
        /// <exception cref="T:CenterSpace.NMath.Core.InvalidArgumentException">
        /// Thrown if <c>data</c> has length of zero.
        /// </exception>
        public OneSampleKSTest(int[] data, ProbabilityDistribution dist) => this.Update(data, dist);

        public OneSampleKSTest(double[] data, Func<double, double> cdf) => this.Update(data, cdf);

        /// <summary>
        /// Constructs a OneSampleKSTest from the given sample data and specified
        /// distribution.
        /// </summary>
        /// <param name="data">The sample data.</param>
        /// <param name="dist">The hypothesized distribution.</param>
        /// <remarks>
        /// The distribution of <c>data</c> is compared to the hypothesized
        /// distribution defined by <c>dist.CDF</c>.
        /// </remarks>
        /// <remarks>
        /// The alpha level is set to the current value specified by the static
        /// DefaultAlpha property; the form of the hypothesis test is set to the
        /// current DefaultType. Missing values are ignored.
        /// </remarks>
        /// <exception cref="T:CenterSpace.NMath.Core.InvalidArgumentException">
        /// Thrown if <c>data</c> has length of zero, after removing missing values.
        /// </exception>
        public OneSampleKSTest(double[] data, ProbabilityDistribution dist) => this.Update(data, dist);

        public OneSampleKSTest(IDFColumn data, Func<double, double> cdf) => this.Update(data, cdf);

        /// <summary>
        /// Constructs a OneSampleKSTest from the given sample data and specified
        /// distribution.
        /// </summary>
        /// <param name="data">The sample data.</param>
        /// <param name="dist">The hypothesized distribution.</param>
        /// <remarks>
        /// The distribution of <c>data</c> is compared to the hypothesized
        /// distribution defined by <c>dist.CDF</c>.
        /// </remarks>
        /// <remarks>
        /// The alpha level is set to the current value specified by the static
        /// DefaultAlpha property; the form of the hypothesis test is set to the
        /// current DefaultType. Missing values are ignored.
        /// </remarks>
        /// <exception cref="T:CenterSpace.NMath.Core.InvalidArgumentException">
        /// Thrown if <c>data</c> is not numeric or has length of zero, after
        /// missing values are ignored.
        /// </exception>
        public OneSampleKSTest(IDFColumn data, ProbabilityDistribution dist) => this.Update(data, dist);

        public OneSampleKSTest(DoubleVector data, Func<double, double> cdf, double alpha)
        {
            this.alpha_ = alpha;
            this.Update(data, cdf);
        }

        /// <summary>
        /// Constructs a OneSampleKSTest from the given sample data and specified
        /// distribution.
        /// </summary>
        /// <param name="data">The sample data.</param>
        /// <param name="dist">The hypothesized distribution.</param>
        /// <param name="alpha">The accepted probability of falsely rejecting
        /// the null hypothesis.</param>
        /// <remarks>
        /// The distribution of <c>data</c> is compared to the hypothesized
        /// distribution defined by <c>dist.CDF</c>. Missing values are ignored.
        /// </remarks>
        /// <exception cref="T:CenterSpace.NMath.Core.InvalidArgumentException">
        /// Thrown if <c>data</c> has length of zero, after removing missing values.
        /// </exception>
        public OneSampleKSTest(DoubleVector data, ProbabilityDistribution dist, double alpha)
          : this(data, new Func<double, double>(dist.CDF), alpha)
        {
        }

        public OneSampleKSTest(int[] data, Func<double, double> cdf, double alpha)
        {
            this.alpha_ = alpha;
            this.Update(data, cdf);
        }

        /// <summary>
        /// Constructs a OneSampleKSTest from the given sample data and specified
        /// distribution.
        /// </summary>
        /// <param name="data">The sample data.</param>
        /// <param name="dist">The hypothesized distribution.</param>
        /// <param name="alpha">The accepted probability of falsely rejecting
        /// the null hypothesis.</param>
        /// <remarks>
        /// The distribution of <c>data</c> is compared to the hypothesized
        /// distribution defined by <c>dist.CDF</c>.
        /// </remarks>
        /// <exception cref="T:CenterSpace.NMath.Core.InvalidArgumentException">
        /// Thrown if <c>data</c> has length of zero.
        /// </exception>
        public OneSampleKSTest(int[] data, ProbabilityDistribution dist, double alpha)
        {
            this.alpha_ = alpha;
            this.Update(data, dist);
        }

        public OneSampleKSTest(double[] data, Func<double, double> cdf, double alpha)
        {
            this.alpha_ = alpha;
            this.Update(data, cdf);
        }

        /// <summary>
        /// Constructs a OneSampleKSTest from the given sample data and specified
        /// distribution.
        /// </summary>
        /// <param name="data">The sample data.</param>
        /// <param name="dist">The hypothesized distribution.</param>
        /// <param name="alpha">The accepted probability of falsely rejecting
        /// the null hypothesis.</param>
        /// <remarks>
        /// The distribution of <c>data</c> is compared to the hypothesized
        /// distribution defined by <c>dist.CDF</c>. Missing values are ignored.
        /// </remarks>
        /// <exception cref="T:CenterSpace.NMath.Core.InvalidArgumentException">
        /// Thrown if <c>data</c> has length of zero, after removing missing values.
        /// </exception>
        public OneSampleKSTest(double[] data, ProbabilityDistribution dist, double alpha)
        {
            this.alpha_ = alpha;
            this.Update(data, dist);
        }

        public OneSampleKSTest(IDFColumn data, Func<double, double> cdf, double alpha)
        {
            this.alpha_ = alpha;
            this.Update(data, cdf);
        }

        /// <summary>
        /// Constructs a OneSampleKSTest from the given sample data and specified
        /// distribution.
        /// </summary>
        /// <param name="data">The sample data.</param>
        /// <param name="dist">The hypothesized distribution.</param>
        /// <param name="alpha">The accepted probability of falsely rejecting
        /// the null hypothesis.</param>
        /// <remarks>
        /// The distribution of <c>data</c> is compared to the hypothesized
        /// distribution defined by <c>dist.CDF</c>.  Missing values are ignored.
        /// </remarks>
        /// <exception cref="T:CenterSpace.NMath.Core.InvalidArgumentException">
        /// Thrown if <c>data</c> is not numeric or has length of zero, after
        /// missing values are ignored.
        /// </exception>
        public OneSampleKSTest(IDFColumn data, ProbabilityDistribution dist, double alpha)
        {
            this.alpha_ = alpha;
            this.Update(data, dist);
        }

        /// <summary>
        /// Gets and sets the alpha level associated with this hypothesis test.
        /// </summary>
        public double Alpha
        {
            get => this.alpha_;
            set => this.alpha_ = value;
        }

        /// <summary>
        /// Gets the critical value based on the current alpha level associated
        /// with this hypothesis test.
        /// </summary>
        public double CriticalValue => this.ComputeCriticalValue(this.alpha_);

        /// <summary>Gets the sample size.</summary>
        public int N => this.N_;

        /// <summary>Gets the p-value associated with the test statistic.</summary>
        public double P => this.pValue_;

        /// <summary>
        /// Tests whether the null hypothesis can be rejected, using the current
        /// alpha level.
        /// </summary>
        /// <remarks>
        /// Returns <c>true</c> if <c>P &lt; Alpha</c>; otherwise, <c>false</c>.
        ///  </remarks>
        public bool Reject => this.pValue_ < this.alpha_;

        /// <summary>
        /// Gets the value of the test statistic associated with this hypothesis
        /// test.
        /// </summary>
        public double Statistic => this.statistic_;

        public void Update(DoubleVector data, Func<double, double> cdf) => this.Compute(NMathFunctions.Sort(NMathFunctions.NaNRemove(data)), new Func<double, double>(cdf.Invoke));

        /// <summary>
        /// Updates this test with new sample data and a new distribution.
        /// </summary>
        /// <param name="data">The sample data.</param>
        /// <param name="dist">The hypothesized distribution.</param>
        /// <remarks>
        /// The distribution of <c>data</c> is compared to the hypothesized
        /// distribution defined by <c>dist.CDF</c>. Missing values are ignored.
        /// </remarks>
        /// <exception cref="T:CenterSpace.NMath.Core.InvalidArgumentException">
        /// Thrown if <c>data</c> has length of zero, after removing missing values.
        /// </exception>
        public void Update(DoubleVector data, ProbabilityDistribution dist) => this.Update(data, new Func<double, double>(dist.CDF));

        public void Update(int[] data, Func<double, double> cdf) => this.Compute(NMathFunctions.Sort(data), cdf);

        /// <summary>
        /// Updates this test with new sample data and a new distribution.
        /// </summary>
        /// <param name="data">The sample data.</param>
        /// <param name="dist">The hypothesized distribution.</param>
        /// <remarks>
        /// The distribution of <c>data</c> is compared to the hypothesized
        /// distribution defined by <c>dist.CDF</c>.
        /// </remarks>
        /// <exception cref="T:CenterSpace.NMath.Core.InvalidArgumentException">
        /// Thrown if <c>data</c> has length of zero.
        /// </exception>
        public void Update(int[] data, ProbabilityDistribution dist) => this.Update(data, new Func<double, double>(dist.CDF));

        public void Update(double[] data, Func<double, double> cdf) => this.Compute(NMathFunctions.Sort(NMathFunctions.NaNRemove(data)), cdf);

        /// <summary>
        /// Updates this test with new sample data and a new distribution.
        /// </summary>
        /// <param name="data">The sample data.</param>
        /// <param name="dist">The hypothesized distribution.</param>
        /// <remarks>
        /// The distribution of <c>data</c> is compared to the hypothesized
        /// distribution defined by <c>dist.CDF</c>. Missing values are ignored.
        /// </remarks>
        /// <exception cref="T:CenterSpace.NMath.Core.InvalidArgumentException">
        /// Thrown if <c>data</c> has length of zero, after missing values are
        /// ignored.
        /// </exception>
        public void Update(double[] data, ProbabilityDistribution dist) => this.Update(data, new Func<double, double>(dist.CDF));

        public void Update(IDFColumn data, Func<double, double> cdf)
        {
            try
            {
                this.Compute((DFNumericColumn)NMathFunctions.Sort(NMathFunctions.NaNRemove((IDFColumn)new DFNumericColumn(data.Name, data))), cdf);
            }
            catch (FormatException ex)
            {
                throw new InvalidArgumentException("Could not convert data to numeric values.");
            }
        }

        /// <summary>
        /// Updates this test with new sample data and a new distribution.
        /// </summary>
        /// <param name="data">The sample data.</param>
        /// <param name="dist">The hypothesized distribution.</param>
        /// <remarks>
        /// The distribution of <c>data</c> is compared to the hypothesized
        /// distribution defined by <c>dist.CDF</c>. Missing values are ignored.
        /// </remarks>
        /// <exception cref="T:CenterSpace.NMath.Core.InvalidArgumentException">
        /// Thrown if <c>data</c> is not numeric or has length of zero, after
        /// missing values are ignored.
        /// </exception>
        public void Update(IDFColumn data, ProbabilityDistribution dist) => this.Update(data, new Func<double, double>(dist.CDF));

        /// <summary>Creates a deep copy of this OneSampleKSTest.</summary>
        /// <returns>A deep copy of this OneSampleKSTest.</returns>
        public object Clone() => (object)new OneSampleKSTest()
        {
            N_ = this.N_,
            ksConst_ = this.ksConst_,
            pValue_ = this.pValue_,
            statistic_ = this.statistic_,
            alpha_ = this.alpha_
        };

        private double ComputeCriticalValue(double alpha) => Math.Abs(ProbabilityDistribution.InverseCdfUsingBracket(new Func<double, double>(this.KSCDF), 1.0 - alpha, 0.0, 1.05));

        private void Compute(double[] sorted, Func<double, double> cdf)
        {
            this.N_ = sorted.Length != 0 ? sorted.Length : throw new InvalidArgumentException("No valid data elements.");
            double num = Math.Sqrt((double)this.N_);
            this.ksConst_ = num + 0.12 + 0.11 / num;
            this.statistic_ = this.ComputeKSStatistic(sorted, cdf);
            this.pValue_ = OneSampleKSTest.ProbKS(this.ksConst_ * this.statistic_);
        }

        private double ComputeKSStatistic(double[] sorted, Func<double, double> cdf)
        {
            double num1 = 0.0;
            double num2 = 0.0;
            int num3 = 0;
            for (int index = 0; index < sorted.Length; ++index)
            {
                double num4 = (double)(num3 + 1) / (double)this.N_;
                double num5 = (double)num3 / (double)this.N_;
                double num6 = cdf(sorted[index]);
                double num7 = Math.Abs(num6 - num4);
                double num8 = Math.Abs(num6 - num5);
                if (num7 > num1)
                    num1 = num7;
                if (num8 > num2)
                    num2 = num8;
                ++num3;
            }
            return num1 <= num2 ? num2 : num1;
        }

        private void Compute(DoubleVector sorted, Func<double, double> cdf)
        {
            this.N_ = sorted.Length != 0 ? sorted.Length : throw new InvalidArgumentException("No valid data elements.");
            double num = Math.Sqrt((double)this.N_);
            this.ksConst_ = num + 0.12 + 0.11 / num;
            this.statistic_ = this.ComputeKSStatistic(sorted, cdf);
            this.pValue_ = OneSampleKSTest.ProbKS(this.ksConst_ * this.statistic_);
        }

        private double ComputeKSStatistic(DoubleVector sorted, Func<double, double> cdf)
        {
            double num1 = 0.0;
            double num2 = 0.0;
            int num3 = 0;
            for (int position = 0; position < sorted.Length; ++position)
            {
                double num4 = (double)(num3 + 1) / (double)this.N_;
                double num5 = (double)num3 / (double)this.N_;
                double num6 = cdf(sorted[position]);
                double num7 = Math.Abs(num6 - num4);
                double num8 = Math.Abs(num6 - num5);
                if (num7 > num1)
                    num1 = num7;
                if (num8 > num2)
                    num2 = num8;
                ++num3;
            }
            return num1 <= num2 ? num2 : num1;
        }

        private void Compute(int[] sorted, Func<double, double> cdf)
        {
            this.N_ = sorted.Length != 0 ? sorted.Length : throw new InvalidArgumentException("No valid data elements.");
            double num = Math.Sqrt((double)this.N_);
            this.ksConst_ = num + 0.12 + 0.11 / num;
            this.statistic_ = this.ComputeKSStatistic(sorted, cdf);
            this.pValue_ = OneSampleKSTest.ProbKS(this.ksConst_ * this.statistic_);
        }

        private double ComputeKSStatistic(int[] sorted, Func<double, double> cdf)
        {
            double num1 = 0.0;
            double num2 = 0.0;
            int num3 = 0;
            for (int index = 0; index < sorted.Length; ++index)
            {
                double num4 = (double)(num3 + 1) / (double)this.N_;
                double num5 = (double)num3 / (double)this.N_;
                double num6 = cdf((double)sorted[index]);
                double num7 = Math.Abs(num6 - num4);
                double num8 = Math.Abs(num6 - num5);
                if (num7 > num1)
                    num1 = num7;
                if (num8 > num2)
                    num2 = num8;
                ++num3;
            }
            return num1 <= num2 ? num2 : num1;
        }

        private void Compute(DFNumericColumn sorted, Func<double, double> cdf)
        {
            this.N_ = sorted.Count != 0 ? sorted.Count : throw new InvalidArgumentException("No valid data elements.");
            double num = Math.Sqrt((double)this.N_);
            this.ksConst_ = num + 0.12 + 0.11 / num;
            this.statistic_ = this.ComputeKSStatistic(sorted, cdf);
            this.pValue_ = OneSampleKSTest.ProbKS(this.ksConst_ * this.statistic_);
        }

        private double ComputeKSStatistic(DFNumericColumn sorted, Func<double, double> cdf)
        {
            double num1 = 0.0;
            double num2 = 0.0;
            int num3 = 0;
            for (int position = 0; position < sorted.Count; ++position)
            {
                double num4 = (double)(num3 + 1) / (double)this.N_;
                double num5 = (double)num3 / (double)this.N_;
                double num6 = cdf(sorted[position]);
                double num7 = Math.Abs(num6 - num4);
                double num8 = Math.Abs(num6 - num5);
                if (num7 > num1)
                    num1 = num7;
                if (num8 > num2)
                    num2 = num8;
                ++num3;
            }
            return num1 <= num2 ? num2 : num1;
        }

        private static double ProbKS(double alam)
        {
            double num1 = 2.0;
            double num2 = 0.0;
            double num3 = 0.0;
            double num4 = -2.0 * alam * alam;
            for (int index = 1; index <= 100; ++index)
            {
                double num5 = num1 * Math.Exp(num4 * (double)index * (double)index);
                num2 += num5;
                if (Math.Abs(num5) <= 0.001 * num3 || Math.Abs(num5) <= 1E-08 * num2)
                    return num2;
                num1 = -num1;
                num3 = Math.Abs(num5);
            }
            return 1.0;
        }

        private double KSCDF(double x) => 1.0 - OneSampleKSTest.ProbKS(this.ksConst_ * x);

        /// <summary>
        /// Gets and sets the default alpha level associated with OneSampleKSTests.
        /// </summary>
        public static double DefaultAlpha
        {
            get => OneSampleKSTest.DEFAULT_ALPHA;
            set => OneSampleKSTest.DEFAULT_ALPHA = value;
        }
    }
}
