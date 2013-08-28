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
            components = new System.ComponentModel.Container();
            System.Drawing.StringFormat stringFormat1 = new System.Drawing.StringFormat();
            timerUpdate = new System.Windows.Forms.Timer(components);
            tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            spaceGridControl = new SF.Controls.SpaceGridControl();
            shipControl = new SF.Controls.ShipControl();
            scaleControl = new SF.Controls.ScaleControl();
            indicatorControl = new SF.Controls.IndicatorControl();
            tableLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // timerUpdate
            // 
            timerUpdate.Interval = 500;
            timerUpdate.Tick += new System.EventHandler(timerUpdate_Tick);
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel.Controls.Add(spaceGridControl, 0, 0);
            tableLayoutPanel.Controls.Add(shipControl, 1, 1);
            tableLayoutPanel.Controls.Add(scaleControl, 1, 0);
            tableLayoutPanel.Controls.Add(indicatorControl, 1, 2);
            tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 3;
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel.Size = new System.Drawing.Size(794, 632);
            tableLayoutPanel.TabIndex = 2;
            // 
            // spaceGridControl
            // 
            stringFormat1.Alignment = System.Drawing.StringAlignment.Center;
            stringFormat1.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
            stringFormat1.LineAlignment = System.Drawing.StringAlignment.Center;
            stringFormat1.Trimming = System.Drawing.StringTrimming.None;
            spaceGridControl.CenteredLayout = stringFormat1;
            spaceGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            spaceGridControl.Location = new System.Drawing.Point(3, 3);
            spaceGridControl.Name = "spaceGridControl";
            spaceGridControl.Polar = false;
            spaceGridControl.ReadOnly = false;
            spaceGridControl.Rotation = 0D;
            tableLayoutPanel.SetRowSpan(spaceGridControl, 3);
            spaceGridControl.Size = new System.Drawing.Size(533, 626);
            spaceGridControl.StaticGrid = true;
            spaceGridControl.TabIndex = 0;
            spaceGridControl.WorldScale = 2000000D;
            // 
            // shipControl
            // 
            shipControl.AutoSize = true;
            shipControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            shipControl.Helm = null;
            shipControl.Location = new System.Drawing.Point(542, 38);
            shipControl.Name = "shipControl";
            shipControl.ReadOnly = false;
            shipControl.Size = new System.Drawing.Size(249, 415);
            shipControl.TabIndex = 2;
            // 
            // scaleControl
            // 
            scaleControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            scaleControl.Dock = System.Windows.Forms.DockStyle.Fill;
            scaleControl.Location = new System.Drawing.Point(542, 3);
            scaleControl.MaxValue = 5000000000D;
            scaleControl.MinValue = 500D;
            scaleControl.Name = "scaleControl";
            scaleControl.Size = new System.Drawing.Size(249, 29);
            scaleControl.TabIndex = 3;
            scaleControl.Unit = "км";
            scaleControl.Value = 2000000D;
            // 
            // indicatorControl
            // 
            indicatorControl.Location = new System.Drawing.Point(542, 459);
            indicatorControl.Name = "indicatorControl";
            indicatorControl.Size = new System.Drawing.Size(249, 170);
            indicatorControl.TabIndex = 4;
            // 
            // PilotForm
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            BackColor = System.Drawing.Color.AliceBlue;
            ClientSize = new System.Drawing.Size(794, 632);
            Controls.Add(tableLayoutPanel);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            KeyPreview = true;
            Name = "PilotForm";
            WindowState = System.Windows.Forms.FormWindowState.Maximized;
            KeyPress += new System.Windows.Forms.KeyPressEventHandler(PilotForm_KeyPress);
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            ResumeLayout(false);

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

