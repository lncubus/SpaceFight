using System;
using System.Drawing;
using SF.Space;

namespace SF.Controls
{
    partial class CompositeControl
    {
        private void CompassMouseHit(Point point, double alpha)
        {
            if (compass.IsVisible(point))
                CompassHit(alpha);
            else if (roller.IsVisible(point))
                RollHit(alpha);
            else if (thruster.IsVisible(point))
                ThrustHit(alpha);
        }

        private void DrawCompass(Graphics g)
        {
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
            DrawFaces(g, thrustMax);
            DrawArrow(g, Palette.NavyPen, BandRadius(2.8f), BandRadius(5.2f), thrust, Math.PI / 18, false);
            DrawArrow(g, Palette.SignalPen, BandRadius(5.2f), BandRadius(2.8f), thrustTo, Math.PI / 18, false);
            DrawArrow(g, Palette.NavyPen, BandRadius(0.8f), BandRadius(3.2f), heading, Math.PI / 18);
            DrawArrow(g, Palette.SignalPen, BandRadius(3.2f), BandRadius(0.8f), headingTo, Math.PI / 18);
            if (speed.Length > MathUtils.Epsilon)
                DrawDiamond(g, Palette.SecondPen, BandRadius(3.2f), BandRadius(0.8f), speed.Argument, Math.PI / 18, true);
            DrawArrow(g, Palette.NavyPen, BandRadius(4.8f), BandRadius(7.2f), roll, Math.PI / 16);
            DrawArrow(g, Palette.SignalPen, BandRadius(7.2f), BandRadius(4.8f), rollTo, Math.PI / 16);
            DrawDiamond(g, Palette.LeftPen, BandRadius(6.8f), BandRadius(5.2f), roll - Math.PI / 2, Math.PI / 24);
            DrawDiamond(g, Palette.RightPen, BandRadius(6.8f), BandRadius(5.2f), roll + Math.PI / 2, Math.PI / 24);
        }

        public void DrawArrow(Graphics g, Pen pen, int headRadius, int tailRadius, double head, double sweep, bool opposite = true)
        {
            var left = GetXY(m_center, tailRadius, head - sweep / 2);
            var right = GetXY(m_center, tailRadius, head + sweep / 2);
            var point = GetXY(m_center, headRadius, head);
            var tail = GetXY(m_center, tailRadius, head);
            var tailRect = Band(tailRadius);
            g.DrawLines(pen, new[] { left, point, right });
            g.DrawLine(pen, tail, point);
            g.DrawArc(pen, tailRect, (float)MathUtils.ToDegrees(head - sweep / 2 - Math.PI / 2), (float)MathUtils.ToDegrees(sweep));
            if (!opposite)
                return;
            point = GetXY(m_center, -headRadius, head);
            tail = GetXY(m_center, -tailRadius, head);
            g.DrawLine(pen, tail, point);
        }

        public void DrawDiamond(Graphics g, Pen pen, int headRadius, int tailRadius, double head, double sweep, bool opposite = false)
        {
            var r = (headRadius + tailRadius) / 2;
            var left = GetXY(m_center, r, head - sweep / 2);
            var right = GetXY(m_center, r, head + sweep / 2);
            var point = GetXY(m_center, headRadius, head);
            var tail = GetXY(m_center, tailRadius, head);
            g.DrawPolygon(pen, new[] { left, point, right, tail });
            g.DrawLine(pen, tail, point);
            if (!opposite)
                return;
            point = GetXY(m_center, -headRadius, head);
            tail = GetXY(m_center, -tailRadius, head);
            g.DrawLine(pen, tail, point);
        }

        private void DrawFaces(Graphics g, double maxThrust)
        {
            var r6 = BandRadius(6);
            g.DrawEllipse(Palette.BlackPen, BandN(5));
            g.DrawEllipse(Palette.BlackPen, BandN(7));
            g.DrawString("0", Font, Palette.BlackInk, GetXY(m_center, r6, 0), CenteredLayout);
            g.DrawString("-90", Font, Palette.BlackInk, GetXY(m_center, r6, -Math.PI / 2), CenteredLayout);
            g.DrawString("180", Font, Palette.BlackInk, GetXY(m_center, r6, Math.PI), CenteredLayout);
            g.DrawString("90", Font, Palette.BlackInk, GetXY(m_center, r6, Math.PI / 2), CenteredLayout);

            g.DrawEllipse(Palette.BlackPen, BandN(1));
            g.DrawEllipse(Palette.BlackPen, BandN(3));
            const int N = 12;
            var r2 = BandRadius(2);
            for (int i = 1; i <= N; i++)
            {
                var p = GetXY(m_center, r2, 2 * i * Math.PI / N);
                g.DrawString((i * 30).ToString(), Font, Palette.BlackInk, p, CenteredLayout);
            }
            var r1 = BandRadius(1);
            var r15 = BandRadius(1.5f);
            var r25 = BandRadius(2.5f);
            var r3 = BandRadius(3);
            for (int i = 0; i < N * 2; i++)
            {
                var pen = Palette.BlackPencil;
                var a = i * Math.PI / N;
                if (i % 2 == 1)
                    g.DrawLine(pen, GetXY(m_center, r1, a), GetXY(m_center, r3, a));
                else
                {
                    g.DrawLine(pen, GetXY(m_center, r1, a), GetXY(m_center, r15, a));
                    g.DrawLine(pen, GetXY(m_center, r25, a), GetXY(m_center, r3, a));
                }
            }
            g.DrawPath(Palette.BlackPen, thrusterPath);
            var r4 = BandRadius(4);
            var r5 = BandRadius(5);
            var r35 = BandRadius(3.5f);
            var r45 = BandRadius(4.5f);
            g.DrawString("0", Font, Palette.BlackInk, m_center.X, m_center.Y + r4, CenteredLayout);
            int zeros = (int)Math.Truncate(Math.Log10(maxThrust));
            double exponent = Math.Pow(10, zeros);
            double first = maxThrust / exponent;
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
                for (int i = 1; i <= first / mark; i++)
                {
                    var angle = Math.PI * (1 - i * mark / first);
                    if (i % 2 == 1)
                        g.DrawLine(Palette.BlackPencil, GetXY(m_center, r3, angle), GetXY(m_center, r5, angle));
                    else
                    {
                        var p = GetXY(m_center, r4, angle);
                        g.DrawString((i * mark).ToString("##.0"), Font, Palette.BlackInk, p, CenteredLayout);
                        g.DrawLine(Palette.BlackPencil, GetXY(m_center, r3, angle), GetXY(m_center, r35, angle));
                        g.DrawLine(Palette.BlackPencil, GetXY(m_center, r45, angle), GetXY(m_center, r5, angle));
                    }
                }
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
            var thrustTo = (1 - alpha / Math.PI) * Universe.Ship.Class.MaximumAcceleration;
            if (thrustTo < 0)
                thrustTo = 0;
            var handler = ThrustToChanged;
            if (handler != null)
                handler(this, new ValueEventArgs<double>(thrustTo));
        }
    }
}
