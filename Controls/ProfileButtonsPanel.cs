using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SF.Space;

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

        private Vector[][] m_trajectories;
        public Vector[][] Trajectories
        {
            get
            {
                return m_trajectories ?? new Vector[0][];
            }
            set
            {
                m_trajectories = value;
                curvesControl.Curves.Clear();
                foreach (var trajectoory in Trajectories)
                {
                    var curve = new SpaceGridControl.Curve();
                    curve.Pencil = Pens.Crimson;
                    curve.AddRange(trajectoory);
                    curvesControl.Curves.Add(curve);
                }
                curvesControl.Invalidate();
            }
        }

        private void buttonAttackProfile_Click(object sender, EventArgs e)
        {
            using (var dialog = new ProfileEditorForm())
            {
                dialog.Trajectories = Trajectories;
                if (DialogResult.OK != dialog.ShowDialog())
                    return;
                Trajectories = dialog.Trajectories;
            }
        }
    }
}
