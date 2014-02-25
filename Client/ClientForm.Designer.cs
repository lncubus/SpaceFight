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
            System.Drawing.StringFormat stringFormat2 = new System.Drawing.StringFormat();
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            this.spaceGridControl = new SF.Controls.NavigationControl();
            this.scaleControl = new SF.Controls.ScaleControl();
            this.SuspendLayout();
            // 
            // timerUpdate
            // 
            this.timerUpdate.Interval = 500;
            this.timerUpdate.Tick += new System.EventHandler(this.timerUpdate_Tick);
            // 
            // spaceGridControl
            // 
            stringFormat2.Alignment = System.Drawing.StringAlignment.Center;
            stringFormat2.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
            stringFormat2.LineAlignment = System.Drawing.StringAlignment.Center;
            stringFormat2.Trimming = System.Drawing.StringTrimming.None;
            this.spaceGridControl.CenteredLayout = stringFormat2;
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
            this.spaceGridControl.Size = new System.Drawing.Size(794, 588);
            this.spaceGridControl.Stars = null;
            this.spaceGridControl.StaticGrid = true;
            this.spaceGridControl.TabIndex = 1;
            this.spaceGridControl.WorldScale = 2000000D;
            // 
            // scaleControl
            // 
            this.scaleControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.scaleControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.scaleControl.BackColor = System.Drawing.Color.Transparent;
            this.scaleControl.Location = new System.Drawing.Point(591, 12);
            this.scaleControl.MaxValue = 5000000000D;
            this.scaleControl.MinValue = 1000D;
            this.scaleControl.Name = "scaleControl";
            this.scaleControl.Size = new System.Drawing.Size(191, 29);
            this.scaleControl.TabIndex = 2;
            this.scaleControl.Unit = "км";
            this.scaleControl.Value = 2000000D;
            // 
            // ClientForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(794, 588);
            this.Controls.Add(this.scaleControl);
            this.Controls.Add(this.spaceGridControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.KeyPreview = true;
            this.Name = "ClientForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerUpdate;
        private SF.Controls.NavigationControl spaceGridControl;
        private SF.Controls.ScaleControl scaleControl;
    }
}

