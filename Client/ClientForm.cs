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
            spaceGridControl.Visible = false;
//            scaleControl.Visible = false;
            toolStrip.Visible = false;
            timerUpdate.Enabled = true;
//            scaleControl.OnValueChanged += scaleControl_ValueChanged;
            spaceGridControl.Options = 
                SpaceGridControl.DrawingOptions.FriendlyMissileCircles | SpaceGridControl.DrawingOptions.FriendlyVulnerableSectors |
                SpaceGridControl.DrawingOptions.HostileVulnerableSectors |
                SpaceGridControl.DrawingOptions.MyMissileCircles | SpaceGridControl.DrawingOptions.MyVulnerableSectors;
            spaceGridControl.Selectable =
//                SpaceGridControl.SelectableObjects.Missiles | 
                SpaceGridControl.SelectableObjects.Stars |
                SpaceGridControl.SelectableObjects.Ships;
            Controls.Add(labelMessage);
        }

        private void SetPalette(PaletteDefinition palette)
        {
            spaceGridControl.SetPalette(palette);
            toolStrip.BackColor = palette.BackColor;
            toolStrip.ForeColor = palette.ForeColor;
            //scaleControl.BackColor = palette.BackColor;
            //scaleControl.ButtonBackColor = palette.SecondaryBackColor;
            //scaleControl.ForeColor = palette.ForeColor;
            //scaleControl.ButtonForeColor = palette.SecondaryForeColor;
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
            bool okay = credentials != 0 && client.Login(credentials);
            if (!okay)
            {
                Close();
                return;
            }
            UpdatePermanentData();
            spaceGridControl.Visible = true;
//            scaleControl.Visible = true;
            toolStrip.Visible = true;
            timerUpdate.Enabled = true;
            //SetPalette(PaletteDefinition.Black);
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            if (client == null)
            {
                timerUpdate.Enabled = false;
                Login();
            }
            if (client != null && client.Universe != null)
            {
                int generation = client.Universe.Generation;
                client.UpdateView();
                if (client.Universe.Generation != generation)
                    UpdatePermanentData();
                UpdateData();
            }
        }

        //private void scaleControl_ValueChanged(object sender, EventArgs e)
        //{
        //    spaceGridControl.WorldScale = scaleControl.Value;
        //}

        private void UpdatePermanentData()
        {
            spaceGridControl.Constants = client.Universe.Constants;
//            scaleControl.Value = client.Universe.Constants.DefaultScale;
            spaceGridControl.WorldScale = client.Universe.Constants.DefaultScale;
            spaceGridControl.Stars = client.Universe.Stars;
        }

        private void UpdateData()
        {
            //shipControl.Helm = helm = client.GetHelm();
            //Text = helm.Name;
            spaceGridControl.Ships = client.Universe.Ships;
            spaceGridControl.Missiles = client.Universe.Missiles;
            if (client.Universe.Ship != null)
            {
                spaceGridControl.OwnShip = client.Universe.Ship;
                spaceGridControl.Origin = client.Universe.Ship.Position;
            }
            //spaceGridControl.Curves.Add(Trajectory);
            //spaceGridControl.WorldScale = Catalog.Instance.DefaultScale;
            //scaleControl.Value = Catalog.Instance.DefaultScale;
            //if (helm == null)
            //    return;
            //client.Update();
            //shipControl.UpdateControls();
            //if (helm != null)
            //    GetData();
            //spaceGridControl.Invalidate();
        }

        private void miWhite_Click(object sender, EventArgs e)
        {
            SetPalette(PaletteDefinition.White);
        }

        private void miBlack_Click(object sender, EventArgs e)
        {
            SetPalette(PaletteDefinition.Black);
        }
    }
}
