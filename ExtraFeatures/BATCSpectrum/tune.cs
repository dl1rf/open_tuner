using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static opentuner.ExtraFeatures.BATCSpectrum.signal;

namespace opentuner.ExtraFeatures.BATCSpectrum
{
    class spectrumTune
    {
        public float[] fft_centre;  // signal center position in spectrum
        public float[] fft_width;   // signal width
        public bool[] locked;       // demode locked state
        public bool[] switching;    // switching flag

        public Timer tuneTimer;

        private int fft_data_length = 918;
        private int tuners;
        private Object lock_item;

        private DateTime[] last_tuned_time = new DateTime[8];   //time the last signal was tuned

        public spectrumTune(object _lock_item, int _tuners)
        {
            this.lock_item = _lock_item;
            this.tuners = _tuners;
            this.fft_centre = new float[_tuners];
            this.fft_width = new float[_tuners];
            this.locked = new bool[_tuners];
            this.switching = new bool[_tuners];

            this.tuneTimer = new Timer();
            this.tuneTimer.Enabled = false;
            this.tuneTimer.Interval = 1500;
            this.tuneTimer.Tick += new System.EventHandler(this.tuneTimer_Tick);
        }

        private void tuneTimer_Tick(object sender, EventArgs e)
        {
//            int mode = tuneMode;
//            float spectrum_w = spectrum.Width;
//            float spectrum_wScale = spectrum_w / fft_data_length;
            ushort autotuneWait = 30;

//            Tuple<signal.Sig, int> ret = sigs.tune(mode, Convert.ToInt16(autotuneWait), 0);
//            if (ret.Item1.frequency > 0)      //above 0 is a change in signal
//            {
//                System.Threading.Thread.Sleep(100);
//                selectSignal(Convert.ToInt32(ret.Item1.fft_centre * spectrum_wScale), 0);
//                sigs.set_tuned(ret.Item1, 0);
//                rx_blocks[0, 0] = Convert.ToInt16(ret.Item1.fft_centre);
//                rx_blocks[0, 1] = Convert.ToInt16(ret.Item1.fft_stop - ret.Item1.fft_start);
//            }
        }

        //public void changeTuneMode(int mode)
        //{
        //    tuneMode = mode;

        //    if (mode == 0)
        //    {
        //        tuneTimer?.Stop();
        //    }
        //    else
        //    {
        //        tuneTimer?.Start();
        //    }
        //}

    }
}
