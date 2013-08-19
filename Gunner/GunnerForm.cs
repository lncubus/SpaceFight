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
                SpaceGridOptions.MyMissileCircles |
                SpaceGridOptions.FriendlySectorsByMyMissileRange;
        }

        private IHelm helm;
        private SF.ClientLibrary.SpaceClient client;
        private bool m_left;

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
            spaceGridControl.Missiles = client.GetVisibleMissiles().ToList();
            var s = helm.Ship.S;
            var h = helm.Ship.Heading;
            spaceGridControl.Origin = s;
            spaceGridControl.Rotation = h;
        }

        private void scaleControl_ValueChanged(object sender, EventArgs e)
        {
            spaceGridControl.WorldScale = scaleControl.Value;
        }

        private void spaceGridControl_ShipSelected(object sender, EventArgs e)
        {
            CheckCanFire();
        }

        private void checkBoxFriendlyFire_CheckedChanged(object sender, EventArgs e)
        {
            CheckCanFire();
        }

        private void buttonFire_Click(object sender, EventArgs e)
        {
            Fire();
        }

        private void spaceGridControl_DoubleClick(object sender, EventArgs e)
        {
            if (buttonFire.Enabled)
                buttonFire.PerformClick();
        }

        private void CheckCanFire()
        {
            var ship = spaceGridControl.SelectedShip;
            bool okay = ship != null && ship != helm.Ship && (ship.Nation != helm.Ship.Nation || checkBoxFriendlyFire.Checked);
            buttonFire.Enabled = okay;
            labelBoard.Visible = okay;
            if (!okay)
                return;
            m_left = Math.Sin((ship.S - helm.Ship.S).Argument - helm.Ship.Heading) < 0;
            labelBoard.Text = m_left ? "Левый борт" : "Правый борт";
        }

        private void Fire()
        {
            var ship = spaceGridControl.SelectedShip;
            if (ship != null)
                client.Fire(ship, m_left);
        }
    }
}
