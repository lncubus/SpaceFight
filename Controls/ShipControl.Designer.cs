namespace SF.Controls
{
    partial class ShipControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Drawing.StringFormat stringFormat1 = new System.Drawing.StringFormat();
            System.Drawing.StringFormat stringFormat2 = new System.Drawing.StringFormat();
            this.mainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelAcceleration = new System.Windows.Forms.TableLayoutPanel();
            this.accelerationProgressBar = new SF.Controls.VerticalProgressBar();
            this.accelerationTrackBar = new System.Windows.Forms.TrackBar();
            this.labelAcceleration = new System.Windows.Forms.Label();
            this.flowLayoutPanelRoll = new System.Windows.Forms.FlowLayoutPanel();
            this.labelRoll = new System.Windows.Forms.Label();
            this.rollControl = new SF.Controls.RollControl();
            this.flowLayoutPanelHeading = new System.Windows.Forms.FlowLayoutPanel();
            this.labelHeading = new System.Windows.Forms.Label();
            this.headingControl = new SF.Controls.CompassControl();
            this.mainTableLayoutPanel.SuspendLayout();
            this.tableLayoutPanelAcceleration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.accelerationTrackBar)).BeginInit();
            this.flowLayoutPanelRoll.SuspendLayout();
            this.flowLayoutPanelHeading.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTableLayoutPanel
            // 
            this.mainTableLayoutPanel.AutoSize = true;
            this.mainTableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mainTableLayoutPanel.ColumnCount = 2;
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.mainTableLayoutPanel.Controls.Add(this.tableLayoutPanelAcceleration, 1, 1);
            this.mainTableLayoutPanel.Controls.Add(this.flowLayoutPanelRoll, 0, 1);
            this.mainTableLayoutPanel.Controls.Add(this.flowLayoutPanelHeading, 0, 0);
            this.mainTableLayoutPanel.Location = new System.Drawing.Point(8, 0);
            this.mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            this.mainTableLayoutPanel.RowCount = 2;
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayoutPanel.Size = new System.Drawing.Size(238, 412);
            this.mainTableLayoutPanel.TabIndex = 25;
            // 
            // tableLayoutPanelAcceleration
            // 
            this.tableLayoutPanelAcceleration.AutoSize = true;
            this.tableLayoutPanelAcceleration.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanelAcceleration.ColumnCount = 2;
            this.tableLayoutPanelAcceleration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelAcceleration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelAcceleration.Controls.Add(this.accelerationProgressBar, 1, 1);
            this.tableLayoutPanelAcceleration.Controls.Add(this.accelerationTrackBar, 0, 1);
            this.tableLayoutPanelAcceleration.Controls.Add(this.labelAcceleration, 0, 0);
            this.tableLayoutPanelAcceleration.Location = new System.Drawing.Point(139, 259);
            this.tableLayoutPanelAcceleration.Name = "tableLayoutPanelAcceleration";
            this.tableLayoutPanelAcceleration.RowCount = 2;
            this.tableLayoutPanelAcceleration.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelAcceleration.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelAcceleration.Size = new System.Drawing.Size(96, 150);
            this.tableLayoutPanelAcceleration.TabIndex = 25;
            // 
            // accelerationProgressBar
            // 
            this.accelerationProgressBar.ForeColor = System.Drawing.Color.DarkGreen;
            this.accelerationProgressBar.Location = new System.Drawing.Point(51, 33);
            this.accelerationProgressBar.Margin = new System.Windows.Forms.Padding(3, 13, 3, 13);
            this.accelerationProgressBar.Maximum = 1000;
            this.accelerationProgressBar.Name = "accelerationProgressBar";
            this.accelerationProgressBar.Size = new System.Drawing.Size(32, 104);
            this.accelerationProgressBar.Step = 100;
            this.accelerationProgressBar.TabIndex = 2;
            // 
            // accelerationTrackBar
            // 
            this.accelerationTrackBar.LargeChange = 100;
            this.accelerationTrackBar.Location = new System.Drawing.Point(3, 23);
            this.accelerationTrackBar.Maximum = 1000;
            this.accelerationTrackBar.Name = "accelerationTrackBar";
            this.accelerationTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.accelerationTrackBar.Size = new System.Drawing.Size(42, 124);
            this.accelerationTrackBar.TabIndex = 1;
            this.accelerationTrackBar.TickFrequency = 100;
            this.accelerationTrackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.accelerationTrackBar.ValueChanged += new System.EventHandler(this.accelerationTrackBar_ValueChanged);
            // 
            // labelAcceleration
            // 
            this.tableLayoutPanelAcceleration.SetColumnSpan(this.labelAcceleration, 2);
            this.labelAcceleration.Location = new System.Drawing.Point(3, 0);
            this.labelAcceleration.Name = "labelAcceleration";
            this.labelAcceleration.Size = new System.Drawing.Size(84, 20);
            this.labelAcceleration.TabIndex = 0;
            this.labelAcceleration.Text = "Ускорение";
            this.labelAcceleration.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanelRoll
            // 
            this.flowLayoutPanelRoll.AutoSize = true;
            this.flowLayoutPanelRoll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanelRoll.Controls.Add(this.labelRoll);
            this.flowLayoutPanelRoll.Controls.Add(this.rollControl);
            this.flowLayoutPanelRoll.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelRoll.Location = new System.Drawing.Point(3, 259);
            this.flowLayoutPanelRoll.Name = "flowLayoutPanelRoll";
            this.flowLayoutPanelRoll.Size = new System.Drawing.Size(130, 150);
            this.flowLayoutPanelRoll.TabIndex = 24;
            // 
            // labelRoll
            // 
            this.labelRoll.Location = new System.Drawing.Point(3, 0);
            this.labelRoll.Name = "labelRoll";
            this.labelRoll.Size = new System.Drawing.Size(123, 20);
            this.labelRoll.TabIndex = 0;
            this.labelRoll.Text = "Перекат";
            this.labelRoll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rollControl
            // 
            stringFormat1.Alignment = System.Drawing.StringAlignment.Center;
            stringFormat1.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
            stringFormat1.LineAlignment = System.Drawing.StringAlignment.Center;
            stringFormat1.Trimming = System.Drawing.StringTrimming.None;
            this.rollControl.CenteredLayout = stringFormat1;
            this.rollControl.Location = new System.Drawing.Point(3, 23);
            this.rollControl.Name = "rollControl";
            this.rollControl.ReadOnly = false;
            this.rollControl.Roll = 0;
            this.rollControl.RollTo = 0;
            this.rollControl.Size = new System.Drawing.Size(124, 124);
            this.rollControl.TabIndex = 1;
            this.rollControl.OnRollToChanged += new System.EventHandler(this.rollControl_OnRollToChanged);
            // 
            // flowLayoutPanelHeading
            // 
            this.flowLayoutPanelHeading.AutoSize = true;
            this.flowLayoutPanelHeading.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mainTableLayoutPanel.SetColumnSpan(this.flowLayoutPanelHeading, 2);
            this.flowLayoutPanelHeading.Controls.Add(this.labelHeading);
            this.flowLayoutPanelHeading.Controls.Add(this.headingControl);
            this.flowLayoutPanelHeading.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelHeading.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanelHeading.Name = "flowLayoutPanelHeading";
            this.flowLayoutPanelHeading.Size = new System.Drawing.Size(230, 250);
            this.flowLayoutPanelHeading.TabIndex = 23;
            // 
            // labelHeading
            // 
            this.labelHeading.Location = new System.Drawing.Point(3, 0);
            this.labelHeading.Name = "labelHeading";
            this.labelHeading.Size = new System.Drawing.Size(224, 20);
            this.labelHeading.TabIndex = 0;
            this.labelHeading.Text = "Курс";
            this.labelHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // headingControl
            // 
            this.headingControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            stringFormat2.Alignment = System.Drawing.StringAlignment.Center;
            stringFormat2.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
            stringFormat2.LineAlignment = System.Drawing.StringAlignment.Center;
            stringFormat2.Trimming = System.Drawing.StringTrimming.None;
            this.headingControl.CenteredLayout = stringFormat2;
            this.headingControl.Heading = 0;
            this.headingControl.HeadingTo = 0;
            this.headingControl.Location = new System.Drawing.Point(3, 23);
            this.headingControl.Name = "headingControl";
            this.headingControl.ReadOnly = false;
            this.headingControl.Size = new System.Drawing.Size(224, 224);
            this.headingControl.TabIndex = 1;
            this.headingControl.OnHeadingToChanged += new System.EventHandler(this.headingControl_OnHeadingToChanged);
            // 
            // ShipControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.mainTableLayoutPanel);
            this.Name = "ShipControl";
            this.Size = new System.Drawing.Size(249, 415);
            this.mainTableLayoutPanel.ResumeLayout(false);
            this.mainTableLayoutPanel.PerformLayout();
            this.tableLayoutPanelAcceleration.ResumeLayout(false);
            this.tableLayoutPanelAcceleration.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.accelerationTrackBar)).EndInit();
            this.flowLayoutPanelRoll.ResumeLayout(false);
            this.flowLayoutPanelHeading.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainTableLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelAcceleration;
        public VerticalProgressBar accelerationProgressBar;
        public System.Windows.Forms.TrackBar accelerationTrackBar;
        private System.Windows.Forms.Label labelAcceleration;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelRoll;
        private System.Windows.Forms.Label labelRoll;
        public RollControl rollControl;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelHeading;
        private System.Windows.Forms.Label labelHeading;
        public CompassControl headingControl;


    }
}
