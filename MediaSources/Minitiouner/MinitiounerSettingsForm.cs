using System;
using System.Windows.Forms;

namespace opentuner.MediaSources.Minitiouner
{
    public partial class MinitiounerSettingsForm : Form
    {
        private MinitiounerSettings _settings;
        public MinitiounerSettingsForm(ref MinitiounerSettings Settings)
        {
            InitializeComponent();
            _settings = Settings;

            comboHardwareInterface.SelectedIndex = _settings.DefaultInterface;
            txtTuner1Frequency.Text = _settings.DefaultFrequency[0].ToString();
            txtTuner1FreqOffset.Text = _settings.DefaultOffset[0].ToString();
            txtTuner1SymbolRate.Text = _settings.DefaultSR[0].ToString();
            txtTuner2Frequency.Text = _settings.DefaultFrequency[1].ToString();
            txtTuner2FreqOffset.Text = _settings.DefaultOffset[1].ToString();
            txtTuner2SymbolRate.Text = _settings.DefaultSR[1].ToString();
            comboSupplyADefault.SelectedIndex = _settings.DefaultLnbASupply;
            comboSupplyBDefault.SelectedIndex = _settings.DefaultLnbBSupply;
            ComboDefaultRFInput.SelectedIndex = _settings.DefaultRFInput;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
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

            _settings.DefaultInterface = (byte)comboHardwareInterface.SelectedIndex;
            _settings.DefaultLnbASupply = (byte)comboSupplyADefault.SelectedIndex;
            _settings.DefaultLnbBSupply = (byte)comboSupplyBDefault.SelectedIndex;
            _settings.DefaultRFInput = (byte)ComboDefaultRFInput.SelectedIndex;

            _settings.DefaultFrequency[0] = frequency1;
            _settings.DefaultOffset[0] = offset1;
            _settings.DefaultSR[0] = sr1;
            _settings.DefaultFrequency[1] = frequency2;
            _settings.DefaultOffset[1] = offset2;
            _settings.DefaultSR[1] = sr2;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
