using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using SF.Space;

namespace SF.Controls
{
    public class NavigationControl : SpaceGridControl
    {
        private int Modulo(int n, int M)
        {
            n = n % M;
            if (n < 0)
                n += M;
            return n;
        }

        private int DpiX, DpiY;
        private int margin;
        private Region compass;
        private Region roller;
        private Rectangle plusButton, minusButton, scaleLabel;
        private Point[] scaleRuler;

        private string unit = "км";
        public string Unit
        {
            get
            {
                return unit;
            }
            set
            {
                unit = value;
                Invalidate();
            }
        }

        public const double DefaultMinScaleValue = 1000;
        public const double DefaultMaxScaleValue = 5000000000;
        public double MinScaleValue = DefaultMinScaleValue;
        public double MaxScaleValue = DefaultMaxScaleValue;

        private int BandRadius(float i)
        {
            return (int) (m_size/2.0 - margin*i);
        }

        private Rectangle Band(int radius)
        {
            return new Rectangle
            {
                X = m_center.X - radius,
                Y = m_center.Y - radius,
                Width = 2*radius,
                Height = 2*radius,
            };
        }

        private Rectangle BandN(float i)
        {
            return Band(BandRadius(i));
        }
        
        private void Calculate()
        {
            margin = m_size / 24;
            scaleLabel = new Rectangle
            {
                X = ClientRectangle.Right - 3*margin - DpiX,
                Y = margin,
                Width = DpiX + margin,
                Height = margin,
            };
            minusButton = new Rectangle
            {
                X = ClientRectangle.Right - 2*margin,
                Y = margin,
                Width = margin,
                Height = margin,
            };
            plusButton = new Rectangle
            {
                X = ClientRectangle.Right - 4*margin - DpiX,
                Y = margin,
                Width = margin,
                Height = margin,
            };
            var x0 = ClientRectangle.Right - margin*5/2 - DpiX;
            var x1 = ClientRectangle.Right - margin*5/2;
            scaleRuler = new []
            {
                new Point(x0, 3*margin/2),
                new Point(x0, 2*margin),
                new Point(x1, 2*margin),
                new Point(x1, 3*margin/2),
            };
            var path = new GraphicsPath();
            path.AddEllipse(BandN(1));
            path.AddEllipse(BandN(3));
            compass = new Region(path);
            path = new GraphicsPath();
            path.AddEllipse(BandN(5));
            path.AddEllipse(BandN(7));
            roller = new Region(path);
        }

