using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convesys.Common.Analytics.Verification
{
    using np = numpy;

    using stats = scipy.stats;

    public static class Module
    {

        public static object x = np.linspace(-15, 15, 9);

        static Module() => stats.kstest(x, "norm");
    }
}
