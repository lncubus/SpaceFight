namespace Gunner
{
    partial class GunnerForm
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
            this.scaleControl = new SF.Controls.ScaleControl();
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
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.Controls.Add(this.spaceGridControl, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.scaleControl, 1, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(792, 573);
            this.tableLayoutPanel.TabIndex = 0;
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
            this.spaceGridControl.Polar = true;
            this.spaceGridControl.ReadOnly = false;
            this.spaceGridControl.Rotation = 0D;
            this.tableLayoutPanel.SetRowSpan(this.spaceGridControl, 2);
            this.spaceGridControl.Size = new System.Drawing.Size(531, 567);
            this.spaceGridControl.TabIndex = 1;
            this.spaceGridControl.WorldScale = 2000000D;
            // 
            // scaleControl
            // 
            this.scaleControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.scaleControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scaleControl.Location = new System.Drawing.Point(540, 3);
            this.scaleControl.MaxValue = 1000000000D;
            this.scaleControl.MinValue = 1000D;
            this.scaleControl.Name = "scaleControl";
            this.scaleControl.Size = new System.Drawing.Size(249, 29);
            this.scaleControl.TabIndex = 3;
            this.scaleControl.Unit = "км";
            this.scaleControl.Value = 2000000D;
            // 
            // GunnerForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.MintCream;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GunnerForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerUpdate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private SF.Controls.SpaceGridControl spaceGridControl;
        private SF.Controls.ScaleControl scaleControl;
    }
}