        protected override void DrawContents(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            DpiX = (int)e.Graphics.DpiX;
            DpiY = (int)e.Graphics.DpiY;
            Calculate();
            e.Graphics.FillRegion(Palette.ControlPaper, compass);
            e.Graphics.FillRegion(Palette.ControlPaper, roller);
            e.Graphics.FillRectangle(Palette.ControlPaper, plusButton);
            e.Graphics.FillRectangle(Palette.ControlPaper, minusButton);
            base.DrawContents(e);
            DrawCompassFace(e);
            DrawRolloverFace(e);
            DrawScaleControl(e);
            var heading = (Universe == null || Universe.Ship == null) ? 1.0 : Universe.Ship.Heading;
            var headingTo = (Universe == null || Universe.Ship == null) ? 1.1 : Universe.Ship.HeadingTo;
            var speed = (Universe == null || Universe.Ship == null) ? Vector.Direction(1.3) : Universe.Ship.Speed;
            var roll = (Universe == null || Universe.Ship == null) ? 2.7 : Universe.Ship.Roll;
            var rollTo = (Universe == null || Universe.Ship == null) ? 2.9 : Universe.Ship.RollTo;
            DrawArrow(e.Graphics, Palette.NavyPen, BandRadius(0.8f), BandRadius(3.2f), heading, Math.PI / 18);
            DrawArrow(e.Graphics, Palette.SignalPen, BandRadius(3.2f), BandRadius(0.8f), headingTo, Math.PI / 18);
            if (speed.Length > MathUtils.Epsilon)
                DrawDiamond(e.Graphics, Palette.SecondPen, BandRadius(3.2f), BandRadius(0.8f), speed.Argument, Math.PI/18);
            DrawArrow(e.Graphics, Palette.NavyPen, BandRadius(4.8f), BandRadius(7.2f), roll, Math.PI / 16);
            DrawArrow(e.Graphics, Palette.SignalPen, BandRadius(7.2f), BandRadius(4.8f), rollTo, Math.PI / 16);
            if (Universe == null || Universe.Ship == null)
                return;
            //int h = MathUtils.ToDegreesInt(Universe.Ship.Heading);
            //int hTo = MathUtils.ToDegreesInt(Universe.Ship.HeadingTo);
            //var arrowHead = new[] { GetXY(m_center, r4, h), GetXY(m_center, r1, h), GetXY(m_center, r4, h) };
            //var arrowHeadTo = new[] { GetXY(m_center, r1, hTo), GetXY(m_center, r4, hTo), GetXY(m_center, r1, hTo) };
            //            e.Graphics.DrawLine(Palette.NavyPen, GetXY(r1, h), GetXY(r4, h));
            //            e.Graphics.DrawLine(Palette.SignalPen, GetXY(r1, hTo), GetXY(r4, hTo));
            //e.Graphics.DrawLines(Palette.NavyPen, arrowHead);
            //e.Graphics.DrawLines(Palette.SignalPen, arrowHeadTo);
            //            int h = MathUtils.ToDegreesInt(Universe.Ship.Heading);
            //            int hTo = MathUtils.ToDegreesInt(Universe.Ship.HeadingTo);
            ////            Palette.NavyPen
            //            var arrow = new GraphicsPath();
            //            arrow.AddArc(smallField, h - 5, h + 5);
            //            arrow.AddLines(new[] { GetXY(r1, h - 5), GetXY(r4, h), GetXY(r1, h + 5)});
            //            e.Graphics.FillPath(Palette.NavyBrush, arrow);
            //            e.Graphics.DrawLine(Palette.NavyPen, GetXY(r1, h), GetXY(r4, h));
            //            e.Graphics.DrawLine(Palette.SignalPen, GetXY(r1, hTo), GetXY(r4, hTo));
            //e.Graphics.DrawString("N", Font, Palette.BlackInk, GetXY(smallRadius, 0), CenteredLayout);
            //e.Graphics.DrawString("W", Font, Palette.BlackInk, GetXY(smallRadius, -90), CenteredLayout);
            //e.Graphics.DrawString("S", Font, Palette.BlackInk, GetXY(smallRadius, 180), CenteredLayout);
            //e.Graphics.DrawString("E", Font, Palette.BlackInk, GetXY(smallRadius, 90), CenteredLayout);

            //            DrawArrow(e.Graphics, Palette.SupportPen, m_bigRadius, m_bigRadius, Secondary, 174);
            //            DrawArrow(e.Graphics, Palette.NavyPen, m_bigRadius, m_bigRadius, Heading, 170);
            //            DrawArrow(e.Graphics, Palette.SignalPen, m_bigRadius, m_halfSize, HeadingTo, 10);
        }

        public void DrawArrow(Graphics g, Pen pen, int headRadius, int tailRadius, double head, double sweep)
        {
            var left = GetXY(m_center, tailRadius, head - sweep/2);
            var right = GetXY(m_center, tailRadius, head + sweep/2);
            var point = GetXY(m_center, headRadius, head);
            var tail = GetXY(m_center, tailRadius, head);
            var tailRect = Band(tailRadius);
            g.DrawLines(pen, new[] { left, point, right });
            g.DrawLine(pen, tail, point);
            g.DrawArc(pen, tailRect, (float)MathUtils.ToDegrees(head - sweep/2 - Math.PI / 2), (float)MathUtils.ToDegrees(sweep));
        }

        public void DrawDiamond(Graphics g, Pen pen, int headRadius, int tailRadius, double head, double sweep)
        {
            var r = (headRadius + tailRadius)/2;
            var left = GetXY(m_center, r, head - sweep / 2);
            var right = GetXY(m_center, r, head + sweep / 2);
            var point = GetXY(m_center, headRadius, head);
            var tail = GetXY(m_center, tailRadius, head);
            g.DrawPolygon(pen, new[] { left, point, right, tail });
            g.DrawLine(pen, tail, point);
            //g.DrawArc(pen, tailRect, (float)MathUtils.ToDegrees(head - sweep / 2 - Math.PI / 2), (float)MathUtils.ToDegrees(sweep));
        }

        private void DrawScaleControl(PaintEventArgs e)
        {
            var scale = MathUtils.NumberToText(WorldScale, unit);
            e.Graphics.DrawRectangle(Palette.BlackPen, plusButton);
            e.Graphics.DrawRectangle(Palette.BlackPen, minusButton);
            e.Graphics.DrawString("+", Font, Palette.BlackInk, plusButton, CenteredLayout);
            e.Graphics.DrawString("-", Font, Palette.BlackInk, minusButton, CenteredLayout);
            e.Graphics.DrawString(scale, Font, Palette.BlackInk, scaleLabel, CenteredLayout);
            e.Graphics.DrawLines(Palette.BlackPen, scaleRuler);
        }

