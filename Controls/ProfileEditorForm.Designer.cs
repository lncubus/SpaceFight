﻿namespace SF.Controls
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
            this.curveEditorControl1 = new SF.Controls.CurveEditorControl();
            this.SuspendLayout();
            // 
            // panelButtons
            // 
            this.panelButtons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 531);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(792, 42);
            this.panelButtons.TabIndex = 1;
            // 
            // curveEditorControl1
            // 
            stringFormat1.Alignment = System.Drawing.StringAlignment.Center;
            stringFormat1.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
            stringFormat1.LineAlignment = System.Drawing.StringAlignment.Center;
            stringFormat1.Trimming = System.Drawing.StringTrimming.None;
            this.curveEditorControl1.CenteredLayout = stringFormat1;
            this.curveEditorControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.curveEditorControl1.Location = new System.Drawing.Point(0, 0);
            this.curveEditorControl1.Missiles = null;
            this.curveEditorControl1.Name = "curveEditorControl1";
            this.curveEditorControl1.Options = SF.Controls.SpaceGridControl.DrawingOptions.None;
            this.curveEditorControl1.Polar = false;
            this.curveEditorControl1.ReadOnly = false;
            this.curveEditorControl1.Rotation = 0D;
            this.curveEditorControl1.Selectable = SF.Controls.SpaceGridControl.SelectableObjects.None;
            this.curveEditorControl1.Selected = null;
            this.curveEditorControl1.Ships = null;
            this.curveEditorControl1.Size = new System.Drawing.Size(792, 531);
            this.curveEditorControl1.Stars = null;
            this.curveEditorControl1.StaticGrid = true;
            this.curveEditorControl1.TabIndex = 3;
            this.curveEditorControl1.WorldScale = 1000000D;
            // 
            // ProfileEditorForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.MintCream;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.curveEditorControl1);
            this.Controls.Add(this.panelButtons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.KeyPreview = true;
            this.Name = "ProfileEditorForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelButtons;
        private CurveEditorControl curveEditorControl1;

    }
}