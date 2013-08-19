using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SF.Controls
{
    public partial class CompassControl : RoundControl
    {
        private int m_heading;
        private int m_headingTo;
        private int m_secondary;

        public event EventHandler OnHeadingChanged;
        public event EventHandler OnHeadingToChanged;
        public event EventHandler OnSecondaryChanged;

        public Pen SupportPen { get; set; }

        public int Heading
        {
            get { return m_heading; }
            set { ChangeValue(ref m_heading, Modulo(value, 360), OnHeadingChanged); }
        }

        public int HeadingTo
		{
			get { return m_headingTo; }
			set { ChangeValue(ref m_headingTo, Modulo(value, 360), OnHeadingToChanged); }
		}

        public int Secondary
        {
            get { return m_secondary; }
            set { ChangeValue(ref m_secondary, value, OnSecondaryChanged); }
        }

        public CompassControl()
        {
            InitializeComponent();
            SupportPen = Pens.MediumSeaGreen;
        }

        private int Gold(int n)
        {
            return n * 618 / 1000;
        }

        private int Modulo(int n, int M)
        {
            n = n % M;
            if (n < 0)
                n += M;
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
            HeadingTo = degrees;
        }

        protected override void DrawContents(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Calculate();
            for (int i = 0; i < 360; i += 5)
            {
                var pen = i % 30 == 0 || i % 45 == 0 ? BlackPen : BlackPencil;
                var p = GetXY(m_halfSize - m_bandWidth / 3, i);
                var q = GetXY(m_halfSize, i);
                e.Graphics.DrawLine(pen, p, q);
            }
            const int N = 12;
            for (int i = 1; i <= N; i++)
            {
                var p = GetXY(m_bigRadius, 360 * i / N);
                e.Graphics.DrawString(i.ToString(), Font, BlackInk, p, CenteredLayout);
            }
            e.Graphics.DrawString("N", Font, BlackInk, GetXY(m_smallRadius, 0), CenteredLayout);
            e.Graphics.DrawString("W", Font, BlackInk, GetXY(m_smallRadius, -90), CenteredLayout);
            e.Graphics.DrawString("S", Font, BlackInk, GetXY(m_smallRadius, 180), CenteredLayout);
            e.Graphics.DrawString("E", Font, BlackInk, GetXY(m_smallRadius, 90), CenteredLayout);
            DrawArrow(e.Graphics, SupportPen, m_bigRadius, m_bigRadius, Secondary, 174);
            DrawArrow(e.Graphics, NavyPen, m_bigRadius, m_bigRadius, Heading, 170);
            DrawArrow(e.Graphics, SignalPen, m_bigRadius, m_halfSize, HeadingTo, 10);
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
