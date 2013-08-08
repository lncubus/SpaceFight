using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SF.Space;
using SF.Controls;

namespace Gunner
{
    public partial class GunnerForm : Form
    {
        public GunnerForm()
        {
            InitializeComponent();
            tableLayoutPanel.Visible = false;
            labelScale.Text = MathUtils.NumberToText(spaceGridControl.WorldScale, Km);
            timerUpdate.Enabled = true;
            spaceGridControl.VulnerableSectors.Hostile = Pens.Black;
            spaceGridControl.VulnerableSectors.Friendly = Pens.Black;
            spaceGridControl.Options =
                SpaceGridOptions.FriendlyVulnerableSectors |
                SpaceGridOptions.HostileVulnerableSectors |
                SpaceGridOptions.MyMissleCircles |
                SpaceGridOptions.FriendlySectorsByMyMissleRange;
        }

        private IHelm helm;
        private SF.ClientLibrary.SpaceClient client;

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
                helm = client.GetHelm(credentials.Nation, credentials.ShipName);
                Text = helm.Ship.Name;
                tableLayoutPanel.Visible = true;
                spaceGridControl.OwnShip = helm.Ship;
                ScaleFactor = Math.Log10(spaceGridControl.WorldScale);
                timerUpdate.Enabled = true;
            }
        }

        public const string Km = "км";
        public const double MinimalScale = 1000;
        public const double MaximalScale = 1000000000000;

        private double ScaleFactor;

        private void GetData()
        {
            spaceGridControl.Ships = client.GetVisibleShips();
            var s = helm.Ship.S;
            var h = helm.Ship.Heading;
            spaceGridControl.Origin = s;
            spaceGridControl.Rotation = h; 
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            if (helm == null)
            {
                timerUpdate.Enabled = false;
                Login();
            }
            client.Update();
            if (helm != null)
                GetData();
            spaceGridControl.Invalidate();
        }

        private void buttonZoomIn_Click(object sender, EventArgs e)
        {
            if (spaceGridControl.WorldScale <= MinimalScale * 2)
                return;
            ScaleFactor -= 0.5;
            spaceGridControl.WorldScale = Math.Exp(ScaleFactor * MathUtils.Ln10);
            labelScale.Text = MathUtils.NumberToText(spaceGridControl.WorldScale, Km);
        }

        private void buttonZoomOut_Click(object sender, EventArgs e)
        {
            if (spaceGridControl.WorldScale >= MaximalScale / 2)
                return;
            ScaleFactor += 0.5;
            spaceGridControl.WorldScale = Math.Exp(ScaleFactor * MathUtils.Ln10);
            labelScale.Text = MathUtils.NumberToText(spaceGridControl.WorldScale, Km);
        }
    }
}
