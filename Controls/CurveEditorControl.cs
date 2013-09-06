using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using SF.Space;
using System.Diagnostics;

namespace SF.Controls
{
    public class CurveEditorControl : SpaceGridControl
    {
        private const int Quality = 10;
        private class Trajectory : List<Vector>
        {
            public bool Finished = false;
        }
        private List<Trajectory> Trajectories = new List<Trajectory>();

        public CurveEditorControl()
        {
            //List<Vector> points = new List<Vector>();
            //points.Add(new Vector(-44390705.378367372, 37187705.396267861));
            //points.Add(new Vector(0, 0));
            //points.Add(new Vector(85311905.349006966, -66566142.576139823));
            //points.Add(new Vector(70992558.707589224, -131680280.61320958));

            //Curve missile = new Curve();
            //missile.AddRange(CRSpline.Create(points).Interpolate(10));
            //missile.Pencil = new Pen(Color.Magenta, 2);

            //Curve missile2 = new Curve();
            //missile2.AddRange(points);
            //missile2.Pencil = new Pen(Color.Green, 2);

            //Curves.Add(missile);
            //Curves.Add(missile2);

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // CurveEditorControl
            // 
            this.Name = "CurveEditorControl";
            this.ResumeLayout(false);
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (Trajectories.Count == 0 || Trajectories.Last().Finished)
                {
                    Trajectories.Add(new Trajectory());
                    Trajectories.Last().Add(OwnShip.Position);
                }

                var trajectory = Trajectories.Last();
                trajectory.Add(DeviceToWorld(CreateGraphics(), e.Location));
            }
            else if (e.Button == MouseButtons.Right && Trajectories.Count > 0)
            {
                var trajectory = Trajectories.Last();
                if (trajectory.Finished)
                {
                    trajectory.Finished = false;
                }
                else
                {
                    if (trajectory.Count > 2)
                    {
                        trajectory.RemoveAt(trajectory.Count - 1);
                    }
                    else
                    {
                        // only 1 point left - delete the whole thing
                        Trajectories.RemoveAt(Trajectories.Count - 1);
                    }
                }
            }
            Invalidate();
        }

        private void Draw(Trajectory t, PaintEventArgs e)
        {
            Curve c = new Curve();
            c.AddRange(CRSpline.Create(t).Interpolate(t.Count * Quality));
            c.Pencil = new Pen(t.Finished ? Brushes.OliveDrab : Brushes.MediumSlateBlue, 1);
            DrawCurve(e.Graphics, c);
        }

        protected override void DrawContents(PaintEventArgs e)
        {
            base.DrawContents(e);

            Trajectories.ForEach(x => Draw(x, e));
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && Trajectories.Count > 0)
            {
                Trajectories.Last().Finished = true;
            }
            Invalidate();
        }

    }
}
