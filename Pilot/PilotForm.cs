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

namespace Pilot
{
    public partial class PilotForm : Form
    {
        public PilotForm()
        {
            InitializeComponent();
            tableLayoutPanel.Visible = false;
            timerUpdate.Enabled = true;
            scaleControl.OnValueChanged += scaleControl_ValueChanged;
            spaceGridControl.Options = 
                SpaceGridOptions.FriendlyMissileCircles | SpaceGridOptions.FriendlyVulnerableSectors |
                SpaceGridOptions.HostileVulnerableSectors |
                SpaceGridOptions.MyMissileCircles | SpaceGridOptions.MyVulnerableSectors;
        }

        private IHelm helm;
        private SF.ClientLibrary.SpaceClient client;

        private const int TrajectorySize = 250;
        private SpaceGridControl.Curve Trajectory = new SpaceGridControl.Curve
            {
                Pencil = Pens.DarkGreen
            };

        private void Login()
        {
            client = new SF.ClientLibrary.SpaceClient();
            var credentials = LogonDialog.Execute(client.GetShipNames());
            if (credentials == null)
                Close();
            else
            {
                client.Login(credentials.Nation, credentials.ShipName);
                shipControl.Helm = helm = client.GetHelm();
                Text = helm.Ship.Name;
                spaceGridControl.OwnShip = helm.Ship;
                spaceGridControl.Curves.Add(Trajectory);
                spaceGridControl.WorldScale = Catalog.Instance.DefaultScale;
                scaleControl.Value = Catalog.Instance.DefaultScale; ;
                tableLayoutPanel.Visible = true;
                timerUpdate.Enabled = true;
            }
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            if (helm == null)
            {
                timerUpdate.Enabled = false;
                Login();
            }
            client.Update();
            shipControl.UpdateControls();
            if (helm != null)
                GetData();
            spaceGridControl.Invalidate();
        }

        private void GetData()
        {
            spaceGridControl.Ships = client.GetVisibleShips().ToList();
            spaceGridControl.Stars = client.GetStars().ToList();
            spaceGridControl.Missiles = client.GetVisibleMissiles().ToList();
            spaceGridControl.Origin = helm.Ship.S;
            if (Trajectory.Count == 0 || Trajectory[Trajectory.Count - 1] != helm.Ship.S)
                Trajectory.Add(helm.Ship.S);
            if (Trajectory.Count > TrajectorySize)
                Trajectory.RemoveRange(0, Trajectory.Count - TrajectorySize);
            var ship = spaceGridControl.SelectedShip ?? helm.Ship;
            indicatorControl.Acceleration = ship.A;
            indicatorControl.Speed = ship.V;
            indicatorControl.Position = ship.S;
        }

        private void scaleControl_ValueChanged(object sender, EventArgs e)
        {
            spaceGridControl.WorldScale = scaleControl.Value;
        }

        private void PilotForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            switch (e.KeyChar)
            {
                case '+':
                case '=':
                    scaleControl.ZoomIn();
                    break;
                case '-':
                case '_':
                    scaleControl.ZoomOut();
                    break;
                default:
                    e.Handled = false;
                    break;
            }
        }
    }
}
