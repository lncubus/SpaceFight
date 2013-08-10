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
            this.labelLocationX = new System.Windows.Forms.Label();
            this.labelLocationY = new System.Windows.Forms.Label();
            this.labelSpeedY = new System.Windows.Forms.Label();
            this.labelSpeedFull = new System.Windows.Forms.Label();
            this.labelSpeedTitle = new System.Windows.Forms.Label();
            this.labelSpeedX = new System.Windows.Forms.Label();
            this.labelLocationTitle = new System.Windows.Forms.Label();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Controls.Add(this.labelLocationX, 0, 4);
            this.tableLayoutPanel.Controls.Add(this.labelLocationY, 1, 4);
            this.tableLayoutPanel.Controls.Add(this.labelSpeedY, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.labelSpeedFull, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.labelSpeedTitle, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.labelSpeedX, 0, 2);
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
            this.tableLayoutPanel.Size = new System.Drawing.Size(344, 119);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // labelLocationX
            // 
            this.labelLocationX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelLocationX.Location = new System.Drawing.Point(3, 92);
            this.labelLocationX.Name = "labelLocationX";
            this.labelLocationX.Size = new System.Drawing.Size(166, 23);
            this.labelLocationX.TabIndex = 6;
            this.labelLocationX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelLocationY
            // 
            this.labelLocationY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelLocationY.Location = new System.Drawing.Point(175, 92);
            this.labelLocationY.Name = "labelLocationY";
            this.labelLocationY.Size = new System.Drawing.Size(166, 23);
            this.labelLocationY.TabIndex = 7;
            this.labelLocationY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelSpeedY
            // 
            this.labelSpeedY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSpeedY.Location = new System.Drawing.Point(175, 46);
            this.labelSpeedY.Name = "labelSpeedY";
            this.labelSpeedY.Size = new System.Drawing.Size(166, 23);
            this.labelSpeedY.TabIndex = 3;
            this.labelSpeedY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelSpeedFull
            // 
            this.labelSpeedFull.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel.SetColumnSpan(this.labelSpeedFull, 2);
            this.labelSpeedFull.Location = new System.Drawing.Point(3, 23);
            this.labelSpeedFull.Name = "labelSpeedFull";
            this.labelSpeedFull.Size = new System.Drawing.Size(338, 23);
            this.labelSpeedFull.TabIndex = 1;
            this.labelSpeedFull.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelSpeedTitle
            // 
            this.labelSpeedTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel.SetColumnSpan(this.labelSpeedTitle, 2);
            this.labelSpeedTitle.Location = new System.Drawing.Point(3, 0);
            this.labelSpeedTitle.Name = "labelSpeedTitle";
            this.labelSpeedTitle.Size = new System.Drawing.Size(338, 23);
            this.labelSpeedTitle.TabIndex = 0;
            this.labelSpeedTitle.Text = "Скорость";
            this.labelSpeedTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelSpeedX
            // 
            this.labelSpeedX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSpeedX.Location = new System.Drawing.Point(3, 46);
            this.labelSpeedX.Name = "labelSpeedX";
            this.labelSpeedX.Size = new System.Drawing.Size(166, 23);
            this.labelSpeedX.TabIndex = 5;
            this.labelSpeedX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelLocationTitle
            // 
            this.labelLocationTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel.SetColumnSpan(this.labelLocationTitle, 2);
            this.labelLocationTitle.Location = new System.Drawing.Point(3, 69);
            this.labelLocationTitle.Name = "labelLocationTitle";
            this.labelLocationTitle.Size = new System.Drawing.Size(338, 23);
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
            this.Size = new System.Drawing.Size(344, 119);
            this.tableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label labelLocationX;
        private System.Windows.Forms.Label labelLocationY;
        private System.Windows.Forms.Label labelSpeedY;
        private System.Windows.Forms.Label labelSpeedFull;
        private System.Windows.Forms.Label labelSpeedTitle;
        private System.Windows.Forms.Label labelSpeedX;
        private System.Windows.Forms.Label labelLocationTitle;


    }
}
