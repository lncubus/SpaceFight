namespace SF.Controls
{
    partial class ProfileEditorForm
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
            System.Drawing.StringFormat stringFormat1 = new System.Drawing.StringFormat();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonOkay = new System.Windows.Forms.Button();
            this.textBoxHint = new System.Windows.Forms.TextBox();
            this.curveEditorControl = new SF.Controls.CurveEditorControl();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelButtons
            // 
            this.panelButtons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelButtons.Controls.Add(this.buttonOkay);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 537);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(792, 36);
            this.panelButtons.TabIndex = 1;
            // 
            // buttonOkay
            // 
            this.buttonOkay.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOkay.Location = new System.Drawing.Point(12, 6);
            this.buttonOkay.Name = "buttonOkay";
            this.buttonOkay.Size = new System.Drawing.Size(75, 23);
            this.buttonOkay.TabIndex = 0;
            this.buttonOkay.Text = "Применить";
            this.buttonOkay.UseVisualStyleBackColor = true;
            // 
            // textBoxHint
            // 
            this.textBoxHint.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxHint.Location = new System.Drawing.Point(0, 0);
            this.textBoxHint.Name = "textBoxHint";
            this.textBoxHint.ReadOnly = true;
            this.textBoxHint.Size = new System.Drawing.Size(792, 20);
            this.textBoxHint.TabIndex = 2;
            this.textBoxHint.Text = "Левая кнопка мыши - добавить точку. Правая кнопка мыши - удалить точку. Двойной к" +
                "лик - завершить кривую.";
            // 
            // curveEditorControl
            // 
            stringFormat1.Alignment = System.Drawing.StringAlignment.Center;
            stringFormat1.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
            stringFormat1.LineAlignment = System.Drawing.StringAlignment.Center;
            stringFormat1.Trimming = System.Drawing.StringTrimming.None;
            this.curveEditorControl.CenteredLayout = stringFormat1;
            this.curveEditorControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.curveEditorControl.Location = new System.Drawing.Point(0, 20);
            this.curveEditorControl.Missiles = null;
            this.curveEditorControl.Name = "curveEditorControl";
            this.curveEditorControl.Options = SF.Controls.SpaceGridControl.DrawingOptions.None;
            this.curveEditorControl.Polar = true;
            this.curveEditorControl.ReadOnly = false;
            this.curveEditorControl.Rotation = 0D;
            this.curveEditorControl.Selectable = SF.Controls.SpaceGridControl.SelectableObjects.Missiles;
            this.curveEditorControl.Selected = null;
            this.curveEditorControl.Ships = null;
            this.curveEditorControl.Size = new System.Drawing.Size(792, 517);
            this.curveEditorControl.Stars = null;
            this.curveEditorControl.StaticGrid = true;
            this.curveEditorControl.TabIndex = 5;
            this.curveEditorControl.WorldScale = 1000000D;
            this.curveEditorControl.Resize += new System.EventHandler(this.curveEditorControl_Resize);
            // 
            // ProfileEditorForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.MintCream;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.curveEditorControl);
            this.Controls.Add(this.textBoxHint);
            this.Controls.Add(this.panelButtons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.KeyPreview = true;
            this.Name = "ProfileEditorForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.TextBox textBoxHint;
        private System.Windows.Forms.Button buttonOkay;
        private CurveEditorControl curveEditorControl;

    }
}