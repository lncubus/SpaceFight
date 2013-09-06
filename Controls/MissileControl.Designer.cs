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
            this.labelAccumulator = new System.Windows.Forms.Label();
            this.progressBarAccumulator = new System.Windows.Forms.ProgressBar();
            this.labelMissiles = new System.Windows.Forms.Label();
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
            this.dataGridViewMissiles.MultiSelect = false;
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
            // labelAccumulator
            // 
            this.labelAccumulator.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelAccumulator.Location = new System.Drawing.Point(0, 0);
            this.labelAccumulator.Name = "labelAccumulator";
            this.labelAccumulator.Size = new System.Drawing.Size(150, 23);
            this.labelAccumulator.TabIndex = 3;
            this.labelAccumulator.Text = "Пусковая батарея";
            this.labelAccumulator.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBarAccumulator
            // 
            this.progressBarAccumulator.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressBarAccumulator.ForeColor = System.Drawing.Color.Gray;
            this.progressBarAccumulator.Location = new System.Drawing.Point(0, 23);
            this.progressBarAccumulator.Name = "progressBarAccumulator";
            this.progressBarAccumulator.Size = new System.Drawing.Size(150, 23);
            this.progressBarAccumulator.TabIndex = 4;
            this.progressBarAccumulator.Value = 100;
            // 
            // labelMissiles
            // 
            this.labelMissiles.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelMissiles.Location = new System.Drawing.Point(0, 46);
            this.labelMissiles.Name = "labelMissiles";
            this.labelMissiles.Size = new System.Drawing.Size(150, 23);
            this.labelMissiles.TabIndex = 5;
            this.labelMissiles.Text = "Ракетные установки";
            this.labelMissiles.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MissileControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelMissiles);
            this.Controls.Add(this.progressBarAccumulator);
            this.Controls.Add(this.labelAccumulator);
            this.Controls.Add(this.dataGridViewMissiles);
            this.Name = "MissileControl";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMissiles)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewMissiles;
        private System.Windows.Forms.DataGridViewCheckBoxColumn columnSelected;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnName;
        private System.Windows.Forms.Label labelAccumulator;
        private System.Windows.Forms.ProgressBar progressBarAccumulator;
        private System.Windows.Forms.Label labelMissiles;

    }
}
