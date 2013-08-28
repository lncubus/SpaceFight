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
            tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            textLocationX = new System.Windows.Forms.TextBox();
            textLocationY = new System.Windows.Forms.TextBox();
            textSpeedY = new System.Windows.Forms.TextBox();
            textSpeedFull = new System.Windows.Forms.TextBox();
            labelSpeedTitle = new System.Windows.Forms.Label();
            textSpeedX = new System.Windows.Forms.TextBox();
            labelLocationTitle = new System.Windows.Forms.Label();
            labelAccelerationTitle = new System.Windows.Forms.Label();
            textAccelerationFull = new System.Windows.Forms.TextBox();
            textAccelerationX = new System.Windows.Forms.TextBox();
            textAccelerationY = new System.Windows.Forms.TextBox();
            tableLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel.Controls.Add(textLocationX, 0, 7);
            tableLayoutPanel.Controls.Add(textLocationY, 1, 7);
            tableLayoutPanel.Controls.Add(textSpeedY, 1, 5);
            tableLayoutPanel.Controls.Add(textSpeedFull, 0, 4);
            tableLayoutPanel.Controls.Add(labelSpeedTitle, 0, 3);
            tableLayoutPanel.Controls.Add(textSpeedX, 0, 5);
            tableLayoutPanel.Controls.Add(labelLocationTitle, 0, 6);
            tableLayoutPanel.Controls.Add(labelAccelerationTitle, 0, 0);
            tableLayoutPanel.Controls.Add(textAccelerationFull, 0, 1);
            tableLayoutPanel.Controls.Add(textAccelerationX, 0, 2);
            tableLayoutPanel.Controls.Add(textAccelerationY, 1, 2);
            tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 8;
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel.Size = new System.Drawing.Size(344, 170);
            tableLayoutPanel.TabIndex = 0;
            // 
            // textLocationX
            // 
            textLocationX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            textLocationX.Location = new System.Drawing.Point(3, 146);
            textLocationX.Name = "textLocationX";
            textLocationX.ReadOnly = true;
            textLocationX.Size = new System.Drawing.Size(166, 20);
            textLocationX.TabIndex = 9;
            // 
            // textLocationY
            // 
            textLocationY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            textLocationY.Location = new System.Drawing.Point(175, 146);
            textLocationY.Name = "textLocationY";
            textLocationY.ReadOnly = true;
            textLocationY.Size = new System.Drawing.Size(166, 20);
            textLocationY.TabIndex = 10;
            // 
            // textSpeedY
            // 
            textSpeedY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            textSpeedY.Location = new System.Drawing.Point(175, 107);
            textSpeedY.Name = "textSpeedY";
            textSpeedY.ReadOnly = true;
            textSpeedY.Size = new System.Drawing.Size(166, 20);
            textSpeedY.TabIndex = 7;
            // 
            // textSpeedFull
            // 
            textSpeedFull.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            tableLayoutPanel.SetColumnSpan(textSpeedFull, 2);
            textSpeedFull.Location = new System.Drawing.Point(3, 81);
            textSpeedFull.Name = "textSpeedFull";
            textSpeedFull.ReadOnly = true;
            textSpeedFull.Size = new System.Drawing.Size(338, 20);
            textSpeedFull.TabIndex = 5;
            // 
            // labelSpeedTitle
            // 
            labelSpeedTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            labelSpeedTitle.AutoSize = true;
            tableLayoutPanel.SetColumnSpan(labelSpeedTitle, 2);
            labelSpeedTitle.Location = new System.Drawing.Point(3, 65);
            labelSpeedTitle.Name = "labelSpeedTitle";
            labelSpeedTitle.Size = new System.Drawing.Size(338, 13);
            labelSpeedTitle.TabIndex = 4;
            labelSpeedTitle.Text = "Скорость";
            labelSpeedTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textSpeedX
            // 
            textSpeedX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            textSpeedX.Location = new System.Drawing.Point(3, 107);
            textSpeedX.Name = "textSpeedX";
            textSpeedX.ReadOnly = true;
            textSpeedX.Size = new System.Drawing.Size(166, 20);
            textSpeedX.TabIndex = 6;
            // 
            // labelLocationTitle
            // 
            labelLocationTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            labelLocationTitle.AutoSize = true;
            tableLayoutPanel.SetColumnSpan(labelLocationTitle, 2);
            labelLocationTitle.Location = new System.Drawing.Point(3, 130);
            labelLocationTitle.Name = "labelLocationTitle";
            labelLocationTitle.Size = new System.Drawing.Size(338, 13);
            labelLocationTitle.TabIndex = 8;
            labelLocationTitle.Text = "Положение";
            labelLocationTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelAccelerationTitle
            // 
            labelAccelerationTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            labelAccelerationTitle.AutoSize = true;
            tableLayoutPanel.SetColumnSpan(labelAccelerationTitle, 2);
            labelAccelerationTitle.Location = new System.Drawing.Point(3, 0);
            labelAccelerationTitle.Name = "labelAccelerationTitle";
            labelAccelerationTitle.Size = new System.Drawing.Size(338, 13);
            labelAccelerationTitle.TabIndex = 0;
            labelAccelerationTitle.Text = "Ускорение";
            labelAccelerationTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textAccelerationFull
            // 
            textAccelerationFull.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            tableLayoutPanel.SetColumnSpan(textAccelerationFull, 2);
            textAccelerationFull.Location = new System.Drawing.Point(3, 16);
            textAccelerationFull.Name = "textAccelerationFull";
            textAccelerationFull.ReadOnly = true;
            textAccelerationFull.Size = new System.Drawing.Size(338, 20);
            textAccelerationFull.TabIndex = 1;
            // 
            // textAccelerationX
            // 
            textAccelerationX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            textAccelerationX.Location = new System.Drawing.Point(3, 42);
            textAccelerationX.Name = "textAccelerationX";
            textAccelerationX.ReadOnly = true;
            textAccelerationX.Size = new System.Drawing.Size(166, 20);
            textAccelerationX.TabIndex = 2;
            // 
            // textAccelerationY
            // 
            textAccelerationY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            textAccelerationY.Location = new System.Drawing.Point(175, 42);
            textAccelerationY.Name = "textAccelerationY";
            textAccelerationY.ReadOnly = true;
            textAccelerationY.Size = new System.Drawing.Size(166, 20);
            textAccelerationY.TabIndex = 3;
            // 
            // IndicatorControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel);
            Name = "IndicatorControl";
            Size = new System.Drawing.Size(344, 170);
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            ResumeLayout(false);

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


    }
}
