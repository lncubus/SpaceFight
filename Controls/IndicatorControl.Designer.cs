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
            this.labelAccelerationTitle = new System.Windows.Forms.Label();
            this.textAccelerationFull = new System.Windows.Forms.TextBox();
            this.textAccelerationX = new System.Windows.Forms.TextBox();
            this.textAccelerationY = new System.Windows.Forms.TextBox();
            this.textAccelerationAngle = new System.Windows.Forms.TextBox();
            this.textSpeedAngle = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel.ColumnCount = 3;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel.Controls.Add(this.textLocationX, 0, 7);
            this.tableLayoutPanel.Controls.Add(this.textLocationY, 1, 7);
            this.tableLayoutPanel.Controls.Add(this.textSpeedY, 1, 5);
            this.tableLayoutPanel.Controls.Add(this.textSpeedFull, 0, 4);
            this.tableLayoutPanel.Controls.Add(this.labelSpeedTitle, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.textSpeedX, 0, 5);
            this.tableLayoutPanel.Controls.Add(this.labelLocationTitle, 0, 6);
            this.tableLayoutPanel.Controls.Add(this.labelAccelerationTitle, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.textAccelerationFull, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.textAccelerationX, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.textAccelerationY, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.textAccelerationAngle, 2, 1);
            this.tableLayoutPanel.Controls.Add(this.textSpeedAngle, 2, 4);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 9;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(344, 170);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // textLocationX
            // 
            this.textLocationX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textLocationX.Location = new System.Drawing.Point(3, 146);
            this.textLocationX.Name = "textLocationX";
            this.textLocationX.ReadOnly = true;
            this.textLocationX.Size = new System.Drawing.Size(166, 20);
            this.textLocationX.TabIndex = 9;
            // 
            // textLocationY
            // 
            this.textLocationY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel.SetColumnSpan(this.textLocationY, 2);
            this.textLocationY.Location = new System.Drawing.Point(175, 146);
            this.textLocationY.Name = "textLocationY";
            this.textLocationY.ReadOnly = true;
            this.textLocationY.Size = new System.Drawing.Size(166, 20);
            this.textLocationY.TabIndex = 10;
            // 
            // textSpeedY
            // 
            this.textSpeedY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel.SetColumnSpan(this.textSpeedY, 2);
            this.textSpeedY.Location = new System.Drawing.Point(175, 107);
            this.textSpeedY.Name = "textSpeedY";
            this.textSpeedY.ReadOnly = true;
            this.textSpeedY.Size = new System.Drawing.Size(166, 20);
            this.textSpeedY.TabIndex = 7;
            // 
            // textSpeedFull
            // 
            this.textSpeedFull.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel.SetColumnSpan(this.textSpeedFull, 2);
            this.textSpeedFull.Location = new System.Drawing.Point(3, 81);
            this.textSpeedFull.Name = "textSpeedFull";
            this.textSpeedFull.ReadOnly = true;
            this.textSpeedFull.Size = new System.Drawing.Size(252, 20);
            this.textSpeedFull.TabIndex = 5;
            // 
            // labelSpeedTitle
            // 
            this.labelSpeedTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSpeedTitle.AutoSize = true;
            this.tableLayoutPanel.SetColumnSpan(this.labelSpeedTitle, 3);
            this.labelSpeedTitle.Location = new System.Drawing.Point(3, 65);
            this.labelSpeedTitle.Name = "labelSpeedTitle";
            this.labelSpeedTitle.Size = new System.Drawing.Size(338, 13);
            this.labelSpeedTitle.TabIndex = 4;
            this.labelSpeedTitle.Text = "Скорость";
            this.labelSpeedTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textSpeedX
            // 
            this.textSpeedX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textSpeedX.Location = new System.Drawing.Point(3, 107);
            this.textSpeedX.Name = "textSpeedX";
            this.textSpeedX.ReadOnly = true;
            this.textSpeedX.Size = new System.Drawing.Size(166, 20);
            this.textSpeedX.TabIndex = 6;
            // 
            // labelLocationTitle
            // 
            this.labelLocationTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelLocationTitle.AutoSize = true;
            this.tableLayoutPanel.SetColumnSpan(this.labelLocationTitle, 3);
            this.labelLocationTitle.Location = new System.Drawing.Point(3, 130);
            this.labelLocationTitle.Name = "labelLocationTitle";
            this.labelLocationTitle.Size = new System.Drawing.Size(338, 13);
            this.labelLocationTitle.TabIndex = 8;
            this.labelLocationTitle.Text = "Положение";
            this.labelLocationTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelAccelerationTitle
            // 
            this.labelAccelerationTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelAccelerationTitle.AutoSize = true;
            this.tableLayoutPanel.SetColumnSpan(this.labelAccelerationTitle, 3);
            this.labelAccelerationTitle.Location = new System.Drawing.Point(3, 0);
            this.labelAccelerationTitle.Name = "labelAccelerationTitle";
            this.labelAccelerationTitle.Size = new System.Drawing.Size(338, 13);
            this.labelAccelerationTitle.TabIndex = 0;
            this.labelAccelerationTitle.Text = "Ускорение";
            this.labelAccelerationTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textAccelerationFull
            // 
            this.textAccelerationFull.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel.SetColumnSpan(this.textAccelerationFull, 2);
            this.textAccelerationFull.Location = new System.Drawing.Point(3, 16);
            this.textAccelerationFull.Name = "textAccelerationFull";
            this.textAccelerationFull.ReadOnly = true;
            this.textAccelerationFull.Size = new System.Drawing.Size(252, 20);
            this.textAccelerationFull.TabIndex = 1;
            // 
            // textAccelerationX
            // 
            this.textAccelerationX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textAccelerationX.Location = new System.Drawing.Point(3, 42);
            this.textAccelerationX.Name = "textAccelerationX";
            this.textAccelerationX.ReadOnly = true;
            this.textAccelerationX.Size = new System.Drawing.Size(166, 20);
            this.textAccelerationX.TabIndex = 2;
            // 
            // textAccelerationY
            // 
            this.textAccelerationY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel.SetColumnSpan(this.textAccelerationY, 2);
            this.textAccelerationY.Location = new System.Drawing.Point(175, 42);
            this.textAccelerationY.Name = "textAccelerationY";
            this.textAccelerationY.ReadOnly = true;
            this.textAccelerationY.Size = new System.Drawing.Size(166, 20);
            this.textAccelerationY.TabIndex = 3;
            // 
            // textAccelerationAngle
            // 
            this.textAccelerationAngle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textAccelerationAngle.Location = new System.Drawing.Point(261, 16);
            this.textAccelerationAngle.Name = "textAccelerationAngle";
            this.textAccelerationAngle.ReadOnly = true;
            this.textAccelerationAngle.Size = new System.Drawing.Size(80, 20);
            this.textAccelerationAngle.TabIndex = 11;
            // 
            // textSpeedAngle
            // 
            this.textSpeedAngle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textSpeedAngle.Location = new System.Drawing.Point(261, 81);
            this.textSpeedAngle.Name = "textSpeedAngle";
            this.textSpeedAngle.ReadOnly = true;
            this.textSpeedAngle.Size = new System.Drawing.Size(80, 20);
            this.textSpeedAngle.TabIndex = 11;
            // 
            // IndicatorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "IndicatorControl";
            this.Size = new System.Drawing.Size(344, 170);
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
        private System.Windows.Forms.Label labelAccelerationTitle;
        private System.Windows.Forms.TextBox textAccelerationFull;
        private System.Windows.Forms.TextBox textAccelerationX;
        private System.Windows.Forms.TextBox textAccelerationY;
        private System.Windows.Forms.TextBox textAccelerationAngle;
        private System.Windows.Forms.TextBox textSpeedAngle;


    }
}