        private void DrawRolloverFace(PaintEventArgs e)
        {
            var r = BandRadius(6);
            e.Graphics.DrawEllipse(Palette.BlackPen, BandN(5));
            e.Graphics.DrawEllipse(Palette.BlackPen, BandN(7));
            e.Graphics.DrawString("0", Font, Palette.BlackInk, GetXY(m_center, r, 0), CenteredLayout);
            e.Graphics.DrawString("-90", Font, Palette.BlackInk, GetXY(m_center, r, -Math.PI / 2), CenteredLayout);
            e.Graphics.DrawString("180", Font, Palette.BlackInk, GetXY(m_center, r, Math.PI), CenteredLayout);
            e.Graphics.DrawString("90", Font, Palette.BlackInk, GetXY(m_center, r, Math.PI / 2), CenteredLayout);
        }

        private void DrawCompassFace(PaintEventArgs e)
        {
            e.Graphics.DrawEllipse(Palette.BlackPen, BandN(1));
            e.Graphics.DrawEllipse(Palette.BlackPen, BandN(3));
            const int N = 12;
            var r = BandRadius(2);
            for (int i = 1; i <= N; i++)
            {
                var p = GetXY(m_center, r, 2*i*Math.PI/N);
                e.Graphics.DrawString((i*30).ToString(), Font, Palette.BlackInk, p, CenteredLayout);
            }
            var r1 = BandRadius(1);
            var r2 = BandRadius(1.5f);
            var r3 = BandRadius(2.5f);
            var r4 = BandRadius(3);
            for (int i = 0; i < N*2; i++)
            {
                var pen = Palette.BlackPencil;
                var a = i*Math.PI/N;
                if (i%2 == 1)
                    e.Graphics.DrawLine(pen, GetXY(m_center, r1, a), GetXY(m_center, r4, a));
                else
                {
                    e.Graphics.DrawLine(pen, GetXY(m_center, r1, a), GetXY(m_center, r2, a));
                    e.Graphics.DrawLine(pen, GetXY(m_center, r3, a), GetXY(m_center, r4, a));
                }
            }
        }

        protected override void MouseHit(Point point, double alpha, MouseEventType type)
        {
            base.MouseHit(point, alpha, type);
            if (plusButton.Contains(point) && type == MouseEventType.MouseUp)
                ZoomIn();
            else if (minusButton.Contains(point) && type == MouseEventType.MouseUp)
                ZoomOut();
            else if (compass.IsVisible(point))
                CompassHit(alpha);
            else if (roller.IsVisible(point))
                RollHit(alpha);
        }

        private void CompassHit(double alpha)
        {
            //throw new NotImplementedException();
        }

        private void RollHit(double alpha)
        {
            //throw new NotImplementedException();
        }

        private void ZoomIn()
        {
            if (WorldScale <= MinScaleValue)
            {
                WorldScale = MinScaleValue;
                return;
            }
            if (WorldScale > MaxScaleValue)
            {
                WorldScale = MaxScaleValue;
                return;
            }
            int zeros = (int)Math.Truncate(Math.Log10(WorldScale));
            double exponent = Math.Pow(10, zeros);
            double first = WorldScale / exponent;
            if (first < 1)
                WorldScale = MinScaleValue;
            else if (first < 1.5)
                WorldScale = 0.5 * exponent;
            else if (first < 3.5)
                WorldScale = exponent;
            else if (first < 6.5)
                WorldScale = 2 * exponent;
            else
                WorldScale = 5 * exponent;
        }

        private void ZoomOut()
        {
            if (WorldScale < MinScaleValue)
            {
                WorldScale = MinScaleValue;
                return;
            }
            if (WorldScale >= MaxScaleValue)
            {
                WorldScale = MaxScaleValue;
                return;
            }
            int zeros = (int)Math.Truncate(Math.Log10(WorldScale));
            double exponent = Math.Pow(10, zeros);
            double first = WorldScale / exponent;
            if (first < 1)
                WorldScale = MinScaleValue;
            else if (first < 1.5)
                WorldScale = 2 * exponent;
            else if (first < 3.5)
                WorldScale = 5 * exponent;
            else if (first < 6.5)
                WorldScale = 10 * exponent;
            else
                WorldScale = 20 * exponent;
        }
    }
}
