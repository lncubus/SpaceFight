namespace SF.Controls
{
    partial class LogonDialog
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
            this.labelState = new System.Windows.Forms.Label();
            this.comboBoxState = new System.Windows.Forms.ComboBox();
            this.labelShip = new System.Windows.Forms.Label();
            this.comboBoxShip = new System.Windows.Forms.ComboBox();
            this.buttonOkay = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelState
            // 
            this.labelState.AutoSize = true;
            this.labelState.Location = new System.Drawing.Point(12, 9);
            this.labelState.Name = "labelState";
            this.labelState.Size = new System.Drawing.Size(71, 13);
            this.labelState.TabIndex = 0;
            this.labelState.Text = "Государство";
            // 
            // comboBoxState
            // 
            this.comboBoxState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxState.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxState.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxState.FormattingEnabled = true;
            this.comboBoxState.Location = new System.Drawing.Point(12, 25);
            this.comboBoxState.Name = "comboBoxState";
            this.comboBoxState.Size = new System.Drawing.Size(268, 21);
            this.comboBoxState.TabIndex = 1;
            this.comboBoxState.SelectedValueChanged += new System.EventHandler(this.comboBoxState_SelectedValueChanged);
            // 
            // labelShip
            // 
            this.labelShip.AutoSize = true;
            this.labelShip.Location = new System.Drawing.Point(12, 53);
            this.labelShip.Name = "labelShip";
            this.labelShip.Size = new System.Drawing.Size(50, 13);
            this.labelShip.TabIndex = 2;
            this.labelShip.Text = "Корабль";
            // 
            // comboBoxShip
            // 
            this.comboBoxShip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxShip.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxShip.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxShip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxShip.FormattingEnabled = true;
            this.comboBoxShip.Location = new System.Drawing.Point(12, 69);
            this.comboBoxShip.Name = "comboBoxShip";
            this.comboBoxShip.Size = new System.Drawing.Size(268, 21);
            this.comboBoxShip.TabIndex = 3;
            this.comboBoxShip.SelectedValueChanged += new System.EventHandler(this.comboBoxShip_SelectedValueChanged);
            // 
            // buttonOkay
            // 
            this.buttonOkay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOkay.Enabled = false;
            this.buttonOkay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOkay.Location = new System.Drawing.Point(204, 109);
            this.buttonOkay.Name = "buttonOkay";
            this.buttonOkay.Size = new System.Drawing.Size(75, 23);
            this.buttonOkay.TabIndex = 4;
            this.buttonOkay.Text = "Вход";
            this.buttonOkay.UseVisualStyleBackColor = true;
            this.buttonOkay.Click += new System.EventHandler(this.buttonOkay_Click);
            // 
            // LogonDialog
            // 
            this.AcceptButton = this.buttonOkay;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 142);
            this.Controls.Add(this.buttonOkay);
            this.Controls.Add(this.comboBoxShip);
            this.Controls.Add(this.comboBoxState);
            this.Controls.Add(this.labelShip);
            this.Controls.Add(this.labelState);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "LogonDialog";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.LogonDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelState;
        private System.Windows.Forms.ComboBox comboBoxState;
        private System.Windows.Forms.Label labelShip;
        private System.Windows.Forms.ComboBox comboBoxShip;
        private System.Windows.Forms.Button buttonOkay;
    }
}