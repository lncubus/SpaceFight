using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace SF.Controls
{

    public partial class LogonDialog : Form
    {
        public LogonDialog()
        {
            InitializeComponent();
        }

        private IDictionary<string, IDictionary<string, int>> ShipNames;

        public static int Execute(IDictionary<string, IDictionary<string, int>> ships)
        {
            var dialog = new LogonDialog
            {
                ShipNames = ships,
            };
            if (dialog.ShowDialog() != DialogResult.OK)
                return 0;
            return ships[dialog.comboBoxState.Text][dialog.comboBoxShip.Text];
        }

        private void buttonOkay_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBoxState.Text) && !string.IsNullOrEmpty(comboBoxShip.Text))
                DialogResult = DialogResult.OK;
        }

        private void LogonDialog_Load(object sender, EventArgs e)
        {
            comboBoxState.Items.Clear();
            comboBoxState.Items.AddRange(ShipNames.Keys.ToArray());
        }

        private void comboBoxState_SelectedValueChanged(object sender, EventArgs e)
        {
            buttonOkay.Enabled = false;
            comboBoxShip.Items.Clear();
            string nation = comboBoxState.Text;
            if (string.IsNullOrEmpty(nation))
                return;
            comboBoxShip.Items.AddRange(ShipNames[nation].Keys.ToArray());
        }

        private void comboBoxShip_SelectedValueChanged(object sender, EventArgs e)
        {
            buttonOkay.Enabled = !string.IsNullOrEmpty(comboBoxShip.Text);
        }
    }
}
