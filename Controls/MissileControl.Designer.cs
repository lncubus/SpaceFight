namespace SF.Controls
{
    partial class MissileControl
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
            this.dataGridViewMissiles = new System.Windows.Forms.DataGridView();
            this.columnSelected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.columnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMissiles)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewMissiles
            // 
            this.dataGridViewMissiles.AllowUserToAddRows = false;
            this.dataGridViewMissiles.AllowUserToDeleteRows = false;
            this.dataGridViewMissiles.AllowUserToResizeColumns = false;
            this.dataGridViewMissiles.AllowUserToResizeRows = false;
            this.dataGridViewMissiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMissiles.ColumnHeadersVisible = false;
            this.dataGridViewMissiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnSelected,
            this.columnName});
            this.dataGridViewMissiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewMissiles.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dataGridViewMissiles.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewMissiles.Name = "dataGridViewMissiles";
            this.dataGridViewMissiles.RowHeadersVisible = false;
            this.dataGridViewMissiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewMissiles.ShowEditingIcon = false;
            this.dataGridViewMissiles.Size = new System.Drawing.Size(150, 150);
            this.dataGridViewMissiles.TabIndex = 0;
            // 
            // columnSelected
            // 
            this.columnSelected.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.columnSelected.HeaderText = "Selected";
            this.columnSelected.Name = "columnSelected";
            this.columnSelected.Width = 5;
            // 
            // columnName
            // 
            this.columnName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.columnName.HeaderText = "Name";
            this.columnName.Name = "columnName";
            this.columnName.ReadOnly = true;
            // 
            // MissileControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridViewMissiles);
            this.Name = "MissileControl";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMissiles)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewMissiles;
        private System.Windows.Forms.DataGridViewCheckBoxColumn columnSelected;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnName;

    }
}
