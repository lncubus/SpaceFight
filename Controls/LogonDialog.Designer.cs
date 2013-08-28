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
            labelState = new System.Windows.Forms.Label();
            comboBoxState = new System.Windows.Forms.ComboBox();
            labelShip = new System.Windows.Forms.Label();
            comboBoxShip = new System.Windows.Forms.ComboBox();
            buttonOkay = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // labelState
            // 
            labelState.AutoSize = true;
            labelState.Location = new System.Drawing.Point(12, 9);
            labelState.Name = "labelState";
            labelState.Size = new System.Drawing.Size(71, 13);
            labelState.TabIndex = 0;
            labelState.Text = "Государство";
            // 
            // comboBoxState
            // 
            comboBoxState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            comboBoxState.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            comboBoxState.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            comboBoxState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxState.FormattingEnabled = true;
            comboBoxState.Location = new System.Drawing.Point(12, 25);
            comboBoxState.Name = "comboBoxState";
            comboBoxState.Size = new System.Drawing.Size(268, 21);
            comboBoxState.TabIndex = 1;
            comboBoxState.SelectedValueChanged += new System.EventHandler(comboBoxState_SelectedValueChanged);
            // 
            // labelShip
            // 
            labelShip.AutoSize = true;
            labelShip.Location = new System.Drawing.Point(12, 53);
            labelShip.Name = "labelShip";
            labelShip.Size = new System.Drawing.Size(50, 13);
            labelShip.TabIndex = 2;
            labelShip.Text = "Корабль";
            // 
            // comboBoxShip
            // 
            comboBoxShip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            comboBoxShip.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            comboBoxShip.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            comboBoxShip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxShip.FormattingEnabled = true;
            comboBoxShip.Location = new System.Drawing.Point(12, 69);
            comboBoxShip.Name = "comboBoxShip";
            comboBoxShip.Size = new System.Drawing.Size(268, 21);
            comboBoxShip.TabIndex = 3;
            comboBoxShip.SelectedValueChanged += new System.EventHandler(comboBoxShip_SelectedValueChanged);
            // 
            // buttonOkay
            // 
            buttonOkay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            buttonOkay.Enabled = false;
            buttonOkay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonOkay.Location = new System.Drawing.Point(204, 109);
            buttonOkay.Name = "buttonOkay";
            buttonOkay.Size = new System.Drawing.Size(75, 23);
            buttonOkay.TabIndex = 4;
            buttonOkay.Text = "Вход";
            buttonOkay.UseVisualStyleBackColor = true;
            buttonOkay.Click += new System.EventHandler(buttonOkay_Click);
            // 
            // LogonDialog
            // 
            AcceptButton = buttonOkay;
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(292, 142);
            Controls.Add(buttonOkay);
            Controls.Add(comboBoxShip);
            Controls.Add(comboBoxState);
            Controls.Add(labelShip);
            Controls.Add(labelState);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            Name = "LogonDialog";
            RightToLeft = System.Windows.Forms.RightToLeft.No;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Load += new System.EventHandler(LogonDialog_Load);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelState;
        private System.Windows.Forms.ComboBox comboBoxState;
        private System.Windows.Forms.Label labelShip;
        private System.Windows.Forms.ComboBox comboBoxShip;
        private System.Windows.Forms.Button buttonOkay;
    }
}