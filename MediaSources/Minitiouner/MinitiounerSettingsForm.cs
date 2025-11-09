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
            txtTuner1FreqOffset.Text = _settings.DefaultOffset[0].ToString();
            txtTuner2FreqOffset.Text = _settings.DefaultOffset[1].ToString();
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
            uint offset1 = 0;
            if (!uint.TryParse(txtTuner1FreqOffset.Text, out offset1))
            {
                MessageBox.Show("Invalid Offset 1");
                return;
            }

            uint offset2 = 0;
            if (!uint.TryParse(txtTuner2FreqOffset.Text, out offset2))
            {
                MessageBox.Show("Invalid Offset 2");
                return;
            }

            _settings.DefaultInterface = (byte)comboHardwareInterface.SelectedIndex;
            _settings.DefaultLnbASupply = (byte)comboSupplyADefault.SelectedIndex;
            _settings.DefaultLnbBSupply = (byte)comboSupplyBDefault.SelectedIndex;
            _settings.DefaultRFInput = (byte)ComboDefaultRFInput.SelectedIndex;

            _settings.DefaultOffset[0] = offset1;
            _settings.DefaultOffset[1] = offset2;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
