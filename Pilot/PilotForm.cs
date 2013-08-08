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
                SpaceGridOptions.FriendlyMissleCircles | SpaceGridOptions.FriendlyVulnerableSectors |
                SpaceGridOptions.HostileVulnerableSectors |
                SpaceGridOptions.MyMissleCircles | SpaceGridOptions.MyVulnerableSectors;
        }

        private IHelm helm;
        private SF.ClientLibrary.SpaceClient client;

        private const int TrajectorySize = 2500;
        private SpaceGridControl.Curve Trajectory = new SpaceGridControl.Curve
            {
                Pencil = Pens.DarkGreen
            };

        private void Login()
        {
            client = new SF.ClientLibrary.SpaceClient();

            var shipNames = new SortedDictionary<string, ICollection<string>>();
            foreach (var nation in client.GetNations())
                shipNames.Add(nation, client.GetShipNames(nation).ToList());
            var credentials = LogonDialog.Execute(shipNames);
            if (credentials == null)
                Close();
            else
            {
                shipControl.Helm = helm = client.GetHelm(credentials.Nation, credentials.ShipName);
                Text = helm.Ship.Name;
                tableLayoutPanel.Visible = true;
                spaceGridControl.OwnShip = helm.Ship;
                spaceGridControl.Curves.Add(Trajectory);
                spaceGridControl.WorldScale = scaleControl.Value;
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
            spaceGridControl.Ships = client.GetVisibleShips();

            var s = helm.Ship.S;
            var v = helm.Ship.V;
            spaceGridControl.Origin = s;
            if (Trajectory.Count == 0 || Trajectory[Trajectory.Count - 1] != s)
                Trajectory.Add(s);
            if (Trajectory.Count > TrajectorySize)
                Trajectory.RemoveRange(0, Trajectory.Count - TrajectorySize);
        }

        private void scaleControl_ValueChanged(object sender, EventArgs e)
        {
            spaceGridControl.WorldScale = scaleControl.Value;
        }
    }
}
