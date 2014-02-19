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
            scaleControl.OnValueChanged += scaleControl_ValueChanged;
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

        private IHelm helm;
        private SF.ClientLibrary.SpaceClient client;

        private const int TrajectorySize = 250;
        private SpaceGridControl.Curve Trajectory = new SpaceGridControl.Curve
            {
                Pencil = Pens.DarkGreen
            };

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
            var credentials = LogonDialog.Execute(client.GetShipNames());
            if (credentials == null)
            {
                Close();
                return;
            }
            if (!client.Login(credentials.Nation, credentials.ShipName))
            {
                Close();
                return;
            }
            shipControl.Helm = helm = client.GetHelm();
            Text = helm.Name;
            spaceGridControl.OwnShip = helm;
            spaceGridControl.Curves.Add(Trajectory);
            spaceGridControl.WorldScale = Catalog.Instance.DefaultScale;
            scaleControl.Value = Catalog.Instance.DefaultScale;
            tableLayoutPanel.Visible = true;
            timerUpdate.Enabled = true;
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            if (helm == null)
            {
                timerUpdate.Enabled = false;
                Login();
            }
            if (helm == null)
                return;
            client.Update();
            shipControl.UpdateControls();
            if (helm != null)
                GetData();
            spaceGridControl.Invalidate();
        }

        private void GetData()
        {
            if (helm.State == ShipState.Annihilated || helm.State == ShipState.Junk)
            {
                Die();
                return;
            }
            if (helm.State == ShipState.Hyperspace || !string.IsNullOrEmpty(helm.Carrier))
            {
                tableLayoutPanel.Visible = false;
                labelMessage.Visible = true;
                return;
            }
            if (!tableLayoutPanel.Visible)
            {
                labelMessage.Visible = false;
                tableLayoutPanel.Visible = true;
            }
            spaceGridControl.Ships = client.GetVisibleShips();
            spaceGridControl.Stars = client.GetStars();
            spaceGridControl.Missiles = client.GetVisibleMissiles();
            spaceGridControl.Origin = helm.Position;
            if (Trajectory.Count == 0 || Trajectory[Trajectory.Count - 1] != helm.Position)
                Trajectory.Add(helm.Position);
            if (Trajectory.Count > TrajectorySize)
                Trajectory.RemoveRange(0, Trajectory.Count - TrajectorySize);
            var ship = spaceGridControl.Selected ?? helm;
            indicatorControl.Acceleration = ship.Acceleration;
            indicatorControl.Speed = ship.Speed;
            indicatorControl.Position = ship.Position;
        }

        private void Die()
        {
            tableLayoutPanel.Hide();
            BackColor = Color.Maroon;
            timerUpdate.Enabled = false;
            MessageBox.Show(this, CommonResources.DeadMessage);
            Close();
        }

        private void scaleControl_ValueChanged(object sender, EventArgs e)
        {
            spaceGridControl.WorldScale = scaleControl.Value;
        }

        private void ClientForm_KeyPress(object sender, KeyPressEventArgs e)
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
