using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SF.Controls
{
    public partial class ProfileButtonsPanel : UserControl
    {
        public ProfileButtonsPanel()
        {
            InitializeComponent();
            // dirty random
            comboBoxJammer.SelectedIndex = (DateTime.Now.Millisecond % comboBoxJammer.Items.Count);
        }

        public SpaceGridControl.Curve[] Curves
        {
            get
            {
                return curvesControl.Curves.ToArray();
            }
        }

        public int Jammer
        {
            get
            {
                return comboBoxJammer.SelectedIndex;
            }
            set
            {
                comboBoxJammer.SelectedIndex = value;
            }
        }

        private void buttonAttackProfile_Click(object sender, EventArgs e)
        {
            using (var dialog = new ProfileEditorForm())
            {
                if (DialogResult.OK != dialog.ShowDialog())
                    return;
                //dialog.
            }
        }
    }
}
