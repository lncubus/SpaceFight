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
            this.panelInterface = new System.Windows.Forms.Panel();
            this.labelScale = new System.Windows.Forms.Label();
            this.buttonZoomOut = new System.Windows.Forms.Button();
            this.buttonZoomIn = new System.Windows.Forms.Button();
            this.tableLayoutPanel.SuspendLayout();
            this.panelInterface.SuspendLayout();
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
            this.tableLayoutPanel.Controls.Add(this.panelInterface, 1, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.Size = new System.Drawing.Size(794, 578);
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
            this.spaceGridControl.Size = new System.Drawing.Size(533, 572);
            this.spaceGridControl.TabIndex = 0;
            this.spaceGridControl.WorldScale = 1000000D;
            // 
            // shipControl
            // 
            this.shipControl.AutoSize = true;
            this.shipControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.shipControl.Helm = null;
            this.shipControl.Location = new System.Drawing.Point(542, 40);
            this.shipControl.Name = "shipControl";
            this.shipControl.ReadOnly = false;
            this.shipControl.Size = new System.Drawing.Size(249, 415);
            this.shipControl.TabIndex = 2;
            // 
            // panelInterface
            // 
            this.panelInterface.Controls.Add(this.labelScale);
            this.panelInterface.Controls.Add(this.buttonZoomOut);
            this.panelInterface.Controls.Add(this.buttonZoomIn);
            this.panelInterface.Location = new System.Drawing.Point(542, 3);
            this.panelInterface.Name = "panelInterface";
            this.panelInterface.Size = new System.Drawing.Size(249, 31);
            this.panelInterface.TabIndex = 1;
            // 
            // labelScale
            // 
            this.labelScale.AutoSize = true;
            this.labelScale.Location = new System.Drawing.Point(62, 9);
            this.labelScale.Name = "labelScale";
            this.labelScale.Size = new System.Drawing.Size(30, 13);
            this.labelScale.TabIndex = 2;
            this.labelScale.Text = "0 км";
            // 
            // buttonZoomOut
            // 
            this.buttonZoomOut.BackColor = System.Drawing.Color.White;
            this.buttonZoomOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonZoomOut.Location = new System.Drawing.Point(33, 4);
            this.buttonZoomOut.Name = "buttonZoomOut";
            this.buttonZoomOut.Size = new System.Drawing.Size(23, 23);
            this.buttonZoomOut.TabIndex = 1;
            this.buttonZoomOut.TabStop = false;
            this.buttonZoomOut.Text = "-";
            this.buttonZoomOut.UseVisualStyleBackColor = false;
            this.buttonZoomOut.Click += new System.EventHandler(this.buttonZoomOut_Click);
            // 
            // buttonZoomIn
            // 
            this.buttonZoomIn.BackColor = System.Drawing.Color.White;
            this.buttonZoomIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonZoomIn.Location = new System.Drawing.Point(4, 4);
            this.buttonZoomIn.Name = "buttonZoomIn";
            this.buttonZoomIn.Size = new System.Drawing.Size(23, 23);
            this.buttonZoomIn.TabIndex = 0;
            this.buttonZoomIn.TabStop = false;
            this.buttonZoomIn.Text = "+";
            this.buttonZoomIn.UseVisualStyleBackColor = false;
            this.buttonZoomIn.Click += new System.EventHandler(this.buttonZoomIn_Click);
            // 
            // PilotForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(794, 578);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PilotForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.panelInterface.ResumeLayout(false);
            this.panelInterface.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerUpdate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private SF.Controls.SpaceGridControl spaceGridControl;
        private SF.Controls.ShipControl shipControl;
        private System.Windows.Forms.Panel panelInterface;
        private System.Windows.Forms.Button buttonZoomOut;
        private System.Windows.Forms.Button buttonZoomIn;
        private System.Windows.Forms.Label labelScale;
    }
}

