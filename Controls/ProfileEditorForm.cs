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
    public partial class ProfileEditorForm : Form
    {
        public ProfileEditorForm()
        {
            InitializeComponent();
        }

        private void curveEditorControl_Resize(object sender, EventArgs e)
        {
            curveEditorControl.Invalidate();
        }

        public Vector[][] Trajectories
        {
            get
            {
                return curveEditorControl.Trajectories;
            }
            set
            {
                curveEditorControl.Trajectories = value;
            }
        }
    }
}
