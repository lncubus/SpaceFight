using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SF.Controls;
using SF.Space;

namespace Client
{
    public partial class ClientForm : Form
    {
        public ClientForm()
        {
            InitializeComponent();
            tableLayoutPanel.Visible = false;
            timerUpdate.Enabled = true;
            //scaleControl.OnValueChanged += scaleControl_ValueChanged;
            spaceGridControl.Options = 
                SpaceGridControl.DrawingOptions.FriendlyMissileCircles | SpaceGridControl.DrawingOptions.FriendlyVulnerableSectors |
                SpaceGridControl.DrawingOptions.HostileVulnerableSectors |
                SpaceGridControl.DrawingOptions.MyMissileCircles | SpaceGridControl.DrawingOptions.MyVulnerableSectors;
            spaceGridControl.Selectable =
                SpaceGridControl.SelectableObjects.Missiles | 
                SpaceGridControl.SelectableObjects.Stars |
                SpaceGridControl.SelectableObjects.Ships;
            Controls.Add(labelMessage);
        }

        //private IHelm helm;
        private SF.ClientLibrary.SpaceClient client;

        //private const int TrajectorySize = 250;
        //private SpaceGridControl.Curve Trajectory = new SpaceGridControl.Curve
        //    {
        //        Pencil = Pens.DarkGreen
        //    };

        private Label labelMessage = new Label
        {
            Dock = DockStyle.Fill,
            Location = new Point(0, 40),
            Name = "labelMessage",
            TextAlign = ContentAlignment.MiddleCenter,
            Text = CommonResources.NoControl,
            Visible = false,
        };

        private void Login()
        {
            client = new SF.ClientLibrary.SpaceClient();
            var credentials = LogonDialog.Execute(client.GetShipRegistry());
            if (credentials == 0)
            {
                Close();
                return;
            }
            if (!client.Login(credentials))
            {
                Close();
                return;
            }
            //shipControl.Helm = helm = client.GetHelm();
            //Text = helm.Name;
            //spaceGridControl.OwnShip = helm;
            //spaceGridControl.Curves.Add(Trajectory);
            //spaceGridControl.WorldScale = Catalog.Instance.DefaultScale;
            //scaleControl.Value = Catalog.Instance.DefaultScale;
            tableLayoutPanel.Visible = true;
            timerUpdate.Enabled = true;
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            //if (helm == null)
            //{
            //    timerUpdate.Enabled = false;
            //    Login();
            //}
            //if (helm == null)
            //    return;
            //client.Update();
            //shipControl.UpdateControls();
            //if (helm != null)
            //    GetData();
            //spaceGridControl.Invalidate();
        }
    }
}
