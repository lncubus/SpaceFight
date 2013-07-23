using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SF.Space;

namespace SF.Controls
{
    public partial class LogonDialog : Form
    {
        public LogonDialog()
        {
            InitializeComponent();
        }

        public class Logon
        {
            public string Nation;
            public string ShipName;
            public string Password;
        }

        private IDictionary<string, ICollection<string>> ShipNames;

        public static Logon Execute(IDictionary<string, ICollection<string>> ships)
        {
            var dialog = new LogonDialog
            {
                ShipNames = ships,
            };
            if (dialog.ShowDialog() != DialogResult.OK)
                return null;
            return new Logon
            {
                Nation = dialog.comboBoxState.Text,
                ShipName = dialog.comboBoxShip.Text,
                Password = string.Empty,
            };
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
            comboBoxShip.Items.AddRange(ShipNames[nation].ToArray());
        }

        private void comboBoxShip_SelectedValueChanged(object sender, EventArgs e)
        {
            buttonOkay.Enabled = !string.IsNullOrEmpty(comboBoxShip.Text);
        }
    }
}
