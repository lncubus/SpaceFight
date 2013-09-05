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
                SpaceGridControl.DrawingOptions.FriendlyVulnerableSectors |
                SpaceGridControl.DrawingOptions.HostileVulnerableSectors |
                SpaceGridControl.DrawingOptions.MyMissileCircles |
                SpaceGridControl.DrawingOptions.FriendlySectorsByMyMissileRange;
            spaceGridControl.Selectable =
                SpaceGridControl.SelectableObjects.Stars |
                SpaceGridControl.SelectableObjects.Ships;
        }

        private IHelm helm;
        private SF.ClientLibrary.SpaceClient client;
        private bool left;
        private IShip target;

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
                Text = helm.Name;
                spaceGridControl.OwnShip = helm;
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
            if (helm != null)
                GetData();
            spaceGridControl.Invalidate();
        }

        private void GetData()
        {
            if (helm.IsDead())
                Die();
            spaceGridControl.Ships = client.GetVisibleShips();
            spaceGridControl.Stars = client.GetStars();
            spaceGridControl.Missiles = client.GetVisibleMissiles();
            spaceGridControl.Origin = helm.Position;
            spaceGridControl.Rotation = helm.Heading;
            var ship = spaceGridControl.Selected ?? helm;
            indicatorControl.Acceleration = ship.Acceleration;
            indicatorControl.Speed = ship.Speed;
            indicatorControl.Position = ship.Position;
            if (target == null)
                labelBoard.Text = "Цель не выбрана";
            else
            {
                left = helm.IsLeft(target);
                if (Math.Cos(helm.Roll) < 0)
                    left = !left;
                labelBoard.Text = left ? "Левый борт" : "Правый борт";
            }
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

        private void spaceGridControl_ParticleSelected(object sender, EventArgs e)
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
            var ship = spaceGridControl.Selected;
            bool okay = ship != null && ship != helm && (ship is IShip) && (ship.Nation != helm.Nation || checkBoxFriendlyFire.Checked);
            buttonFire.Enabled = okay;
            target = !okay ? null : ship as IShip;
        }

        private void Fire()
        {
            var ship = spaceGridControl.Selected;
            if (ship is IShip)
                client.Fire((IShip)ship);
        }

        private void GunnerForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            switch (e.KeyChar)
            {
                case '+': case '=':
                    scaleControl.ZoomIn();
                    break;
                case '-': case '_':
                    scaleControl.ZoomOut();
                    break;
                default :
                    e.Handled = false;
                    break;
            }
        }
    }
}
