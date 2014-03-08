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
        public static int PredefinedIdShip;
        public static ControlMode PredefinedControlMode;

        public ClientForm()
        {
            InitializeComponent();
            spaceGridControl.Visible = false;
            toolStrip.Visible = false;
            spaceGridControl.Options = 
                DrawingOptions.FriendlyMissileCircles |
                DrawingOptions.FriendlyVulnerableSectors |
                DrawingOptions.HostileVulnerableSectors |
                DrawingOptions.MyMissileCircles |
                DrawingOptions.MyVulnerableSectors;
            spaceGridControl.Selectable =
                SelectableObjects.Stars |
                SelectableObjects.Ships;
            spaceGridControl.HeadingToChanged += HeadingToChanged;
            spaceGridControl.RollToChanged += RollToChanged;
            spaceGridControl.ThrustToChanged += ThrustToChanged;
            Controls.Add(labelMessage);
        }

        private void SetPalette(PaletteDefinition palette)
        {
            spaceGridControl.SetPalette(palette);
        }

        private SF.ClientLibrary.SpaceClient client;

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
            var credentials = PredefinedIdShip;
            if (credentials == 0)
                credentials = LogonDialog.Execute(client.GetShipRegistry());
            bool okay = credentials != 0 && client.Login(credentials);
            if (!okay)
            {
                Close();
                return;
            }
            if (ControlMode.None != PredefinedControlMode)
            {
                btnMode.Visible = false;
                spaceGridControl.Mode = PredefinedControlMode;
            }
            spaceGridControl.Universe = client.Universe;
            toolStrip.Visible = true;
            spaceGridControl.Visible = true;
            timerUpdate.Enabled = true;
        }

        private void UpdateData()
        {
            if (client == null || client.Universe == null)
                return;
            client.UpdateView();
            if (client.Universe.Ship != null)
                spaceGridControl.Origin = client.Universe.Ship.Position;
            spaceGridControl.Invalidate();
        }

        private void HeadingToChanged(object sender, ValueEventArgs<double> e)
        {
            if (client == null || client.Universe == null)
                return;
            client.SetHeadingTo(e.Argument);
        }

        private void RollToChanged(object sender, ValueEventArgs<double> e)
        {
            if (client == null || client.Universe == null)
                return;
            client.SetRollTo(e.Argument);
        }

        private void ThrustToChanged(object sender, ValueEventArgs<double> e)
        {
            if (client == null || client.Universe == null)
                return;
            client.SetThrustTo(e.Argument);
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            Login();
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void pilotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            spaceGridControl.Mode = ControlMode.Pilot;
        }

        private void fireControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            spaceGridControl.Mode = ControlMode.Gunner;
        }

        private void whiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetPalette(PaletteDefinition.White);
            toolStrip.BackColor = BackColor = Color.White;
            ForeColor = Color.Black;
        }

        private void blackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetPalette(PaletteDefinition.Black);
            toolStrip.BackColor = BackColor = Color.DarkSlateGray;
            ForeColor = Color.White;
        }
    }
}
