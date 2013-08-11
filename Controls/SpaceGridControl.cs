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
    public class SpaceGridControl : RoundControl
    {
        public const double DefaultScale = 1E6;
        public const double MaximumMissleRange = 10000000;
        public const double ThroatAngle = 45 * Math.PI / 180;
        public const double SkirtAngle = 15 * Math.PI / 180;
        //public const double BoardMedianAngle = (Math.PI - SkirtAngle / 2  + ThroatAngle / 2) / 2;
        //public const double BoardSweepAngle = (Math.PI - (SkirtAngle + ThroatAngle) / 2);

        public const int IntegerMaxValue = (int.MaxValue / 8) * 7;
        public const int IntegerMinValue = (int.MinValue / 8) * 7;

        public readonly PenSet VulnerableSectors =
            new PenSet
            {
                My = Pens.Firebrick,
                Friendly = Pens.DarkGray,
                Hostile = Pens.DarkGray,
            };

        public readonly PenSet MissleCircles =
            new PenSet
            {
                My = Pens.Navy,
                Friendly = Pens.DarkGray,
                Hostile = Pens.DarkRed,
            };

        public readonly PenSet ShipHulls =
            new PenSet
            {
                My = Pens.Navy,
                Friendly = Pens.Navy,
                Hostile = Pens.Firebrick,
            };

        /// <summary>
        /// Kilometers per inch
        /// </summary>
        public double WorldScale
        {
            get { return this.m_scale; }
            set
            {
                if (this.m_scale == value)
                    return;
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("Scale value should be positive.");
                this.m_scale = value;
                Invalidate();
            }
        }
        private double m_scale = DefaultScale;

        /// <summary>
        /// Coordinates of the center of the grid.
        /// </summary>
        public Vector Origin
        {
            get { return this.m_origin; }
            set
            {
                if (this.m_origin == value)
                    return;
                this.m_origin = value;
                Invalidate();
            }
        }
        private Vector m_origin;

        public bool Polar
        {
            get { return this.m_polar; }
            set
            {
                if (this.m_polar == value)
                    return;
                this.m_polar = value;
                Invalidate();
            }
        }
        private bool m_polar;

        public double Rotation
        {
            get { return m_rotation; }
            set
            {
                if (MathUtils.NearlyEqual(m_rotation, value))
                    return;
                m_rotation = value;
                Invalidate();
            }
        }
        private double m_rotation;

        public SpaceGridOptions Options { get; set; }

        private Rectangle m_client;

        public IShip OwnShip;
        public ICollection<IShip> Ships;

        public class Curve : List<Vector>
        {
            public Pen Pencil;
        };

        public readonly List<Curve> Curves = new List<Curve>();

        public Point WorldToDevice(Graphics g, Vector v)
        {
            var p = ((v - Origin) / WorldScale).Rotate(-Rotation);
            p.X *= g.DpiX;
            p.Y *= -g.DpiY;
            p.X += m_center.X;
            p.Y += m_center.Y;
            var radius = Math.Max(Math.Abs(p.X), Math.Abs(p.Y));
            if (radius >= IntegerMaxValue)
                p = p * IntegerMaxValue / radius;
            return new Point((int)p.X, (int)p.Y);
        }

        private int WorldToDevice(float dpi, double x)
        {
            var result = x * dpi / WorldScale;
            if (result <= IntegerMinValue)
                return IntegerMinValue;
            if (result >= IntegerMaxValue)
                return IntegerMaxValue;
            return (int)result;
        }

        protected override void DrawBackgroound(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            m_client = new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
            e.Graphics.FillRectangle(WhitePaper, m_client);
            e.Graphics.DrawRectangle(BlackPen, m_client);
        }

        private void DrawGridLines(Graphics graphics)
        { 
            if (Options.HasFlag(SpaceGridOptions.NoGrid))
                return;
            var logScale = Math.Log10(WorldScale);
            var scale = Math.Pow(10, Math.Ceiling(logScale) - logScale);
            var dpiX = (int)(scale * graphics.DpiX);
            var dpiY = (int)(scale * graphics.DpiY);
            if (Polar)
            {
                var n = (int)(m_client.Width / (2.0 * dpiX) + m_client.Height / (2.0 * dpiY));
                if (n <= 1)
                    n = 1;
                for (var i = 1; i <= n; i++)
                    graphics.DrawEllipse(BlackPencil, m_center.X - i * dpiX, m_center.Y - i * dpiY, 2 * i * dpiX, 2 * i * dpiY);
                const int N = 12;
                for (int i = 1; i <= N; i++)
                {
                    var p = new Point
                    {
                        X = (int)(m_center.X + dpiX * n * Math.Cos(2 * Math.PI * i / N)),
                        Y = (int)(m_center.Y + dpiY * n * Math.Sin(2 * Math.PI * i / N))
                    };
                    graphics.DrawLine(BlackPencil, m_center, p);
                    p = new Point
                    {
                        X = (int)(m_center.X + dpiX * n * Math.Cos(2 * Math.PI * (i + 0.5) / N)),
                        Y = (int)(m_center.Y + dpiY * n * Math.Sin(2 * Math.PI * (i + 0.5) / N))
                    };
                    var q = new Point
                    {
                        X = (int)(m_center.X + 2 * dpiX * Math.Cos(2 * Math.PI * (i + 0.5) / N)),
                        Y = (int)(m_center.Y + 2 * dpiY * Math.Sin(2 * Math.PI * (i + 0.5) / N))
                    };
                    graphics.DrawLine(BlackPencil, p, q);
                }
            }
            else
            {
                var n = m_client.Width / (2 * dpiX);
                for (var i = -n; i <= n; i++)
                {
                    var x = m_center.X + i * dpiX;
                    graphics.DrawLine(BlackPencil, x, m_client.Top, x, m_client.Bottom);
                }
                n = m_client.Height / (2 * dpiY);
                for (var i = -n; i <= n; i++)
                {
                    var y = m_center.Y + i * dpiY;
                    graphics.DrawLine(BlackPencil, m_client.Left, y, m_client.Right, y);
                }
            }
        }

        protected override void DrawContents(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            m_client = new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
            DrawGridLines(e.Graphics);
            foreach (var c in Curves)
                DrawCurve(e.Graphics, c);
            if (OwnShip != null)
                DrawShip(e.Graphics, OwnShip);
            if (Ships != null && Ships.Count > 0)
                foreach (var ship in Ships)
                    DrawShip(e.Graphics, ship);
        }

        private void DrawCurve(Graphics graphics, Curve curve)
        {
            if (curve.Count == 0)
                return;
            var points = new List<Point>();
            foreach (var p in curve)
            {
                var q = WorldToDevice(graphics, p);
                if (points.Count != 0)
                {
                    var prev = points[points.Count - 1];
                    if (prev.X == q.X && prev.Y == q.Y)
                        continue;
                }
                points.Add(q);
            }
            if (points.Count > 1)
                graphics.DrawLines(curve.Pencil, points.ToArray());
            else
            {
                var q = points[0];
                points.Clear();
                points.Add(new Point(q.X + 4, q.Y));
                points.Add(new Point(q.X, q.Y + 4));
                points.Add(new Point(q.X - 4, q.Y));
                points.Add(new Point(q.X, q.Y - 4));
                graphics.DrawPolygon(curve.Pencil, points.ToArray());
            }
        }

        private void DrawShip(Graphics graphics, IShip ship)
        {
            DrawShipHull(graphics, ship);
            if (ship.Board() > 0.5)
                DrawMissleCircle(graphics, ship);
            DrawVulnerableSectors(graphics, ship);
            DrawShipWedge(graphics, ship);
        }

        private void DrawVulnerableSectors(Graphics graphics, IShip ship)
        {
            var pen = VulnerableSectors.Select(OwnShip, ship);
            bool isMyShip = ship == OwnShip;
            bool isFriendlyShip = !isMyShip && (OwnShip != null && OwnShip.Nation == ship.Nation);
            bool isHostileShip = (OwnShip != null && OwnShip.Nation != ship.Nation);
            var range = (isMyShip || isFriendlyShip || OwnShip != null && OwnShip.Class == null) ?
                MaximumMissleRange : OwnShip.Class.MissleRange;
            if (Options.HasFlag(SpaceGridOptions.FriendlySectorsByMyMissleRange) && OwnShip != null && OwnShip.Class != null)
                range = OwnShip.Class.MissleRange;
            if ((isMyShip && !Options.HasFlag(SpaceGridOptions.MyVulnerableSectors)) ||
                (isFriendlyShip && !Options.HasFlag(SpaceGridOptions.FriendlyVulnerableSectors)) ||
                (isHostileShip && !Options.HasFlag(SpaceGridOptions.HostileVulnerableSectors)))
                return;
            WorldDrawPie(graphics, pen, ship.S, range, ship.Heading, ThroatAngle);
            WorldDrawPie(graphics, pen, ship.S, range, ship.Heading - Math.PI, SkirtAngle);
        }

        private void DrawMissleCircle(Graphics graphics, IShip ship)
        {
            bool isMyShip = ship == OwnShip;
            bool isFriendlyShip = !isMyShip && (OwnShip != null && OwnShip.Nation == ship.Nation);
            bool isHostileShip = (OwnShip != null && OwnShip.Nation != ship.Nation);
            if ((isMyShip && !Options.HasFlag(SpaceGridOptions.MyMissleCircles)) ||
                (isFriendlyShip && !Options.HasFlag(SpaceGridOptions.FriendlyMissleCircles)) ||
                (isHostileShip && !Options.HasFlag(SpaceGridOptions.HostileMissleCircles)))
                return;
            var pen = MissleCircles.Select(OwnShip, ship);
            var range = (ship.Class == null || (OwnShip != null && OwnShip.Nation != ship.Nation)) ? MaximumMissleRange : ship.Class.MissleRange;
            WorldDrawCircle(graphics, pen, ship.S, range);
        }

        private void DrawShipWedge(Graphics graphics, IShip ship)
        {
            if (ship.Board() > 0.5)
                return;
            double size = WorldScale / 4;
            var pen = SignalPen;
            var points = new[]
                {
                    WorldToDevice(graphics, ship.S + Vector.Direction(ship.Heading + Math.PI / 4) * size),
                    WorldToDevice(graphics, ship.S + Vector.Direction(ship.Heading + Math.PI * 11 / 12) * size),
                    WorldToDevice(graphics, ship.S + Vector.Direction(ship.Heading - Math.PI * 11 / 12) * size),
                    WorldToDevice(graphics, ship.S + Vector.Direction(ship.Heading - Math.PI / 4) * size),
                };
            bool visible = points.Any(p => m_client.Contains(p));
            if (!visible)
                return;
            graphics.DrawLine(pen, points[0], points[1]);
            graphics.DrawLine(pen, points[2], points[3]);
        }

        private void DrawShipHull(Graphics graphics, IShip ship)
        {
            const double alpha = Math.PI * 11 / 12;
            double size = WorldScale / 6;
            var pen = ShipHulls.Select(OwnShip, ship);
            var points = new[]
                {
                    WorldToDevice(graphics, ship.S + Vector.Direction(ship.Heading) * size),
                    WorldToDevice(graphics, ship.S + Vector.Direction(ship.Heading + alpha) * size),
                    WorldToDevice(graphics, ship.S + Vector.Direction(ship.Heading - alpha) * size),
                };
            bool visible = points.Any(p => m_client.Contains(p));
            if (!visible)
                return;
            graphics.DrawPolygon(pen, points);
        }

        private void WorldDrawCircle(Graphics graphics, Pen pen, Vector origin, double radius)
        {
            var rx = WorldToDevice(graphics.DpiX, radius);
            var ry = WorldToDevice(graphics.DpiY, radius);
            var p = WorldToDevice(graphics, origin);
            if (rx <= 0 || ry <= 0)
                return;
            var rect = new RectangleF(p.X - rx, p.Y - ry, 2 * rx, 2 * ry);
            graphics.DrawEllipse(pen, rect);
        }

        private void WorldDrawPie(Graphics graphics, Pen pen, Vector origin, double radius, double medianAngle, double sweepAngle)
        {
            var rx = WorldToDevice(graphics.DpiX, radius);
            var ry = WorldToDevice(graphics.DpiY, radius);
            var p = WorldToDevice(graphics, origin);
            if (rx <= 0 || ry <= 0)
                return;
            var rect = new RectangleF(p.X - rx, p.Y - ry, 2 * rx, 2 * ry);
            graphics.DrawPie(pen, rect,
                (float)MathUtils.ToDegrees(medianAngle - sweepAngle/ 2 - Math.PI / 2 - Rotation),
                (float)MathUtils.ToDegrees(sweepAngle));
        }
    }

    [Flags]
    public enum SpaceGridOptions
    {
        None = 0,
        NoGrid = 0x01,
        MyMissleCircles = 0x02,
        FriendlyMissleCircles = 0x04,
        HostileMissleCircles = 0x08,
        MyVulnerableSectors = 0x10,
        FriendlyVulnerableSectors = 0x20,
        HostileVulnerableSectors = 0x40,
        FriendlySectorsByMyMissleRange = 0x1000
    };

    public class PenSet
    {
        public Pen Default = Pens.Black;
        public Pen My { get; set; }
        public Pen Friendly { get; set; }
        public Pen Hostile { get; set; }

        public Pen Select(IShip OwnShip, IShip ship)
        {
            if (ship == OwnShip)
                return My;
            if (OwnShip != null && ship.Nation == OwnShip.Nation)
                return Friendly;
            if (OwnShip != null && ship.Nation != OwnShip.Nation)
                return Hostile;
            return Default;
        }
    }
}
