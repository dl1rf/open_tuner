using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vortice.MediaFoundation;

namespace opentuner.MediaSources.Longmynd
{
    public partial class LongmyndSettingsForm : Form
    {

        private LongmyndSettings _settings;
        public LongmyndSettingsForm(ref LongmyndSettings Settings)
        {
            InitializeComponent();
            _settings = Settings;

            comboHardwareInterface.SelectedIndex = _settings.DefaultInterface;
            txtTSPort.Text = _settings.TS_Port.ToString();
            txtWSIpAddress.Text = _settings.LongmyndWSHost;
            txtWSPort.Text = _settings.LongmyndWSPort.ToString();
            txtMqttIpAddress.Text = _settings.LongmyndMqttHost;
            txtMqttPort.Text = _settings.LongmyndMqttPort.ToString();
            txtBaseCmdTopic.Text = _settings.CmdTopic;
            txtTuner1Frequency.Text = _settings.DefaultFrequency.ToString();
            txtTuner1FreqOffset.Text = _settings.DefaultOffset.ToString();
            txtTuner1SymbolRate.Text = _settings.DefaultSR.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int tsport = 0;
            if (!int.TryParse(txtTSPort.Text, out tsport))
            {
                MessageBox.Show("Invalid TS Port");
                return;
            }

            int wsport = 0;
            if (!int.TryParse(txtWSPort.Text, out wsport))
            {
                MessageBox.Show("Invalid WS Port");
                return;
            }

            int mqttport = 0;
            if (!int.TryParse(txtMqttPort.Text, out mqttport))
            {
                MessageBox.Show("Invalid Mqtt Port");
                return;
            }

            uint frequency = 0;
            if (!uint.TryParse(txtTuner1Frequency.Text, out frequency))
            {
                MessageBox.Show("Invalid Frequency");
                return;
            }

            uint offset = 0;
            if (!uint.TryParse(txtTuner1FreqOffset.Text, out offset))
            {
                MessageBox.Show("Invalid Offset");
                return;
            }

            if (frequency <= offset)
            {
                MessageBox.Show("Invalid Offset");
                return;
            }

            uint symbolrate = 0;
            if (!uint.TryParse(txtTuner1SymbolRate.Text, out symbolrate))
            {
                MessageBox.Show("Invalid Symbol Rate");
                return;
            }

            _settings.DefaultInterface = (byte)comboHardwareInterface.SelectedIndex;
            _settings.TS_Port = tsport;
            _settings.LongmyndWSHost = txtWSIpAddress.Text;
            _settings.LongmyndWSPort = wsport;
            _settings.LongmyndMqttHost = txtMqttIpAddress.Text;
            _settings.LongmyndMqttPort  = mqttport;
            _settings.CmdTopic = txtBaseCmdTopic.Text;
            _settings.DefaultFrequency = frequency;
            _settings.DefaultOffset = offset;
            _settings.DefaultSR = symbolrate;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
