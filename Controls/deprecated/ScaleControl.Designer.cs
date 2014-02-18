namespace SF.Controls
{
    partial class ScaleControl
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
            labelScale = new System.Windows.Forms.Label();
            buttonZoomOut = new System.Windows.Forms.Button();
            buttonZoomIn = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // labelScale
            // 
            labelScale.Dock = System.Windows.Forms.DockStyle.Fill;
            labelScale.Location = new System.Drawing.Point(0, 0);
            labelScale.Name = "labelScale";
            labelScale.Size = new System.Drawing.Size(191, 29);
            labelScale.TabIndex = 5;
            labelScale.Text = "0 км";
            labelScale.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonZoomOut
            // 
            buttonZoomOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            buttonZoomOut.BackColor = System.Drawing.Color.White;
            buttonZoomOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonZoomOut.Location = new System.Drawing.Point(165, 3);
            buttonZoomOut.Name = "buttonZoomOut";
            buttonZoomOut.Size = new System.Drawing.Size(23, 23);
            buttonZoomOut.TabIndex = 4;
            buttonZoomOut.TabStop = false;
            buttonZoomOut.Text = "-";
            buttonZoomOut.UseVisualStyleBackColor = false;
            buttonZoomOut.Click += new System.EventHandler(buttonZoomOut_Click);
            // 
            // buttonZoomIn
            // 
            buttonZoomIn.BackColor = System.Drawing.Color.White;
            buttonZoomIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            buttonZoomIn.Location = new System.Drawing.Point(3, 3);
            buttonZoomIn.Name = "buttonZoomIn";
            buttonZoomIn.Size = new System.Drawing.Size(23, 23);
            buttonZoomIn.TabIndex = 3;
            buttonZoomIn.TabStop = false;
            buttonZoomIn.Text = "+";
            buttonZoomIn.UseVisualStyleBackColor = false;
            buttonZoomIn.Click += new System.EventHandler(buttonZoomIn_Click);
            // 
            // ScaleControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            Controls.Add(buttonZoomOut);
            Controls.Add(buttonZoomIn);
            Controls.Add(labelScale);
            Name = "ScaleControl";
            Size = new System.Drawing.Size(191, 29);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelScale;
        private System.Windows.Forms.Button buttonZoomOut;
        private System.Windows.Forms.Button buttonZoomIn;

    }
}
