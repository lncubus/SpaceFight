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
            this.indicatorControl = new SF.Controls.IndicatorControl();
            this.spaceGridControl = new SF.Controls.SpaceGridControl();
            this.scaleControl = new SF.Controls.ScaleControl();
            this.panelFire = new System.Windows.Forms.Panel();
            this.labelBoard = new System.Windows.Forms.Label();
            this.buttonFire = new System.Windows.Forms.Button();
            this.checkBoxFriendlyFire = new System.Windows.Forms.CheckBox();
            this.tabControlWeapons = new System.Windows.Forms.TabControl();
            this.tabPageMissiles = new System.Windows.Forms.TabPage();
            this.tabPageLACs = new System.Windows.Forms.TabPage();
            this.tableLayoutPanelMissiles = new System.Windows.Forms.TableLayoutPanel();
            this.missileControl = new SF.Controls.MissileControl();
            this.profileButtonsPanel = new SF.Controls.ProfileButtonsPanel();
            this.tableLayoutPanel.SuspendLayout();
            this.panelFire.SuspendLayout();
            this.tabControlWeapons.SuspendLayout();
            this.tabPageMissiles.SuspendLayout();
            this.tableLayoutPanelMissiles.SuspendLayout();
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
            this.tableLayoutPanel.Controls.Add(this.indicatorControl, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.spaceGridControl, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.scaleControl, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.panelFire, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.tabControlWeapons, 1, 2);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(792, 573);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // indicatorControl
            // 
            this.indicatorControl.Location = new System.Drawing.Point(540, 400);
            this.indicatorControl.Name = "indicatorControl";
            this.indicatorControl.Size = new System.Drawing.Size(249, 170);
            this.indicatorControl.TabIndex = 5;
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
            this.spaceGridControl.Missiles = null;
            this.spaceGridControl.Name = "spaceGridControl";
            this.spaceGridControl.Options = SF.Controls.SpaceGridControl.DrawingOptions.None;
            this.spaceGridControl.Polar = true;
            this.spaceGridControl.ReadOnly = false;
            this.spaceGridControl.Rotation = 0D;
            this.tableLayoutPanel.SetRowSpan(this.spaceGridControl, 4);
            this.spaceGridControl.Selectable = SF.Controls.SpaceGridControl.SelectableObjects.None;
            this.spaceGridControl.Selected = null;
            this.spaceGridControl.Ships = null;
            this.spaceGridControl.Size = new System.Drawing.Size(531, 567);
            this.spaceGridControl.Stars = null;
            this.spaceGridControl.StaticGrid = true;
            this.spaceGridControl.TabIndex = 1;
            this.spaceGridControl.WorldScale = 2000000D;
            this.spaceGridControl.ParticleSelected += new System.EventHandler(this.spaceGridControl_ParticleSelected);
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
            this.panelFire.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelFire.Controls.Add(this.labelBoard);
            this.panelFire.Controls.Add(this.buttonFire);
            this.panelFire.Controls.Add(this.checkBoxFriendlyFire);
            this.panelFire.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFire.Location = new System.Drawing.Point(540, 38);
            this.panelFire.Name = "panelFire";
            this.panelFire.Size = new System.Drawing.Size(249, 63);
            this.panelFire.TabIndex = 4;
            // 
            // labelBoard
            // 
            this.labelBoard.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelBoard.Location = new System.Drawing.Point(0, 40);
            this.labelBoard.Name = "labelBoard";
            this.labelBoard.Size = new System.Drawing.Size(249, 23);
            this.labelBoard.TabIndex = 2;
            this.labelBoard.Text = "Цель не выбрана";
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
            // tabControlWeapons
            // 
            this.tabControlWeapons.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControlWeapons.Controls.Add(this.tabPageMissiles);
            this.tabControlWeapons.Controls.Add(this.tabPageLACs);
            this.tabControlWeapons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlWeapons.Location = new System.Drawing.Point(540, 107);
            this.tabControlWeapons.Name = "tabControlWeapons";
            this.tabControlWeapons.SelectedIndex = 0;
            this.tabControlWeapons.Size = new System.Drawing.Size(249, 287);
            this.tabControlWeapons.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControlWeapons.TabIndex = 6;
            // 
            // tabPageMissiles
            // 
            this.tabPageMissiles.BackColor = System.Drawing.Color.MintCream;
            this.tabPageMissiles.Controls.Add(this.tableLayoutPanelMissiles);
            this.tabPageMissiles.Location = new System.Drawing.Point(4, 25);
            this.tabPageMissiles.Name = "tabPageMissiles";
            this.tabPageMissiles.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMissiles.Size = new System.Drawing.Size(241, 258);
            this.tabPageMissiles.TabIndex = 0;
            this.tabPageMissiles.Text = "Ракеты";
            // 
            // tabPageLACs
            // 
            this.tabPageLACs.BackColor = System.Drawing.Color.MintCream;
            this.tabPageLACs.Location = new System.Drawing.Point(4, 25);
            this.tabPageLACs.Name = "tabPageLACs";
            this.tabPageLACs.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLACs.Size = new System.Drawing.Size(241, 258);
            this.tabPageLACs.TabIndex = 1;
            this.tabPageLACs.Text = "ЛАК";
            // 
            // tableLayoutPanelMissiles
            // 
            this.tableLayoutPanelMissiles.ColumnCount = 1;
            this.tableLayoutPanelMissiles.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMissiles.Controls.Add(this.missileControl, 0, 0);
            this.tableLayoutPanelMissiles.Controls.Add(this.profileButtonsPanel, 0, 1);
            this.tableLayoutPanelMissiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMissiles.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelMissiles.Name = "tableLayoutPanelMissiles";
            this.tableLayoutPanelMissiles.RowCount = 2;
            this.tableLayoutPanelMissiles.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMissiles.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelMissiles.Size = new System.Drawing.Size(235, 252);
            this.tableLayoutPanelMissiles.TabIndex = 0;
            // 
            // missileControl
            // 
            this.missileControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.missileControl.Location = new System.Drawing.Point(3, 3);
            this.missileControl.MissileClass = null;
            this.missileControl.Name = "missileControl";
            this.missileControl.ShipClass = null;
            this.missileControl.Size = new System.Drawing.Size(229, 120);
            this.missileControl.TabIndex = 8;
            // 
            // profileButtonsPanel
            // 
            this.profileButtonsPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.profileButtonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.profileButtonsPanel.Jammer = 0;
            this.profileButtonsPanel.Location = new System.Drawing.Point(3, 129);
            this.profileButtonsPanel.Name = "profileButtonsPanel";
            this.profileButtonsPanel.Size = new System.Drawing.Size(229, 120);
            this.profileButtonsPanel.TabIndex = 9;
            // 
            // GunnerForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.MintCream;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.KeyPreview = true;
            this.Name = "GunnerForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GunnerForm_KeyPress);
            this.tableLayoutPanel.ResumeLayout(false);
            this.panelFire.ResumeLayout(false);
            this.panelFire.PerformLayout();
            this.tabControlWeapons.ResumeLayout(false);
            this.tabPageMissiles.ResumeLayout(false);
            this.tableLayoutPanelMissiles.ResumeLayout(false);
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
        private SF.Controls.IndicatorControl indicatorControl;
        private System.Windows.Forms.TabControl tabControlWeapons;
        private System.Windows.Forms.TabPage tabPageMissiles;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMissiles;
        private SF.Controls.MissileControl missileControl;
        private SF.Controls.ProfileButtonsPanel profileButtonsPanel;
        private System.Windows.Forms.TabPage tabPageLACs;
    }
}

