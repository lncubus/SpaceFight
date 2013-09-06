namespace SF.Controls
{
    partial class ProfileButtonsPanel
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
            this.buttonAttackProfile = new System.Windows.Forms.Button();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBoxProfile = new System.Windows.Forms.PictureBox();
            this.labelJammer = new System.Windows.Forms.Label();
            this.comboBoxJammer = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProfile)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonAttackProfile
            // 
            this.buttonAttackProfile.Location = new System.Drawing.Point(3, 95);
            this.buttonAttackProfile.Name = "buttonAttackProfile";
            this.buttonAttackProfile.Size = new System.Drawing.Size(78, 23);
            this.buttonAttackProfile.TabIndex = 3;
            this.buttonAttackProfile.Text = "Профиль";
            this.buttonAttackProfile.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel.ColumnCount = 3;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.Controls.Add(this.comboBoxJammer, 2, 1);
            this.tableLayoutPanel.Controls.Add(this.buttonAttackProfile, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.pictureBoxProfile, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.labelJammer, 1, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.Size = new System.Drawing.Size(249, 121);
            this.tableLayoutPanel.TabIndex = 4;
            // 
            // pictureBoxProfile
            // 
            this.tableLayoutPanel.SetColumnSpan(this.pictureBoxProfile, 3);
            this.pictureBoxProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxProfile.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxProfile.Name = "pictureBoxProfile";
            this.pictureBoxProfile.Size = new System.Drawing.Size(243, 86);
            this.pictureBoxProfile.TabIndex = 6;
            this.pictureBoxProfile.TabStop = false;
            // 
            // labelJammer
            // 
            this.labelJammer.AutoSize = true;
            this.labelJammer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelJammer.Location = new System.Drawing.Point(87, 92);
            this.labelJammer.Name = "labelJammer";
            this.labelJammer.Size = new System.Drawing.Size(28, 29);
            this.labelJammer.TabIndex = 7;
            this.labelJammer.Text = "РЭБ";
            this.labelJammer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBoxJammer
            // 
            this.comboBoxJammer.DropDownHeight = 39;
            this.comboBoxJammer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxJammer.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxJammer.FormattingEnabled = true;
            this.comboBoxJammer.Items.AddRange(new object[] {
            "Вертикальная",
            "Горизонтальная",
            "Круговая"});
            this.comboBoxJammer.Location = new System.Drawing.Point(121, 95);
            this.comboBoxJammer.Name = "comboBoxJammer";
            this.comboBoxJammer.Size = new System.Drawing.Size(125, 21);
            this.comboBoxJammer.TabIndex = 8;
            // 
            // ProfileButtonsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "ProfileButtonsPanel";
            this.Size = new System.Drawing.Size(249, 121);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProfile)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonAttackProfile;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.PictureBox pictureBoxProfile;
        private System.Windows.Forms.Label labelJammer;
        private System.Windows.Forms.ComboBox comboBoxJammer;
    }
}
