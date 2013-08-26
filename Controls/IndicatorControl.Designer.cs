namespace SF.Controls
{
    partial class IndicatorControl
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.textLocationX = new System.Windows.Forms.TextBox();
            this.textLocationY = new System.Windows.Forms.TextBox();
            this.textSpeedY = new System.Windows.Forms.TextBox();
            this.textSpeedFull = new System.Windows.Forms.TextBox();
            this.labelSpeedTitle = new System.Windows.Forms.Label();
            this.textSpeedX = new System.Windows.Forms.TextBox();
            this.labelLocationTitle = new System.Windows.Forms.Label();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Controls.Add(this.textLocationX, 0, 4);
            this.tableLayoutPanel.Controls.Add(this.textLocationY, 1, 4);
            this.tableLayoutPanel.Controls.Add(this.textSpeedY, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.textSpeedFull, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.labelSpeedTitle, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.textSpeedX, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.labelLocationTitle, 0, 3);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 5;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(344, 107);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // textLocationX
            // 
            this.textLocationX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textLocationX.Location = new System.Drawing.Point(3, 81);
            this.textLocationX.Name = "textLocationX";
            this.textLocationX.ReadOnly = true;
            this.textLocationX.Size = new System.Drawing.Size(166, 20);
            this.textLocationX.TabIndex = 6;
            // 
            // textLocationY
            // 
            this.textLocationY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textLocationY.Location = new System.Drawing.Point(175, 81);
            this.textLocationY.Name = "textLocationY";
            this.textLocationY.ReadOnly = true;
            this.textLocationY.Size = new System.Drawing.Size(166, 20);
            this.textLocationY.TabIndex = 7;
            // 
            // textSpeedY
            // 
            this.textSpeedY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textSpeedY.Location = new System.Drawing.Point(175, 42);
            this.textSpeedY.Name = "textSpeedY";
            this.textSpeedY.ReadOnly = true;
            this.textSpeedY.Size = new System.Drawing.Size(166, 20);
            this.textSpeedY.TabIndex = 3;
            // 
            // textSpeedFull
            // 
            this.textSpeedFull.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel.SetColumnSpan(this.textSpeedFull, 2);
            this.textSpeedFull.Location = new System.Drawing.Point(3, 16);
            this.textSpeedFull.Name = "textSpeedFull";
            this.textSpeedFull.ReadOnly = true;
            this.textSpeedFull.Size = new System.Drawing.Size(338, 20);
            this.textSpeedFull.TabIndex = 1;
            // 
            // labelSpeedTitle
            // 
            this.labelSpeedTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSpeedTitle.AutoSize = true;
            this.tableLayoutPanel.SetColumnSpan(this.labelSpeedTitle, 2);
            this.labelSpeedTitle.Location = new System.Drawing.Point(3, 0);
            this.labelSpeedTitle.Name = "labelSpeedTitle";
            this.labelSpeedTitle.Size = new System.Drawing.Size(338, 13);
            this.labelSpeedTitle.TabIndex = 0;
            this.labelSpeedTitle.Text = "Скорость";
            this.labelSpeedTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textSpeedX
            // 
            this.textSpeedX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textSpeedX.Location = new System.Drawing.Point(3, 42);
            this.textSpeedX.Name = "textSpeedX";
            this.textSpeedX.ReadOnly = true;
            this.textSpeedX.Size = new System.Drawing.Size(166, 20);
            this.textSpeedX.TabIndex = 5;
            // 
            // labelLocationTitle
            // 
            this.labelLocationTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelLocationTitle.AutoSize = true;
            this.tableLayoutPanel.SetColumnSpan(this.labelLocationTitle, 2);
            this.labelLocationTitle.Location = new System.Drawing.Point(3, 65);
            this.labelLocationTitle.Name = "labelLocationTitle";
            this.labelLocationTitle.Size = new System.Drawing.Size(338, 13);
            this.labelLocationTitle.TabIndex = 2;
            this.labelLocationTitle.Text = "Положение";
            this.labelLocationTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // IndicatorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "IndicatorControl";
            this.Size = new System.Drawing.Size(344, 107);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.TextBox textLocationX;
        private System.Windows.Forms.TextBox textLocationY;
        private System.Windows.Forms.TextBox textSpeedY;
        private System.Windows.Forms.TextBox textSpeedFull;
        private System.Windows.Forms.Label labelSpeedTitle;
        private System.Windows.Forms.TextBox textSpeedX;
        private System.Windows.Forms.Label labelLocationTitle;


    }
}
