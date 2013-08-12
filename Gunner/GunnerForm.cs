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
            timerUpdate.Enabled = true;
            scaleControl.OnValueChanged += scaleControl_ValueChanged;
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
            var credentials = LogonDialog.Execute(client.GetShipNames());
            if (credentials == null)
                Close();
            else
            {
                client.Login(credentials.Nation, credentials.ShipName);
                helm = client.GetHelm();
                Text = helm.Ship.Name;
                tableLayoutPanel.Visible = true;
                spaceGridControl.OwnShip = helm.Ship;
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
            if (helm != null)
                GetData();
            spaceGridControl.Invalidate();
        }

        private void GetData()
        {
            spaceGridControl.Ships = client.GetVisibleShips().ToList();
            var s = helm.Ship.S;
            var h = helm.Ship.Heading;
            spaceGridControl.Origin = s;
            spaceGridControl.Rotation = h; 
        }

        private void scaleControl_ValueChanged(object sender, EventArgs e)
        {
            spaceGridControl.WorldScale = scaleControl.Value;
        }
    }
}
