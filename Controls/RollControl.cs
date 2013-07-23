using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SF.Controls
{
    public partial class RollControl : RoundControl
    {
        public const double Granularity = 45;
        private int m_roll;
        private int m_rollTo;

        public event EventHandler OnRollChanged;
        public event EventHandler OnRollToChanged;

        public int Roll
        {
            get { return m_roll; }
            set { ChangeValue(ref m_roll, Modulo(value, 360), OnRollChanged); }
        }

        public int RollTo
        {
            get { return m_rollTo; }
            set
            {
                value = (int)(Granularity * Math.Round(value / Granularity));
                ChangeValue(ref m_rollTo, Modulo(value, 360), OnRollToChanged);
            }
        }

        public RollControl()
        {
            InitializeComponent();
        }

        private int Gold(int n)
        {
            return n * 618 / 1000;
        }

        private int Modulo(int n, int M)
        {
            n = n % M;
            if (n <= -M / 2)
                n += M;
            if (n > M / 2)
                n -= M;
            return n;
        }

        private int m_halfSize;
        private int m_bigRadius;
        private int m_smallRadius;
        private int m_bandWidth;
        private int m_halfBandWidth;
        private Rectangle m_bigField;
        private Rectangle m_smallField;

        protected override void MouseHit(Point point, int degrees)
        {
            RollTo = degrees;
        }

        protected override void DrawContents(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Calculate();
            e.Graphics.DrawString("0", Font, BlackInk, GetXY(m_bigRadius, 0), CenteredLayout);
            e.Graphics.DrawString("-90", Font, BlackInk, GetXY(m_bigRadius, -90), CenteredLayout);
            e.Graphics.DrawString("180", Font, BlackInk, GetXY(m_bigRadius, 180), CenteredLayout);
            e.Graphics.DrawString("90", Font, BlackInk, GetXY(m_bigRadius, 90), CenteredLayout);
            DrawArrow(e.Graphics, NavyPen, m_bigRadius, m_smallRadius, Roll, 100);
            DrawArrow(e.Graphics, SignalPen, m_bigRadius, m_halfSize, RollTo, 10);
        }

        public void DrawArrow(Graphics g, Pen pen, int headRadius, int tailRadius, int head, int sweep)
        {
            var left = GetXY(tailRadius, head - sweep);
            var right = GetXY(tailRadius, head + sweep);
            var point = GetXY(headRadius, head);
            var middle = new Point
            {
                X = (left.X + right.X) / 2,
                Y = (left.Y + right.Y) / 2
            };
            g.DrawPolygon(pen, new Point[] { left, point, right });
            g.DrawLine(pen, middle, point);
        }

        protected override void DrawBackgroound(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Calculate();
            e.Graphics.FillEllipse(WhitePaper, m_bigField);
            e.Graphics.DrawEllipse(BlackPencil, m_bigField);
            e.Graphics.DrawEllipse(BlackPencil, m_smallField);
        }

        private void Calculate()
        {
            m_halfSize = m_size / 2;
            m_bigField = new Rectangle
            {
                X = ClientRectangle.X,
                Y = ClientRectangle.Y,
                Width = m_size,
                Height = m_size,
            };
            m_bandWidth = (m_size - Gold(m_size)) / 2;
            m_halfBandWidth = m_bandWidth / 2;
            m_smallField = m_bigField;
            m_smallField.Inflate(-m_bandWidth, -m_bandWidth);
            m_bigRadius = m_size / 2 - m_halfBandWidth;
            m_smallRadius = (m_size - 3 * m_bandWidth) / 2;
        }
    }
}
