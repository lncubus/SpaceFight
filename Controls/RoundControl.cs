using System;
using System.Drawing;
using System.Windows.Forms;
using MathUtils = SF.Space.MathUtils;

namespace SF.Controls
{
    public class RoundControl : UserControl
    {
	    public bool ReadOnly { get; set; }
        public StringFormat CenteredLayout { get; set; }

        public RoundControl()
        {
            DoubleBuffered = true;
            Size = new System.Drawing.Size(200, 200);
            CenteredLayout = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
                Trimming = StringTrimming.None,
            };
        }

        protected int m_size
        {
            get { return Math.Min(ClientRectangle.Width, ClientRectangle.Height) - 1; }
        }

        protected Point m_center
        {
            get
            {
                return new Point
                {
                    X = ClientRectangle.Top + ClientRectangle.Width / 2,
                    Y = ClientRectangle.Y + ClientRectangle.Height / 2
                };
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (ReadOnly || e.Button != System.Windows.Forms.MouseButtons.Left)
                return;
            MouseHit(e.Location);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (ReadOnly || e.Button != System.Windows.Forms.MouseButtons.Left)
                return;
            MouseHit(e.Location);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (ReadOnly || e.Button != System.Windows.Forms.MouseButtons.Left)
                return;
            MouseHit(e.Location);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            DrawBackgroound(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawContents(e);
        }

        protected void ChangeValue(ref int member, int value, EventHandler handler)
        {
            bool changed = member != value;
            member = value;
            if (changed && handler != null)
                handler(this, EventArgs.Empty);
            Invalidate();
        }

        protected Point GetXY(int radius, int deg)
        {
            double a = MathUtils.ToRadians(deg);
            return new Point
            {
                X = (int)(m_center.X + Math.Sin(a) * radius),
                Y = (int)(m_center.Y - Math.Cos(a) * radius)
            };
        }

        private void MouseHit(Point point)
        {
            int x = point.X - m_center.X;
            int y = point.Y - m_center.Y;
            if (x == 0 && y == 0)
                return;
            var alpha = Math.Atan2(x, -y);
            var degrees = MathUtils.ToDegreesInt(alpha);
            MouseHit(point, degrees);
        }

        protected virtual void MouseHit(Point point, int degrees)
        {
        }

        protected virtual void DrawContents(PaintEventArgs e)
        {
        }

        protected virtual void DrawBackgroound(PaintEventArgs e)
        {
        }
    }
}
