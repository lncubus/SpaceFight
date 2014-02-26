using SF.Controls;

namespace Client
{
    partial class ClientForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Drawing.StringFormat stringFormat1 = new System.Drawing.StringFormat();
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnPalette = new System.Windows.Forms.ToolStripDropDownButton();
            this.miWhite = new System.Windows.Forms.ToolStripMenuItem();
            this.miBlack = new System.Windows.Forms.ToolStripMenuItem();
            this.btnZoomIn = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.spaceGridControl = new SF.Controls.NavigationControl();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerUpdate
            // 
            this.timerUpdate.Interval = 500;
            this.timerUpdate.Tick += new System.EventHandler(this.timerUpdate_Tick);
            // 
            // toolStrip
            // 
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPalette,
            this.btnZoomIn,
            this.toolStripButton2});
            this.toolStrip.Location = new System.Drawing.Point(770, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(24, 588);
            this.toolStrip.TabIndex = 2;
            this.toolStrip.Text = "toolStrip";
            this.toolStrip.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical90;
            // 
            // btnPalette
            // 
            this.btnPalette.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miWhite,
            this.miBlack});
            this.btnPalette.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPalette.Name = "btnPalette";
            this.btnPalette.Size = new System.Drawing.Size(21, 44);
            this.btnPalette.Text = "Цвет";
            // 
            // miWhite
            // 
            this.miWhite.Name = "miWhite";
            this.miWhite.Size = new System.Drawing.Size(152, 22);
            this.miWhite.Text = "Белый";
            this.miWhite.Click += new System.EventHandler(this.miWhite_Click);
            // 
            // miBlack
            // 
            this.miBlack.Name = "miBlack";
            this.miBlack.Size = new System.Drawing.Size(152, 22);
            this.miBlack.Text = "Черный";
            this.miBlack.Click += new System.EventHandler(this.miBlack_Click);
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(21, 19);
            this.btnZoomIn.Text = "+";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(21, 15);
            this.toolStripButton2.Text = "-";
            // 
            // spaceGridControl
            // 
            stringFormat1.Alignment = System.Drawing.StringAlignment.Center;
            stringFormat1.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
            stringFormat1.LineAlignment = System.Drawing.StringAlignment.Center;
            stringFormat1.Trimming = System.Drawing.StringTrimming.None;
            this.spaceGridControl.CenteredLayout = stringFormat1;
            this.spaceGridControl.Constants = null;
            this.spaceGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spaceGridControl.Location = new System.Drawing.Point(0, 0);
            this.spaceGridControl.Missiles = null;
            this.spaceGridControl.Name = "spaceGridControl";
            this.spaceGridControl.Options = SF.Controls.SpaceGridControl.DrawingOptions.None;
            this.spaceGridControl.Polar = false;
            this.spaceGridControl.ReadOnly = false;
            this.spaceGridControl.Rotation = 0D;
            this.spaceGridControl.Selectable = SF.Controls.SpaceGridControl.SelectableObjects.None;
            this.spaceGridControl.Selected = null;
            this.spaceGridControl.Ships = null;
            this.spaceGridControl.Size = new System.Drawing.Size(770, 588);
            this.spaceGridControl.Stars = null;
            this.spaceGridControl.StaticGrid = true;
            this.spaceGridControl.TabIndex = 1;
            this.spaceGridControl.WorldScale = 2000000D;
            // 
            // ClientForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(794, 588);
            this.Controls.Add(this.spaceGridControl);
            this.Controls.Add(this.toolStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.KeyPreview = true;
            this.Name = "ClientForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timerUpdate;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripDropDownButton btnPalette;
        private System.Windows.Forms.ToolStripMenuItem miWhite;
        private System.Windows.Forms.ToolStripMenuItem miBlack;
        private System.Windows.Forms.ToolStripButton btnZoomIn;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private NavigationControl spaceGridControl;
    }
}

