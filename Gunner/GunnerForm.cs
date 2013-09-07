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
            Controls.Add(labelMessage);
        }

        private IHelm helm;
        private SF.ClientLibrary.SpaceClient client;
        private bool left;
        private IShip target;

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
            helm = client.GetHelm();
            Text = helm.Name;
            spaceGridControl.OwnShip = helm;
            missileControl.MissileClass = helm.Missile;
            missileControl.ShipClass = helm.Class;
            spaceGridControl.WorldScale = Catalog.Instance.DefaultScale;
            scaleControl.Value = Catalog.Instance.DefaultScale; ;
            if (helm.Class.Superclass != ShipSuperclass.CLAC)
                tabControlWeapons.TabPages.Remove(tabPageLACs);
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
            GetData();
            spaceGridControl.Invalidate();
        }

        private void GetData()
        {
            if (helm.IsDead())
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
            spaceGridControl.Rotation = helm.Speed.Length <= MathUtils.Epsilon ? helm.Heading : helm.Speed.Argument;
            var ship = spaceGridControl.Selected ?? helm;
            indicatorControl.Acceleration = ship.Acceleration;
            indicatorControl.Speed = ship.Speed;
            indicatorControl.Position = ship.Position;
            var carried = client.GetCarriedShips();
            if (carried != null)
            {
                dataGridViewLACs.RowCount = carried.Count;
                for (int i = 0; i < carried.Count; i++)
                    dataGridViewLACs.Rows[i].Cells[columnName.Index].Value = carried[i];
            }
            if (target == null)
            {
                labelBoard.Text = "Цель не выбрана";
                missileControl.Board = default(Board);
            }
            else
            {
                left = helm.IsLeftBoard(target);
                labelBoard.Text = left ? "Левый борт" : "Правый борт";
                var launchers = new double[helm.Missiles];
                for (int i = 0; i < helm.Missiles; i++)
                    launchers[i] = i*1.0/helm.Missiles;
                missileControl.Board = left ? helm.Left : helm.Right;
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
                client.Fire((IShip)ship, missileControl.Selected);
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

        private void dataGridViewLACs_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != columnLaunch.Index)
                return;
            var name = (string) dataGridViewLACs.Rows[e.RowIndex].Cells[columnName.Index].Value;
            client.Launch(name);
        }
    }
}
