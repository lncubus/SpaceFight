namespace Server
{
    partial class ServerForm
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
            statusStrip = new System.Windows.Forms.StatusStrip();
            toolStrip = new System.Windows.Forms.ToolStrip();
            splitContainer = new System.Windows.Forms.SplitContainer();
            treeView = new System.Windows.Forms.TreeView();
            propertyGrid = new System.Windows.Forms.PropertyGrid();
            toolStripButtonBang = new System.Windows.Forms.ToolStripButton();
            toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(splitContainer)).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip
            // 
            statusStrip.Location = new System.Drawing.Point(0, 382);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new System.Drawing.Size(511, 22);
            statusStrip.TabIndex = 2;
            statusStrip.Text = "statusStrip1";
            // 
            // toolStrip
            // 
            toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            toolStripButtonBang});
            toolStrip.Location = new System.Drawing.Point(0, 0);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new System.Drawing.Size(511, 25);
            toolStrip.TabIndex = 3;
            toolStrip.Text = "toolStrip1";
            // 
            // splitContainer
            // 
            splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer.Location = new System.Drawing.Point(0, 25);
            splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(treeView);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(propertyGrid);
            splitContainer.Size = new System.Drawing.Size(511, 357);
            splitContainer.SplitterDistance = 250;
            splitContainer.TabIndex = 4;
            // 
            // treeView
            // 
            treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            treeView.HideSelection = false;
            treeView.Location = new System.Drawing.Point(0, 0);
            treeView.Name = "treeView";
            treeView.Size = new System.Drawing.Size(250, 357);
            treeView.TabIndex = 1;
            treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(treeView_AfterSelect);
            // 
            // propertyGrid
            // 
            propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            propertyGrid.HelpVisible = false;
            propertyGrid.Location = new System.Drawing.Point(0, 0);
            propertyGrid.Name = "propertyGrid";
            propertyGrid.Size = new System.Drawing.Size(257, 357);
            propertyGrid.TabIndex = 0;
            // 
            // toolStripButtonBang
            // 
            toolStripButtonBang.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButtonBang.Name = "toolStripButtonBang";
            toolStripButtonBang.Size = new System.Drawing.Size(39, 22);
            toolStripButtonBang.Text = "Bang!";
            toolStripButtonBang.Click += new System.EventHandler(toolStripButtonBang_Click);
            // 
            // ServerForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(511, 404);
            Controls.Add(splitContainer);
            Controls.Add(toolStrip);
            Controls.Add(statusStrip);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            Name = "ServerForm";
            WindowState = System.Windows.Forms.FormWindowState.Maximized;
            FormClosing += new System.Windows.Forms.FormClosingEventHandler(ServerForm_FormClosing);
            Load += new System.EventHandler(ServerForm_Load);
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(splitContainer)).EndInit();
            splitContainer.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.ToolStripButton toolStripButtonBang;

    }
}

