namespace Pilot
{
    partial class PilotForm
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.spaceGridControl = new SF.Controls.SpaceGridControl();
            this.shipControl = new SF.Controls.ShipControl();
            this.scaleControl = new SF.Controls.ScaleControl();
            this.indicatorControl = new SF.Controls.IndicatorControl();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerUpdate
            // 
            this.timerUpdate.Interval = 500;
            this.timerUpdate.Tick += new System.EventHandler(this.timerUpdate_Tick);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.Controls.Add(this.spaceGridControl, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.shipControl, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.scaleControl, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.indicatorControl, 1, 2);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(794, 632);
            this.tableLayoutPanel.TabIndex = 2;
            // 
            // spaceGridControl
            // 
            stringFormat1.Alignment = System.Drawing.StringAlignment.Center;
            stringFormat1.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
            stringFormat1.LineAlignment = System.Drawing.StringAlignment.Center;
            stringFormat1.Trimming = System.Drawing.StringTrimming.None;
            this.spaceGridControl.CenteredLayout = stringFormat1;
            this.spaceGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spaceGridControl.Location = new System.Drawing.Point(3, 3);
            this.spaceGridControl.Name = "spaceGridControl";
            this.spaceGridControl.Options = SF.Controls.SpaceGridOptions.None;
            this.spaceGridControl.Polar = false;
            this.spaceGridControl.ReadOnly = false;
            this.spaceGridControl.Rotation = 0D;
            this.tableLayoutPanel.SetRowSpan(this.spaceGridControl, 3);
            this.spaceGridControl.SelectedShip = null;
            this.spaceGridControl.Size = new System.Drawing.Size(533, 626);
            this.spaceGridControl.StaticGrid = true;
            this.spaceGridControl.TabIndex = 0;
            this.spaceGridControl.WorldScale = 2000000D;
            // 
            // shipControl
            // 
            this.shipControl.AutoSize = true;
            this.shipControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.shipControl.Helm = null;
            this.shipControl.Location = new System.Drawing.Point(542, 38);
            this.shipControl.Name = "shipControl";
            this.shipControl.ReadOnly = false;
            this.shipControl.Size = new System.Drawing.Size(249, 415);
            this.shipControl.TabIndex = 2;
            // 
            // scaleControl
            // 
            this.scaleControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.scaleControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scaleControl.Location = new System.Drawing.Point(542, 3);
            this.scaleControl.MaxValue = 5000000000D;
            this.scaleControl.MinValue = 500D;
            this.scaleControl.Name = "scaleControl";
            this.scaleControl.Size = new System.Drawing.Size(249, 29);
            this.scaleControl.TabIndex = 3;
            this.scaleControl.Unit = "км";
            this.scaleControl.Value = 2000000D;
            // 
            // indicatorControl
            // 
            this.indicatorControl.Location = new System.Drawing.Point(542, 459);
            this.indicatorControl.Name = "indicatorControl";
            this.indicatorControl.Size = new System.Drawing.Size(249, 170);
            this.indicatorControl.TabIndex = 4;
            // 
            // PilotForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(794, 632);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.KeyPreview = true;
            this.Name = "PilotForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PilotForm_KeyPress);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerUpdate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private SF.Controls.SpaceGridControl spaceGridControl;
        private SF.Controls.ShipControl shipControl;
        private SF.Controls.ScaleControl scaleControl;
        private SF.Controls.IndicatorControl indicatorControl;
    }
}

