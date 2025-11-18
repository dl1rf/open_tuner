using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace opentuner.MediaSources.WinterHill
{
    public partial class WinterHillSettingsForm : Form
    {
        private WinterHillSettings _settings;

        public WinterHillSettingsForm(WinterHillSettings Settings)
        {
            InitializeComponent();

            _settings = Settings;

            comboDefaultInterface.SelectedIndex = _settings.DefaultInterface;
            txtWHWSIp.Text = _settings.WinterHillWSHost;
            txtWHWSPort.Text = _settings.WinterHillWSPort.ToString();
            txtWHWSBaseUdp.Text = _settings.WinterHillWSUdpBasePort.ToString();

            txtUDPBasePort.Text = _settings.WinterHillUdpBasePort.ToString();
            txtUDPIP.Text = _settings.WinterHillUdpHost.ToString();

            txtTuner1Frequency.Text = _settings.DefaultFrequency[0].ToString();
            txtTuner1FreqOffset.Text = _settings.DefaultOffset[0].ToString();
            txtTuner1SymbolRate.Text = _settings.DefaultSR[0].ToString();
            txtTuner2Frequency.Text = _settings.DefaultFrequency[1].ToString();
            txtTuner2FreqOffset.Text = _settings.DefaultOffset[1].ToString();
            txtTuner2SymbolRate.Text = _settings.DefaultSR[1].ToString();
            txtTuner3Frequency.Text = _settings.DefaultFrequency[2].ToString();
            txtTuner3FreqOffset.Text = _settings.DefaultOffset[2].ToString();
            txtTuner3SymbolRate.Text = _settings.DefaultSR[2].ToString();
            txtTuner4Frequency.Text = _settings.DefaultFrequency[3].ToString();
            txtTuner4FreqOffset.Text = _settings.DefaultOffset[3].ToString();
            txtTuner4SymbolRate.Text = _settings.DefaultSR[3].ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int wsport = 0;
            int baseport = 0;

            int udpbaseport = 0;

            if (!int.TryParse(txtWHWSPort.Text, out wsport))
            {
                MessageBox.Show("WinterHill WS Port is not valid.");
                return;
            }

            if (!int.TryParse(txtWHWSBaseUdp.Text, out baseport))
            {
                MessageBox.Show("WinterHill WS Base Port is not valid");
                return;
            }

            if (!int.TryParse(txtUDPBasePort.Text, out udpbaseport))
            {
                MessageBox.Show("WinterHill UDP Base Port is not valid");
                return;
            }

            _settings.WinterHillWSHost = txtWHWSIp.Text;
            _settings.WinterHillWSPort = wsport;
            _settings.WinterHillWSUdpBasePort = baseport;

            _settings.WinterHillUdpBasePort = udpbaseport;
            _settings.WinterHillUdpHost = txtUDPIP.Text;
            _settings.DefaultInterface = (byte)comboDefaultInterface.SelectedIndex;

            uint frequency1 = 0;
            if (!uint.TryParse(txtTuner1Frequency.Text, out frequency1))
            {
                MessageBox.Show("Invalid Frequency 1");
                return;
            }
            uint offset1 = 0;
            if (!uint.TryParse(txtTuner1FreqOffset.Text, out offset1))
            {
                MessageBox.Show("Invalid Offset 1");
                return;
            }
            if (frequency1 <= offset1)
            {
                MessageBox.Show("Invalid Offset 1");
                return;
            }
            uint sr1 = 0;
            if (!uint.TryParse(txtTuner1SymbolRate.Text, out sr1))
            {
                MessageBox.Show("Invalid Symbol Rate 1");
                return;
            }

            uint frequency2 = 0;
            if (!uint.TryParse(txtTuner2Frequency.Text, out frequency2))
            {
                MessageBox.Show("Invalid Frequency 2");
                return;
            }
            uint offset2 = 0;
            if (!uint.TryParse(txtTuner2FreqOffset.Text, out offset2))
            {
                MessageBox.Show("Invalid Offset 2");
                return;
            }
            if (frequency2 <= offset2)
            {
                MessageBox.Show("Invalid Offset 2");
                return;
            }
            uint sr2 = 0;
            if (!uint.TryParse(txtTuner2SymbolRate.Text, out sr2))
            {
                MessageBox.Show("Invalid Symbol Rate 2");
                return;
            }

            uint frequency3 = 0;
            if (!uint.TryParse(txtTuner3Frequency.Text, out frequency3))
            {
                MessageBox.Show("Invalid Frequency 3");
                return;
            }
            uint offset3 = 0;
            if (!uint.TryParse(txtTuner3FreqOffset.Text, out offset3))
            {
                MessageBox.Show("Invalid Offset 3");
                return;
            }
            if (frequency3 <= offset3)
            {
                MessageBox.Show("Invalid Offset 3");
                return;
            }
            uint sr3 = 0;
            if (!uint.TryParse(txtTuner3SymbolRate.Text, out sr3))
            {
                MessageBox.Show("Invalid Symbol Rate 3");
                return;
            }

            uint frequency4 = 0;
            if (!uint.TryParse(txtTuner4Frequency.Text, out frequency4))
            {
                MessageBox.Show("Invalid Frequency 4");
                return;
            }
            uint offset4 = 0;
            if (!uint.TryParse(txtTuner4FreqOffset.Text, out offset4))
            {
                MessageBox.Show("Invalid Offset 4");
                return;
            }
            if (frequency4 <= offset4)
            {
                MessageBox.Show("Invalid Offset 4");
                return;
            }
            uint sr4 = 0;
            if (!uint.TryParse(txtTuner4SymbolRate.Text, out sr4))
            {
                MessageBox.Show("Invalid Symbol Rate 4");
                return;
            }

            _settings.DefaultFrequency[0] = frequency1;
            _settings.DefaultOffset[0] = offset1;
            _settings.DefaultSR[0] = sr1;
            _settings.DefaultFrequency[1] = frequency2;
            _settings.DefaultOffset[1] = offset2;
            _settings.DefaultSR[1] = sr2;
            _settings.DefaultFrequency[2] = frequency3;
            _settings.DefaultOffset[2] = offset3;
            _settings.DefaultSR[2] = sr3;
            _settings.DefaultFrequency[3] = frequency4;
            _settings.DefaultOffset[3] = offset4;
            _settings.DefaultSR[3] = sr4;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void comboDefaultInterface_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void btnBroadcastListener_Click(object sender, EventArgs e)
        {
            PicoWHBroadcastListenerForm picoWHBroadcastListenerForm = new PicoWHBroadcastListenerForm();
            picoWHBroadcastListenerForm.Show();
        }
    }
}
