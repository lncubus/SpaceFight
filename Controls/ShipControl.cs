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
    public partial class ShipControl : UserControl
    {
        public ShipControl()
        {
            InitializeComponent();
        }

        public bool ReadOnly
        {
            get { return headingControl.ReadOnly || rollControl.ReadOnly; }
            set { headingControl.ReadOnly = value; rollControl.ReadOnly = value; accelerationTrackBar.Enabled = !value; }
        }

        public IShip Ship
        {
            get { return m_helm == null ? null : m_helm.Ship; }
        }

        public IHelm Helm
        {
            get { return m_helm; }
            set
            {
                if (m_helm == value)
                    return;
                m_helm = value;
                UpdateControls();
            }
        }

        private IHelm m_helm;
        private int Updating;
        
        public void UpdateControls()
        {
            try
            {
                Updating++;
                InternalUpdateControls();
            }
            finally
            {
                Updating--;
            }
        }

        private void InternalUpdateControls()
        {
            if (Ship != null)
            {
                accelerationProgressBar.Value =
                    Ship.Class == null ? 0 : 
                        (int)MathUtils.LimitedLinear(accelerationProgressBar.Minimum, accelerationProgressBar.Maximum, Ship.Acceleration / Ship.Class.MaximumAcceleration);
                headingControl.Heading = MathUtils.ToDegreesInt(Ship.Heading);
                rollControl.Roll = MathUtils.ToDegreesInt(Ship.Roll);
            }
            else
            {
                headingControl.Heading = 0;
                rollControl.Roll = 0;
                accelerationProgressBar.Value = 0;
            }
            if (Helm != null)
            {
                accelerationTrackBar.Value =
                    Ship.Class == null ? 0 :
                        (int)MathUtils.LimitedLinear(accelerationTrackBar.Minimum, accelerationTrackBar.Maximum, Helm.AccelerateTo / Ship.Class.MaximumAcceleration);
                headingControl.HeadingTo = MathUtils.ToDegreesInt(Helm.HeadingTo);
                rollControl.RollTo = MathUtils.ToDegreesInt(Helm.RollTo);
            }
            else
            {
                headingControl.HeadingTo = 0;
                rollControl.RollTo = 0;
                accelerationTrackBar.Value = 0;
            }
        }

        private void accelerationTrackBar_ValueChanged(object sender, EventArgs e)
        {
            if (Helm == null || Updating > 0)
                return;
            Helm.AccelerateTo = accelerationTrackBar.Value * Ship.Class.MaximumAcceleration / accelerationTrackBar.Maximum;
        }

        private void headingControl_OnHeadingToChanged(object sender, EventArgs e)
        {
            if (Helm == null || Updating > 0)
                return;
            Helm.HeadingTo = MathUtils.ToRadians(headingControl.HeadingTo);
        }

        private void rollControl_OnRollToChanged(object sender, EventArgs e)
        {
            if (Helm == null || Updating > 0)
                return;
            Helm.RollTo = MathUtils.ToRadians(rollControl.RollTo);
       }
    }
}
