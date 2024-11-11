using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace opentuner.ExtraFeatures.BATCSpectrum
{
    public class BATCSpectrumSettings
    {
        public int[] tuneMode = { 1, 1, 1, 1 };
        public bool[] avoidBeacon = { true, true, true, true };
        public int overPowerIndicatorLayout = 0;
    }
}
