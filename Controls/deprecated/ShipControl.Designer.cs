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
            mainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            tableLayoutPanelAcceleration = new System.Windows.Forms.TableLayoutPanel();
            accelerationProgressBar = new SF.Controls.VerticalProgressBar();
            accelerationTrackBar = new System.Windows.Forms.TrackBar();
            labelAcceleration = new System.Windows.Forms.Label();
            flowLayoutPanelRoll = new System.Windows.Forms.FlowLayoutPanel();
            labelRoll = new System.Windows.Forms.Label();
            rollControl = new SF.Controls.RollControl();
            flowLayoutPanelHeading = new System.Windows.Forms.FlowLayoutPanel();
            labelHeading = new System.Windows.Forms.Label();
            headingControl = new SF.Controls.CompassControl();
            mainTableLayoutPanel.SuspendLayout();
            tableLayoutPanelAcceleration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(accelerationTrackBar)).BeginInit();
            flowLayoutPanelRoll.SuspendLayout();
            flowLayoutPanelHeading.SuspendLayout();
            SuspendLayout();
            // 
            // mainTableLayoutPanel
            // 
            mainTableLayoutPanel.AutoSize = true;
            mainTableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            mainTableLayoutPanel.ColumnCount = 2;
            mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            mainTableLayoutPanel.Controls.Add(tableLayoutPanelAcceleration, 1, 1);
            mainTableLayoutPanel.Controls.Add(flowLayoutPanelRoll, 0, 1);
            mainTableLayoutPanel.Controls.Add(flowLayoutPanelHeading, 0, 0);
            mainTableLayoutPanel.Location = new System.Drawing.Point(8, 0);
            mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            mainTableLayoutPanel.RowCount = 2;
            mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            mainTableLayoutPanel.Size = new System.Drawing.Size(238, 412);
            mainTableLayoutPanel.TabIndex = 25;
            // 
            // tableLayoutPanelAcceleration
            // 
            tableLayoutPanelAcceleration.AutoSize = true;
            tableLayoutPanelAcceleration.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            tableLayoutPanelAcceleration.ColumnCount = 2;
            tableLayoutPanelAcceleration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanelAcceleration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanelAcceleration.Controls.Add(accelerationProgressBar, 1, 1);
            tableLayoutPanelAcceleration.Controls.Add(accelerationTrackBar, 0, 1);
            tableLayoutPanelAcceleration.Controls.Add(labelAcceleration, 0, 0);
            tableLayoutPanelAcceleration.Location = new System.Drawing.Point(139, 259);
            tableLayoutPanelAcceleration.Name = "tableLayoutPanelAcceleration";
            tableLayoutPanelAcceleration.RowCount = 2;
            tableLayoutPanelAcceleration.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanelAcceleration.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanelAcceleration.Size = new System.Drawing.Size(96, 150);
            tableLayoutPanelAcceleration.TabIndex = 25;
            // 
            // accelerationProgressBar
            // 
            accelerationProgressBar.ForeColor = System.Drawing.Color.DarkGreen;
            accelerationProgressBar.Location = new System.Drawing.Point(51, 33);
            accelerationProgressBar.Margin = new System.Windows.Forms.Padding(3, 13, 3, 13);
            accelerationProgressBar.Maximum = 1000;
            accelerationProgressBar.Name = "accelerationProgressBar";
            accelerationProgressBar.Size = new System.Drawing.Size(32, 104);
            accelerationProgressBar.Step = 100;
            accelerationProgressBar.TabIndex = 2;
            // 
            // accelerationTrackBar
            // 
            accelerationTrackBar.LargeChange = 100;
            accelerationTrackBar.Location = new System.Drawing.Point(3, 23);
            accelerationTrackBar.Maximum = 1000;
            accelerationTrackBar.Name = "accelerationTrackBar";
            accelerationTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            accelerationTrackBar.Size = new System.Drawing.Size(42, 124);
            accelerationTrackBar.TabIndex = 1;
            accelerationTrackBar.TickFrequency = 100;
            accelerationTrackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            accelerationTrackBar.ValueChanged += new System.EventHandler(accelerationTrackBar_ValueChanged);
            // 
            // labelAcceleration
            // 
            tableLayoutPanelAcceleration.SetColumnSpan(labelAcceleration, 2);
            labelAcceleration.Location = new System.Drawing.Point(3, 0);
            labelAcceleration.Name = "labelAcceleration";
            labelAcceleration.Size = new System.Drawing.Size(84, 20);
            labelAcceleration.TabIndex = 0;
            labelAcceleration.Text = "Ускорение";
            labelAcceleration.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanelRoll
            // 
            flowLayoutPanelRoll.AutoSize = true;
            flowLayoutPanelRoll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            flowLayoutPanelRoll.Controls.Add(labelRoll);
            flowLayoutPanelRoll.Controls.Add(rollControl);
            flowLayoutPanelRoll.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            flowLayoutPanelRoll.Location = new System.Drawing.Point(3, 259);
            flowLayoutPanelRoll.Name = "flowLayoutPanelRoll";
            flowLayoutPanelRoll.Size = new System.Drawing.Size(130, 150);
            flowLayoutPanelRoll.TabIndex = 24;
            // 
            // labelRoll
            // 
            labelRoll.Location = new System.Drawing.Point(3, 0);
            labelRoll.Name = "labelRoll";
            labelRoll.Size = new System.Drawing.Size(123, 20);
            labelRoll.TabIndex = 0;
            labelRoll.Text = "Перекат";
            labelRoll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rollControl
            // 
            stringFormat1.Alignment = System.Drawing.StringAlignment.Center;
            stringFormat1.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
            stringFormat1.LineAlignment = System.Drawing.StringAlignment.Center;
            stringFormat1.Trimming = System.Drawing.StringTrimming.None;
            rollControl.CenteredLayout = stringFormat1;
            rollControl.Location = new System.Drawing.Point(3, 23);
            rollControl.Name = "rollControl";
            rollControl.ReadOnly = false;
            rollControl.Roll = 0;
            rollControl.RollTo = 0;
            rollControl.Size = new System.Drawing.Size(124, 124);
            rollControl.TabIndex = 1;
            rollControl.OnRollToChanged += new System.EventHandler(rollControl_OnRollToChanged);
            // 
            // flowLayoutPanelHeading
            // 
            flowLayoutPanelHeading.AutoSize = true;
            flowLayoutPanelHeading.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            mainTableLayoutPanel.SetColumnSpan(flowLayoutPanelHeading, 2);
            flowLayoutPanelHeading.Controls.Add(labelHeading);
            flowLayoutPanelHeading.Controls.Add(headingControl);
            flowLayoutPanelHeading.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            flowLayoutPanelHeading.Location = new System.Drawing.Point(3, 3);
            flowLayoutPanelHeading.Name = "flowLayoutPanelHeading";
            flowLayoutPanelHeading.Size = new System.Drawing.Size(230, 250);
            flowLayoutPanelHeading.TabIndex = 23;
            // 
            // labelHeading
            // 
            labelHeading.Location = new System.Drawing.Point(3, 0);
            labelHeading.Name = "labelHeading";
            labelHeading.Size = new System.Drawing.Size(224, 20);
            labelHeading.TabIndex = 0;
            labelHeading.Text = "Курс";
            labelHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // headingControl
            // 
            headingControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            stringFormat2.Alignment = System.Drawing.StringAlignment.Center;
            stringFormat2.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
            stringFormat2.LineAlignment = System.Drawing.StringAlignment.Center;
            stringFormat2.Trimming = System.Drawing.StringTrimming.None;
            headingControl.CenteredLayout = stringFormat2;
            headingControl.Heading = 0;
            headingControl.HeadingTo = 0;
            headingControl.Location = new System.Drawing.Point(3, 23);
            headingControl.Name = "headingControl";
            headingControl.ReadOnly = false;
            headingControl.Size = new System.Drawing.Size(224, 224);
            headingControl.TabIndex = 1;
            headingControl.OnHeadingToChanged += new System.EventHandler(headingControl_OnHeadingToChanged);
            // 
            // ShipControl
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            AutoSize = true;
            AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            Controls.Add(mainTableLayoutPanel);
            Name = "ShipControl";
            Size = new System.Drawing.Size(249, 415);
            mainTableLayoutPanel.ResumeLayout(false);
            mainTableLayoutPanel.PerformLayout();
            tableLayoutPanelAcceleration.ResumeLayout(false);
            tableLayoutPanelAcceleration.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(accelerationTrackBar)).EndInit();
            flowLayoutPanelRoll.ResumeLayout(false);
            flowLayoutPanelHeading.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

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
