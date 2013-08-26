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
            this.panelFire = new System.Windows.Forms.Panel();
            this.labelBoard = new System.Windows.Forms.Label();
            this.buttonFire = new System.Windows.Forms.Button();
            this.checkBoxFriendlyFire = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel.SuspendLayout();
            this.panelFire.SuspendLayout();
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
            this.tableLayoutPanel.Controls.Add(this.panelFire, 1, 1);
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
            this.spaceGridControl.SelectedShip = null;
            this.spaceGridControl.Size = new System.Drawing.Size(531, 567);
            this.spaceGridControl.StaticGrid = true;
            this.spaceGridControl.TabIndex = 1;
            this.spaceGridControl.WorldScale = 2000000D;
            this.spaceGridControl.ShipSelected += new System.EventHandler(this.spaceGridControl_ShipSelected);
            this.spaceGridControl.DoubleClick += new System.EventHandler(this.spaceGridControl_DoubleClick);
            // 
            // scaleControl
            // 
            this.scaleControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.scaleControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scaleControl.Location = new System.Drawing.Point(540, 3);
            this.scaleControl.MaxValue = 5000000000D;
            this.scaleControl.MinValue = 500D;
            this.scaleControl.Name = "scaleControl";
            this.scaleControl.Size = new System.Drawing.Size(249, 29);
            this.scaleControl.TabIndex = 3;
            this.scaleControl.Unit = "км";
            this.scaleControl.Value = 2000000D;
            // 
            // panelFire
            // 
            this.panelFire.Controls.Add(this.labelBoard);
            this.panelFire.Controls.Add(this.buttonFire);
            this.panelFire.Controls.Add(this.checkBoxFriendlyFire);
            this.panelFire.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFire.Location = new System.Drawing.Point(540, 38);
            this.panelFire.Name = "panelFire";
            this.panelFire.Size = new System.Drawing.Size(249, 532);
            this.panelFire.TabIndex = 4;
            // 
            // labelBoard
            // 
            this.labelBoard.AutoSize = true;
            this.labelBoard.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelBoard.Location = new System.Drawing.Point(0, 40);
            this.labelBoard.Name = "labelBoard";
            this.labelBoard.Size = new System.Drawing.Size(0, 13);
            this.labelBoard.TabIndex = 2;
            this.labelBoard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonFire
            // 
            this.buttonFire.BackColor = System.Drawing.Color.Maroon;
            this.buttonFire.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonFire.Enabled = false;
            this.buttonFire.ForeColor = System.Drawing.Color.White;
            this.buttonFire.Location = new System.Drawing.Point(0, 17);
            this.buttonFire.Name = "buttonFire";
            this.buttonFire.Size = new System.Drawing.Size(249, 23);
            this.buttonFire.TabIndex = 1;
            this.buttonFire.Text = "Залп";
            this.buttonFire.UseVisualStyleBackColor = false;
            this.buttonFire.Click += new System.EventHandler(this.buttonFire_Click);
            // 
            // checkBoxFriendlyFire
            // 
            this.checkBoxFriendlyFire.AutoSize = true;
            this.checkBoxFriendlyFire.Dock = System.Windows.Forms.DockStyle.Top;
            this.checkBoxFriendlyFire.Location = new System.Drawing.Point(0, 0);
            this.checkBoxFriendlyFire.Name = "checkBoxFriendlyFire";
            this.checkBoxFriendlyFire.Size = new System.Drawing.Size(249, 17);
            this.checkBoxFriendlyFire.TabIndex = 0;
            this.checkBoxFriendlyFire.Text = "Разрешить огонь по своим";
            this.checkBoxFriendlyFire.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxFriendlyFire.UseVisualStyleBackColor = true;
            this.checkBoxFriendlyFire.CheckedChanged += new System.EventHandler(this.checkBoxFriendlyFire_CheckedChanged);
            // 
            // GunnerForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.MintCream;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "GunnerForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tableLayoutPanel.ResumeLayout(false);
            this.panelFire.ResumeLayout(false);
            this.panelFire.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerUpdate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private SF.Controls.SpaceGridControl spaceGridControl;
        private SF.Controls.ScaleControl scaleControl;
        private System.Windows.Forms.Panel panelFire;
        private System.Windows.Forms.Button buttonFire;
        private System.Windows.Forms.CheckBox checkBoxFriendlyFire;
        private System.Windows.Forms.Label labelBoard;
    }
}

