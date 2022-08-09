using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Convesys.Common.Analytics.Tests
{
    [DataContract(Namespace = "urn:MathNet/Numerics")]
    public class RunningStatistics
    {
        [DataMember(Order = 1)]
        private long _n;
        [DataMember(Order = 2)]
        private double _min = double.PositiveInfinity;
        [DataMember(Order = 3)]
        private double _max = double.NegativeInfinity;
        [DataMember(Order = 4)]
        private double _m1;
        [DataMember(Order = 5)]
        private double _m2;
        [DataMember(Order = 6)]
        private double _m3;
        [DataMember(Order = 7)]
        private double _m4;

        public RunningStatistics()
        {
        }

        public RunningStatistics(IEnumerable<double> values) => this.PushRange(values);

        public long Count => this._n;

        public double Minimum => this._n <= 0L ? double.NaN : this._min;

        public double Maximum => this._n <= 0L ? double.NaN : this._max;

        public double Mean => this._n <= 0L ? double.NaN : this._m1;

        public double Variance => this._n >= 2L ? this._m2 / (double)(this._n - 1L) : double.NaN;

        public double PopulationVariance => this._n >= 2L ? this._m2 / (double)this._n : double.NaN;

        public double StandardDeviation => this._n >= 2L ? Math.Sqrt(this._m2 / (double)(this._n - 1L)) : double.NaN;

        public double PopulationStandardDeviation => this._n >= 2L ? Math.Sqrt(this._m2 / (double)this._n) : double.NaN;

        public double Skewness
        {
            get
            {
                return this._n >= 3L ?
                (double)this._n 
                * this._m3 
                * Math.Sqrt(this._m2 / (double)(this._n - 1L)) / (this._m2 * this._m2 * (double)(this._n - 2L)) * (double)(this._n - 1L)
                : double.NaN;
            }
        }

        public double PopulationSkewness => this._n >= 2L ? Math.Sqrt((double)this._n) * this._m3 / Math.Pow(this._m2, 1.5) : double.NaN;

        public double Kurtosis => this._n >= 4L ? ((double)this._n * (double)this._n - 1.0) / (double)((this._n - 2L) * (this._n - 3L)) * ((double)this._n * this._m4 / (this._m2 * this._m2) - 3.0 + 6.0 / (double)(this._n + 1L)) : double.NaN;

        public double PopulationKurtosis => this._n >= 3L ? (double)this._n * this._m4 / (this._m2 * this._m2) - 3.0 : double.NaN;

        public void Push(double value)
        {
            ++this._n;
            double num1 = value - this._m1;
            double num2 = num1 / (double)this._n;
            double num3 = num2 * num2;
            double num4 = num1 * num2 * (double)(this._n - 1L);
            this._m1 += num2;
            this._m4 += num4 * num3 * (double)(this._n * this._n - 3L * this._n + 3L) + 6.0 * num3 * this._m2 - 4.0 * num2 * this._m3;
            this._m3 += num4 * num2 * (double)(this._n - 2L) - 3.0 * num2 * this._m2;
            this._m2 += num4;
            if (value < this._min || double.IsNaN(value))
                this._min = value;
            if (value <= this._max && !double.IsNaN(value))
                return;
            this._max = value;
        }

        public void PushRange(IEnumerable<double> values)
        {
            foreach (double num in values)
                this.Push(num);
        }

        public static RunningStatistics Combine(
          RunningStatistics a,
          RunningStatistics b)
        {
            if (a._n == 0L)
                return b;
            if (b._n == 0L)
                return a;
            long num1 = a._n + b._n;
            double num2 = b._m1 - a._m1;
            double num3 = num2 * num2;
            double num4 = num3 * num2;
            double num5 = num3 * num3;
            double num6 = ((double)a._n * a._m1 + (double)b._n * b._m1) / (double)num1;
            double num7 = a._m2 + b._m2 + num3 * (double)a._n * (double)b._n / (double)num1;
            double num8 = a._m3 + b._m3 + num4 * (double)a._n * (double)b._n * (double)(a._n - b._n) / (double)(num1 * num1) + 3.0 * num2 * ((double)a._n * b._m2 - (double)b._n * a._m2) / (double)num1;
            double num9 = a._m4 + b._m4 + num5 * (double)a._n * (double)b._n * (double)(a._n * a._n - a._n * b._n + b._n * b._n) / (double)(num1 * num1 * num1) + 6.0 * num3 * ((double)(a._n * a._n) * b._m2 + (double)(b._n * b._n) * a._m2) / (double)(num1 * num1) + 4.0 * num2 * ((double)a._n * b._m3 - (double)b._n * a._m3) / (double)num1;
            double num10 = Math.Min(a._min, b._min);
            double num11 = Math.Max(a._max, b._max);
            return new RunningStatistics()
            {
                _n = num1,
                _m1 = num6,
                _m2 = num7,
                _m3 = num8,
                _m4 = num9,
                _min = num10,
                _max = num11
            };
        }

        public static RunningStatistics operator +(
          RunningStatistics a,
          RunningStatistics b)
        {
            return RunningStatistics.Combine(a, b);
        }
    }
}
