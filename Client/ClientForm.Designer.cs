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
            this.btnMode = new System.Windows.Forms.ToolStripDropDownButton();
            this.pilotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fireControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnPalette = new System.Windows.Forms.ToolStripDropDownButton();
            this.whiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spaceGridControl = new SF.Controls.CompositeControl();
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
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnMode,
            this.btnPalette});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(18, 588);
            this.toolStrip.TabIndex = 2;
            this.toolStrip.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical270;
            // 
            // btnMode
            // 
            this.btnMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pilotToolStripMenuItem,
            this.fireControlToolStripMenuItem});
            this.btnMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMode.Name = "btnMode";
            this.btnMode.Size = new System.Drawing.Size(15, 50);
            this.btnMode.Text = "Режим";
            // 
            // pilotToolStripMenuItem
            // 
            this.pilotToolStripMenuItem.Name = "pilotToolStripMenuItem";
            this.pilotToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.pilotToolStripMenuItem.Text = "Пилотирование";
            this.pilotToolStripMenuItem.Click += new System.EventHandler(this.pilotToolStripMenuItem_Click);
            // 
            // fireControlToolStripMenuItem
            // 
            this.fireControlToolStripMenuItem.Name = "fireControlToolStripMenuItem";
            this.fireControlToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.fireControlToolStripMenuItem.Text = "Управление огнем";
            this.fireControlToolStripMenuItem.Click += new System.EventHandler(this.fireControlToolStripMenuItem_Click);
            // 
            // btnPalette
            // 
            this.btnPalette.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.whiteToolStripMenuItem,
            this.blackToolStripMenuItem});
            this.btnPalette.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPalette.Name = "btnPalette";
            this.btnPalette.Size = new System.Drawing.Size(15, 61);
            this.btnPalette.Text = "Палитра";
            // 
            // whiteToolStripMenuItem
            // 
            this.whiteToolStripMenuItem.Name = "whiteToolStripMenuItem";
            this.whiteToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.whiteToolStripMenuItem.Text = "Белая";
            this.whiteToolStripMenuItem.Click += new System.EventHandler(this.whiteToolStripMenuItem_Click);
            // 
            // blackToolStripMenuItem
            // 
            this.blackToolStripMenuItem.Name = "blackToolStripMenuItem";
            this.blackToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.blackToolStripMenuItem.Text = "Черная";
            this.blackToolStripMenuItem.Click += new System.EventHandler(this.blackToolStripMenuItem_Click);
            // 
            // spaceGridControl
            // 
            stringFormat1.Alignment = System.Drawing.StringAlignment.Center;
            stringFormat1.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
            stringFormat1.LineAlignment = System.Drawing.StringAlignment.Center;
            stringFormat1.Trimming = System.Drawing.StringTrimming.None;
            this.spaceGridControl.CenteredLayout = stringFormat1;
            this.spaceGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spaceGridControl.Location = new System.Drawing.Point(18, 0);
            this.spaceGridControl.Mode = SF.Space.ControlMode.None;
            this.spaceGridControl.Name = "spaceGridControl";
            this.spaceGridControl.Options = SF.Space.DrawingOptions.None;
            this.spaceGridControl.Polar = false;
            this.spaceGridControl.ReadOnly = false;
            this.spaceGridControl.Rotation = 0D;
            this.spaceGridControl.Selectable = SF.Space.SelectableObjects.None;
            this.spaceGridControl.Selected = null;
            this.spaceGridControl.Size = new System.Drawing.Size(776, 588);
            this.spaceGridControl.StaticGrid = true;
            this.spaceGridControl.TabIndex = 1;
            this.spaceGridControl.Unit = "км";
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
            this.Load += new System.EventHandler(this.ClientForm_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timerUpdate;
        private CompositeControl spaceGridControl;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripDropDownButton btnMode;
        private System.Windows.Forms.ToolStripDropDownButton btnPalette;
        private System.Windows.Forms.ToolStripMenuItem pilotToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fireControlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem whiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blackToolStripMenuItem;
    }
}

