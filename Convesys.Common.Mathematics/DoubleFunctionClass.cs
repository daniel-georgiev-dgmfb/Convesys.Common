using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twilight.Platform.Common.Mathematics
{
    public class DoubleFunctionClass
    {
        /// <summary>
        /// A function that takes double and returns a double.
        /// </summary>
        public delegate Task<double> DoubleFunction(double d);
    }
}
