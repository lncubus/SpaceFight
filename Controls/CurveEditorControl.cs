using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using SF.Space;

namespace SF.Controls
{
    public class CurveEditorControl : SpaceGridControl
    {
        private const int Quality = 10;
        private readonly IList<Trajectory> m_trajectories = new List<Trajectory>();
        public static readonly Pen[] Pencils = new[] {Pens.Blue, Pens.Crimson, Pens.ForestGreen, Pens.MediumVioletRed, Pens.SaddleBrown, Pens.Teal, Pens.YellowGreen, Pens.Aqua };

        protected class Trajectory : List<Vector>
        {
            public bool Finished = false;
        }

        public Vector[][] Trajectories
        {
            get
            {
                return m_trajectories.Where(trajectory => trajectory.Finished && trajectory.Count > 0).Select(trajectory => trajectory.ToArray()).ToArray();
            }
            set
            {
                m_trajectories.Clear();
                foreach (var vectors in value)
                {
                    var trajectory = new Trajectory
                    {
                        Finished = true,
                    };
                    trajectory.AddRange(vectors);
                    m_trajectories.Add(trajectory);
                }
                Invalidate();
            }
        }

        public CurveEditorControl()
        {
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
                if (m_trajectories.Count == 0 || m_trajectories.Last().Finished)
                {
                    m_trajectories.Add(new Trajectory());
                    m_trajectories.Last().Add(OwnShip == null ? Vector.Zero : OwnShip.Position);
                }
                var trajectory = m_trajectories.Last();
                trajectory.Add(DeviceToWorld(CreateGraphics(), e.Location));
            }
            else if (e.Button == MouseButtons.Right && m_trajectories.Count > 0)
            {
                var trajectory = m_trajectories.Last();
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
                        m_trajectories.RemoveAt(m_trajectories.Count - 1);
                    }
                }
            }
            Invalidate();
        }

        protected void WorldDraw(Graphics g, Pen pencil, Trajectory t)
        {
            Curve c = new Curve();
            c.AddRange(CRSpline.Create(t).Interpolate(t.Count * Quality));
            c.Pencil = pencil;
            DrawCurve(g, c);
        }

        protected override void DrawContents(PaintEventArgs e)
        {
            base.DrawContents(e);
            for (int i = 0; i < m_trajectories.Count; i++)
                WorldDraw(e.Graphics, m_trajectories[i].Finished ? Pencils[i%Pencils.Length] : Pens.Black, m_trajectories[i]);
            foreach(int i in new[] { 1, 2, 5 })
                WorldDrawCircle(e.Graphics, BlackPen, Vector.Zero, WorldScale*i);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && m_trajectories.Count > 0)
            {
                m_trajectories.Last().Finished = true;
            }
            Invalidate();
        }
    }
}
