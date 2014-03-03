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
        private int DpiX, DpiY;
        private int margin;
        private Region compass;
        private Region thruster;
        private GraphicsPath thrusterPath;
        private Region roller;
        private Rectangle plusButton, minusButton, scaleLabel;
        private Point[] scaleRuler;

        public const double DefaultMinScaleValue = 1000;
        public const double DefaultMaxScaleValue = 5000000000;
        public double MinScaleValue = DefaultMinScaleValue;
        public double MaxScaleValue = DefaultMaxScaleValue;

        public event EventHandler<ValueEventArgs<double>> HeadingToChanged;
        public event EventHandler<ValueEventArgs<double>> RollToChanged;
        public event EventHandler<ValueEventArgs<double>> ThrustToChanged;

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
            var b1 = BandN(1);
            var b3 = BandN(3);
            var b5 = BandN(5);
            var b7 = BandN(7);
            var path = new GraphicsPath();
            path.AddEllipse(b1);
            path.AddEllipse(b3);
            compass = new Region(path);
            path = new GraphicsPath();
            path.AddEllipse(b5);
            path.AddEllipse(b7);
            roller = new Region(path);
            path = new GraphicsPath();
            path.AddLine(m_center.X, b5.Top, m_center.X, b3.Top);
            path.AddArc(b3, -90, 180);
            var p = new[]
            {
                new Point(m_center.X, b3.Bottom),
                new Point(m_center.X - margin, m_center.Y + BandRadius(4)),
                new Point(m_center.X, b5.Bottom),
            };
            path.AddLines(p);
            path.AddArc(b5, 90, -180);
            thruster = new Region(path);
            thrusterPath = path;
        }

        protected override void DrawContents(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            DpiX = (int)e.Graphics.DpiX;
            DpiY = (int)e.Graphics.DpiY;
            Calculate();
            e.Graphics.FillRegion(Palette.ControlPaper, compass);
            e.Graphics.FillRegion(Palette.ControlPaper, thruster);
            e.Graphics.FillRegion(Palette.ControlPaper, roller);
            e.Graphics.FillRectangle(Palette.ControlPaper, plusButton);
            e.Graphics.FillRectangle(Palette.ControlPaper, minusButton);
            base.DrawContents(e);

            var heading = (Universe == null || Universe.Ship == null) ? 1.0 : Universe.Ship.Heading;
            var headingTo = (Universe == null || Universe.Ship == null) ? 1.1 : Universe.Ship.HeadingTo;
            var speed = (Universe == null || Universe.Ship == null) ? Vector.Direction(1.3) : Universe.Ship.Speed;
            var roll = (Universe == null || Universe.Ship == null) ? 2.7 : Universe.Ship.Roll;
            var rollTo = (Universe == null || Universe.Ship == null) ? 2.9 : Universe.Ship.RollTo;
            var thrust = (Universe == null || Universe.Ship == null) ? 1.9 : Universe.Ship.Thrust;
            var thrustTo = (Universe == null || Universe.Ship == null) ? 2.1 : Universe.Ship.ThrustTo;
            var thrustMax = (Universe == null || Universe.Ship == null) ? 4.0 : Universe.Ship.Class.MaximumAcceleration;
            thrust = Math.PI * (1 - thrust / thrustMax);
            thrustTo = Math.PI * (1 - thrustTo / thrustMax);
            DrawFaces(e, thrustMax);
            DrawArrow(e.Graphics, Palette.NavyPen, BandRadius(2.8f), BandRadius(5.2f), thrust, Math.PI / 18, false);
            DrawArrow(e.Graphics, Palette.SignalPen, BandRadius(5.2f), BandRadius(2.8f), thrustTo, Math.PI / 18, false);
            DrawArrow(e.Graphics, Palette.NavyPen, BandRadius(0.8f), BandRadius(3.2f), heading, Math.PI / 18);
            DrawArrow(e.Graphics, Palette.SignalPen, BandRadius(3.2f), BandRadius(0.8f), headingTo, Math.PI / 18);
            if (speed.Length > MathUtils.Epsilon)
                DrawDiamond(e.Graphics, Palette.SecondPen, BandRadius(3.2f), BandRadius(0.8f), speed.Argument, Math.PI/18);
            DrawArrow(e.Graphics, Palette.NavyPen, BandRadius(4.8f), BandRadius(7.2f), roll, Math.PI / 16);
            DrawArrow(e.Graphics, Palette.SignalPen, BandRadius(7.2f), BandRadius(4.8f), rollTo, Math.PI / 16);
            DrawDiamond(e.Graphics, Palette.LeftPen, BandRadius(6.8f), BandRadius(5.2f), roll - Math.PI/2, Math.PI / 24);
            DrawDiamond(e.Graphics, Palette.RightPen, BandRadius(6.8f), BandRadius(5.2f), roll + Math.PI/2, Math.PI / 24);
        }

        public void DrawArrow(Graphics g, Pen pen, int headRadius, int tailRadius, double head, double sweep, bool opposite = true)
        {
            var left = GetXY(m_center, tailRadius, head - sweep/2);
            var right = GetXY(m_center, tailRadius, head + sweep/2);
            var point = GetXY(m_center, headRadius, head);
            var tail = GetXY(m_center, tailRadius, head);
            var tailRect = Band(tailRadius);
            g.DrawLines(pen, new[] { left, point, right });
            g.DrawLine(pen, tail, point);
            g.DrawArc(pen, tailRect, (float)MathUtils.ToDegrees(head - sweep/2 - Math.PI / 2), (float)MathUtils.ToDegrees(sweep));
            if (!opposite)
                return;
            point = GetXY(m_center, -headRadius, head);
            tail = GetXY(m_center, -tailRadius, head);
            g.DrawLine(pen, tail, point);
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

        private void DrawFaces(PaintEventArgs e, double maxThrust)
        {
            var scale = MathUtils.NumberToText(WorldScale, unit);
            e.Graphics.DrawRectangle(Palette.BlackPen, plusButton);
            e.Graphics.DrawRectangle(Palette.BlackPen, minusButton);
            e.Graphics.DrawString("+", Font, Palette.BlackInk, plusButton, CenteredLayout);
            e.Graphics.DrawString("-", Font, Palette.BlackInk, minusButton, CenteredLayout);
            e.Graphics.DrawString(scale, Font, Palette.BlackInk, scaleLabel, CenteredLayout);
            e.Graphics.DrawLines(Palette.BlackPen, scaleRuler);

            var r6 = BandRadius(6);
            e.Graphics.DrawEllipse(Palette.BlackPen, BandN(5));
            e.Graphics.DrawEllipse(Palette.BlackPen, BandN(7));
            e.Graphics.DrawString("0", Font, Palette.BlackInk, GetXY(m_center, r6, 0), CenteredLayout);
            e.Graphics.DrawString("-90", Font, Palette.BlackInk, GetXY(m_center, r6, -Math.PI/2), CenteredLayout);
            e.Graphics.DrawString("180", Font, Palette.BlackInk, GetXY(m_center, r6, Math.PI), CenteredLayout);
            e.Graphics.DrawString("90", Font, Palette.BlackInk, GetXY(m_center, r6, Math.PI/2), CenteredLayout);

            e.Graphics.DrawEllipse(Palette.BlackPen, BandN(1));
            e.Graphics.DrawEllipse(Palette.BlackPen, BandN(3));
            const int N = 12;
            var r2 = BandRadius(2);
            for (int i = 1; i <= N; i++)
            {
                var p = GetXY(m_center, r2, 2*i*Math.PI/N);
                e.Graphics.DrawString((i*30).ToString(), Font, Palette.BlackInk, p, CenteredLayout);
            }
            var r1 = BandRadius(1);
            var r15 = BandRadius(1.5f);
            var r25 = BandRadius(2.5f);
            var r3 = BandRadius(3);
            for (int i = 0; i < N*2; i++)
            {
                var pen = Palette.BlackPencil;
                var a = i*Math.PI/N;
                if (i%2 == 1)
                    e.Graphics.DrawLine(pen, GetXY(m_center, r1, a), GetXY(m_center, r3, a));
                else
                {
                    e.Graphics.DrawLine(pen, GetXY(m_center, r1, a), GetXY(m_center, r15, a));
                    e.Graphics.DrawLine(pen, GetXY(m_center, r25, a), GetXY(m_center, r3, a));
                }
            }
            e.Graphics.DrawPath(Palette.BlackPen, thrusterPath);
            var r4 = BandRadius(4);
            var r5 = BandRadius(5);
            var r35 = BandRadius(3.5f);
            var r45 = BandRadius(4.5f);
            e.Graphics.DrawString("0", Font, Palette.BlackInk, m_center.X, m_center.Y + r4, CenteredLayout);
            int zeros = (int) Math.Truncate(Math.Log10(maxThrust));
            double exponent = Math.Pow(10, zeros);
            double first = maxThrust/exponent;
            double mark;
            if (first < 1.3)
                mark = 0.1;
            else if (first < 2.5)
                mark = 0.2;
            else if (first < 6)
                mark = 0.5;
            else
                mark = 1.0;
            if (mark > 0)
                for (int i = 1; i <= first/mark; i++)
                {
                    var angle = Math.PI*(1 - i*mark/first);
                    if (i%2 == 1)
                        e.Graphics.DrawLine(Palette.BlackPencil, GetXY(m_center, r3, angle), GetXY(m_center, r5, angle));
                    else
                    {
                        var p = GetXY(m_center, r4, angle);
                        e.Graphics.DrawString((i*mark).ToString("##.0"), Font, Palette.BlackInk, p, CenteredLayout);
                        e.Graphics.DrawLine(Palette.BlackPencil, GetXY(m_center, r3, angle), GetXY(m_center, r35, angle));
                        e.Graphics.DrawLine(Palette.BlackPencil, GetXY(m_center, r45, angle), GetXY(m_center, r5, angle));
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
            else if (thruster.IsVisible(point))
                ThrustHit(alpha);
        }

        private void CompassHit(double alpha)
        {
            var handler = HeadingToChanged;
            if (handler != null)
                handler(this, new ValueEventArgs<double>(alpha));
        }

        private void RollHit(double alpha)
        {
            var handler = RollToChanged;
            if (handler != null)
                handler(this, new ValueEventArgs<double>(alpha));
        }

        private void ThrustHit(double alpha)
        {
            var thrustTo = (1-alpha/Math.PI);
            var handler = ThrustToChanged;
            if (handler != null)
                handler(this, new ValueEventArgs<double>(thrustTo));
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
