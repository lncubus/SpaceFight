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

        public const int IntegerMaxValue = (int.MaxValue / 8) * 7;
        public const int IntegerMinValue = (int.MinValue / 8) * 7;

        public PointF TextMisplacement = new PointF
        {
            X = 1.0F/8,
            Y = 1.0F/12,
        };

        public readonly PenSet VulnerableSectors =
            new PenSet
            {
                My = Pens.Firebrick,
                Friendly = Pens.DarkGray,
                Hostile = Pens.DarkGray,
            };

        public readonly PenSet MissileCircles =
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

        public readonly BrushSet ShipNames =
            new BrushSet
            {
                My = Brushes.Black,
                Friendly = Brushes.Navy,
                Hostile = Brushes.Firebrick,
            };

        /// <summary>
        /// Kilometers per inch
        /// </summary>
        public double WorldScale
        {
            get { return m_scale; }
            set
            {
                if (m_scale == value)
                    return;
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("Scale value should be positive.");
                m_scale = value;
                Invalidate();
            }
        }
        private double m_scale = DefaultScale;

        /// <summary>
        /// Coordinates of the center of the grid.
        /// </summary>
        public Vector Origin
        {
            get { return m_origin; }
            set
            {
                if (m_origin == value)
                    return;
                m_origin = value;
                Invalidate();
            }
        }
        private Vector m_origin;

        public bool StaticGrid
        {
            get { return m_staticGrid; }
            set
            {
                if (m_staticGrid == value)
                    return;
                m_staticGrid = value;
                Invalidate();
            }
        }
        private bool m_staticGrid = true;

        public bool Polar
        {
            get { return m_polar; }
            set
            {
                if (m_polar == value)
                    return;
                m_polar = value;
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

        public DrawingOptions Options { get; set; }
        public SelectableObjects Selectable { get; set; }

        private RectangleF m_client;

        public IShip OwnShip;

        private ICollection<IShip> m_ships;
        public ICollection<IShip> Ships
        {
            get { return m_ships; }
            set
            {
                var id = Guid.Empty;
                if (m_selectedParticle is IShip)
                    id = m_selectedParticle.Id;
                m_ships = value;
                if (id != Guid.Empty)
                    m_selectedParticle = id == OwnShip.Id ? OwnShip : m_ships.ById(id);
                Invalidate();
            }
        }

        private ICollection<IMissile> m_missiles;
        public ICollection<IMissile> Missiles
        {
            get { return m_missiles; }
            set
            {
                var id = Guid.Empty;
                if (m_selectedParticle is IMissile)
                    id = m_selectedParticle.Id;
                m_missiles = value;
                if (id != Guid.Empty)
                    m_selectedParticle = m_missiles.ById(id);
                Invalidate();
            }
        }

        private ICollection<Star> m_stars;
        public ICollection<Star> Stars
        {
            get { return m_stars; }
            set
            {
                var id = Guid.Empty;
                if (m_selectedParticle is Star)
                    id = m_selectedParticle.Id;
                m_stars = value;
                if (id != Guid.Empty)
                    m_selectedParticle = m_stars.ById(id);
                Invalidate();
            }
        }
        public class Curve : List<Vector>
        {
            public Pen Pencil;
        };

        public readonly List<Curve> Curves = new List<Curve>();

        public event EventHandler ParticleSelected;

        private IParticle m_selectedParticle;
        public IParticle Selected
        {
            get
            {
                return m_selectedParticle;
            }
            set
            {
                m_selectedParticle = value;
                Invalidate();
                var handler = ParticleSelected;
                if (handler != null)
                    handler(this, EventArgs.Empty);
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            Selected = SelectParticle(e.Location);
            base.OnMouseClick(e);
        }

        private IParticle SelectParticle(Point point)
        {
            Graphics g = CreateGraphics();
            var p = DeviceToWorld(g, point);
            var particles = new List<IParticle>();
            if (OwnShip != null && Selectable.HasFlag(SelectableObjects.Ships))
                particles.Add(OwnShip);
            if (Selectable.HasFlag(SelectableObjects.Ships))
                particles.AddRange(Ships);
            if (Selectable.HasFlag(SelectableObjects.Stars))
                particles.AddRange(Stars);
            if (Selectable.HasFlag(SelectableObjects.Missiles))
                particles.AddRange(Missiles);
            if (particles.Count == 0)
                return null;
            particles = particles.Where(particle => (particle.Position - p).Length - particle.Radius < WorldScale / 2).ToList();
            if (particles.Count == 0)
                return null;
            if (particles.Count == 1)
                return particles.First();
            particles.Sort((a, b) => (a.Position - p).SquareLength.CompareTo((b.Position - p).SquareLength));
            if (Selected == null)
                return particles.First();
            var i = particles.IndexOf(Selected);
            if (i < 0)
                return particles.First();
            i = (i + 1) % particles.Count;
            return particles[i];
        }

        public PointF WorldToDevice(Graphics g, Vector v)
        {
            var p = ((v - Origin) / WorldScale).Rotate(-Rotation);
            p.X *= g.DpiX;
            p.Y *= -g.DpiY;
            p.X += m_center.X;
            p.Y += m_center.Y;
            var radius = Math.Max(Math.Abs(p.X), Math.Abs(p.Y));
            if (radius >= IntegerMaxValue)
                p = p * IntegerMaxValue / radius;
            return new PointF((float)p.X, (float)p.Y);
        }

        public Vector DeviceToWorld(Graphics g, Point p)
        {
            return new Vector
            {
                X = (p.X - m_center.X) / g.DpiX,
                Y = -(p.Y - m_center.Y) / g.DpiY,
            }.Rotate(Rotation) * WorldScale + Origin;
        }

        private float WorldToDevice(float dpi, double x)
        {
            var result = x * dpi / WorldScale;
            if (result <= IntegerMinValue)
                return IntegerMinValue;
            if (result >= IntegerMaxValue)
                return IntegerMaxValue;
            return (float)result;
        }

        protected override void DrawBackgroound(PaintEventArgs e)
        {
            m_client = new RectangleF(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
            e.Graphics.FillRectangle(WhitePaper, m_client);
            e.Graphics.DrawRectangles(BlackPen, new[] { m_client });
        }

        protected void DrawGridLines(Graphics graphics)
        { 
            if (Options.HasFlag(DrawingOptions.NoGrid))
                return;
            var logScale = Math.Log10(WorldScale);
            float scale = (float)Math.Pow(10, Math.Ceiling(logScale) - logScale);
            float dpiX = scale * graphics.DpiX;
            float dpiY = scale * graphics.DpiY;
            if (Polar)
            {
                var n = (int) (m_client.Width / (2.0 * dpiX) + m_client.Height / (2.0 * dpiY));
                if (n < 2)
                    n = 2;
                for (var i = 1; i <= n; i++)
                    graphics.DrawEllipse(BlackPencil, m_center.X - i * dpiX, m_center.Y - i * dpiY, 2 * i * dpiX, 2 * i * dpiY);
                const int N = 12;
                for (int i = 1; i <= N; i++)
                {
                    var p = new PointF
                    {
                        X = (float)(m_center.X + dpiX * n * Math.Cos(2 * Math.PI * i / N)),
                        Y = (float)(m_center.Y + dpiY * n * Math.Sin(2 * Math.PI * i / N))
                    };
                    graphics.DrawLine(BlackPencil, m_center, p);
                    p = new PointF
                    {
                        X = (float)(m_center.X + dpiX * n * Math.Cos(2 * Math.PI * (i + 0.5) / N)),
                        Y = (float)(m_center.Y + dpiY * n * Math.Sin(2 * Math.PI * (i + 0.5) / N))
                    };
                    var q = new PointF
                    {
                        X = (float)(m_center.X + 2 * dpiX * Math.Cos(2 * Math.PI * (i + 0.5) / N)),
                        Y = (float)(m_center.Y + 2 * dpiY * Math.Sin(2 * Math.PI * (i + 0.5) / N))
                    };
                    graphics.DrawLine(BlackPencil, p, q);
                }
            }
            else
            {
                PointF center = m_center;
                if (StaticGrid)
                {
                    var dx = (float)(graphics.DpiX * Math.IEEERemainder(Origin.X, WorldScale * scale) / WorldScale);
                    var dy = (float)(graphics.DpiY * Math.IEEERemainder(Origin.Y, WorldScale * scale) / WorldScale);
                    center.X -= dx;
                    center.Y += dy;
                }
                var n = (int) (m_client.Width / (2 * dpiX)) + 1;
                for (var i = -n; i <= n; i++)
                {
                    var x = center.X + i * dpiX;
                    graphics.DrawLine(BlackPencil, x, m_client.Top, x, m_client.Bottom);
                }
                n = (int) (m_client.Height / (2 * dpiY)) + 1;
                for (var i = -n; i <= n; i++)
                {
                    var y = center.Y + i * dpiY;
                    graphics.DrawLine(BlackPencil, m_client.Left, y, m_client.Right, y);
                }
            }
        }

        protected override void DrawContents(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            m_client = new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
            DrawGridLines(e.Graphics);
            if (Stars != null && Stars.Count > 0)
                foreach (var s in Stars)
                    DrawStar(e.Graphics, s);
            if (Curves != null && Curves.Count > 0)
                foreach (var c in Curves)
                    DrawCurve(e.Graphics, c);
            if (Missiles != null && Missiles.Count > 0)
                foreach (var missile in Missiles)
                    DrawMissile(e.Graphics, missile);
            if (Ships != null && Ships.Count > 0)
                foreach (var ship in Ships)
                    DrawShip(e.Graphics, ship);
            if (OwnShip != null)
                DrawShip(e.Graphics, OwnShip);
            if (Selected != null)
                DrawSelection(e.Graphics, Selected);
        }

        protected void DrawCurve(Graphics graphics, Curve curve)
        {
            if (curve.Count == 0)
                return;
            var points = new List<PointF>();
            foreach (var p in curve)
            {
                var q = WorldToDevice(graphics, p);
                if (points.Count != 0)
                {
                    var prev = points[points.Count - 1];
                    if (MathUtils.NearlyEqual(prev.X, q.X) && MathUtils.NearlyEqual(prev.Y, q.Y))
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
                points.Add(new PointF(q.X + 4, q.Y));
                points.Add(new PointF(q.X, q.Y + 4));
                points.Add(new PointF(q.X - 4, q.Y));
                points.Add(new PointF(q.X, q.Y - 4));
                graphics.DrawPolygon(curve.Pencil, points.ToArray());
            }
        }

        protected bool IsVisible(RectangleF rect)
        {
            return m_client.IntersectsWith(rect);
        }

        protected bool IsVisible(PointF point)
        {
            return m_client.Contains((int)point.X, (int)point.Y);
        }

        protected bool IsVisible(PointF[] points)
        {
            return points.Any(p => m_client.Contains((int)p.X, (int)p.Y));
        }

        protected void DrawStar(Graphics graphics, Star star)
        {
            var pen = SignalPen;
            var brush = BlackInk;
            var rx = Math.Max(WorldToDevice(graphics.DpiX, star.Radius), graphics.DpiX / 32);
            var ry = Math.Max(WorldToDevice(graphics.DpiY, star.Radius), graphics.DpiY / 32);
            var p = WorldToDevice(graphics, star.Position);
            var rect = new RectangleF(p.X - rx, p.Y - ry, 2 * rx, 2 * ry);
            if (IsVisible(rect))
            {
                graphics.DrawEllipse(pen, rect);
            }
            WorldDrawText(graphics, brush, star.Position, star.Name);
        }

        protected void DrawShip(Graphics graphics, IShip ship)
        {
            if (!ship.IsDead())
            {
                DrawVulnerableSectors(graphics, ship);
                if (ship.Board() <= 0.5)
                    DrawShipWedge(graphics, ship);
                if (ship.Board() > 0.5)
                    DrawMissileCircle(graphics, ship);
            }
            DrawShipHull(graphics, ship);
            var brush = ShipNames.Select(OwnShip, ship);
            var text = string.IsNullOrEmpty(ship.Description) ? ship.Name : ship.Name + "\n" + ship.Description;
            WorldDrawText(graphics, brush, ship.Position, text);

        }

        protected void DrawVulnerableSectors(Graphics graphics, IShip ship)
        {
            var pen = VulnerableSectors.Select(OwnShip, ship);
            bool isMyShip = ship == OwnShip;
            bool isFriendlyShip = !isMyShip && (OwnShip != null && OwnShip.Nation == ship.Nation);
            bool isHostileShip = (OwnShip != null && OwnShip.Nation != ship.Nation);
            var range = (isMyShip || isFriendlyShip || OwnShip == null || OwnShip != null) ?
                Catalog.Instance.MaximumMissileRange : OwnShip.MissileRange();
            if (Options.HasFlag(DrawingOptions.FriendlySectorsByMyMissileRange) && OwnShip != null && OwnShip.Class != null)
                range = OwnShip.MissileRange();
            if ((isMyShip && !Options.HasFlag(DrawingOptions.MyVulnerableSectors)) ||
                (isFriendlyShip && !Options.HasFlag(DrawingOptions.FriendlyVulnerableSectors)) ||
                (isHostileShip && !Options.HasFlag(DrawingOptions.HostileVulnerableSectors)))
                return;
            WorldDrawPie(graphics, pen, ship.Position, range, ship.Heading, Catalog.Instance.ThroatAngle);
            WorldDrawPie(graphics, pen, ship.Position, range, ship.Heading - Math.PI, Catalog.Instance.SkirtAngle);
        }

        protected void DrawMissileCircle(Graphics graphics, IShip ship)
        {
            bool isMyShip = ship == OwnShip;
            bool isFriendlyShip = !isMyShip && (OwnShip != null && OwnShip.Nation == ship.Nation);
            bool isHostileShip = (OwnShip != null && OwnShip.Nation != ship.Nation);
            if ((isMyShip && !Options.HasFlag(DrawingOptions.MyMissileCircles)) ||
                (isFriendlyShip && !Options.HasFlag(DrawingOptions.FriendlyMissileCircles)) ||
                (isHostileShip && !Options.HasFlag(DrawingOptions.HostileMissileCircles)))
                return;
            var pen = MissileCircles.Select(OwnShip, ship);
            var range = (OwnShip != null && OwnShip.Nation != ship.Nation) ? Catalog.Instance.MaximumMissileRange : ship.MissileRange();
            WorldDrawCircle(graphics, pen, ship.Position, range);
        }

        protected void DrawSelection(Graphics graphics, IParticle p)
        {
            var size = (float)Math.Max(1.0F / 4, p.Radius / WorldScale);
            var pen = BlackPen;
            var position = WorldToDevice(graphics, p.Position);
            var rect = new RectangleF
            {
                X = position.X - size * graphics.DpiX,
                Y = position.Y - size * graphics.DpiY,
                Width = 2 * size * graphics.DpiX,
                Height = 2 * size * graphics.DpiY,
            };
            graphics.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
        }

        protected void DrawShipWedge(Graphics graphics, IShip ship)
        {
            double size = WorldScale / 4;
            var pen = SignalPen;
            var points = new[]
                {
                    WorldToDevice(graphics, ship.Position + Vector.Direction(ship.Heading + Math.PI / 4) * size),
                    WorldToDevice(graphics, ship.Position + Vector.Direction(ship.Heading + Math.PI * 11 / 12) * size),
                    WorldToDevice(graphics, ship.Position + Vector.Direction(ship.Heading - Math.PI * 11 / 12) * size),
                    WorldToDevice(graphics, ship.Position + Vector.Direction(ship.Heading - Math.PI / 4) * size),
                };
            if (!IsVisible(points))
                return;
            graphics.DrawLine(pen, points[0], points[1]);
            graphics.DrawLine(pen, points[2], points[3]);
        }

        protected void DrawShipHull(Graphics graphics, IShip ship)
        {
            var alpha = Math.PI * 11 / 12;
            double size = WorldScale / 6;
            var pen = ShipHulls.Select(OwnShip, ship);
            var points = new[]
                {
                    WorldToDevice(graphics, ship.Position + Vector.Direction(ship.Heading) * size),
                    WorldToDevice(graphics, ship.Position + Vector.Direction(ship.Heading + alpha) * size),
                    WorldToDevice(graphics, ship.Position + Vector.Direction(ship.Heading - alpha) * size),
                };
            if (!IsVisible(points))
                return;
            graphics.DrawPolygon(pen, points);
            if (!ship.IsDead())
                return;
            alpha = Math.PI*1/3;
            var beta = Math.PI*2/3;
            pen = SignalPen;
            points = new[]
                {
                    WorldToDevice(graphics, ship.Position + Vector.Direction(ship.Heading + alpha) * size),
                    WorldToDevice(graphics, ship.Position + Vector.Direction(ship.Heading - beta) * size),
                    WorldToDevice(graphics, ship.Position + Vector.Direction(ship.Heading - alpha) * size),
                    WorldToDevice(graphics, ship.Position + Vector.Direction(ship.Heading + beta) * size),
                };
            graphics.DrawLine(pen, points[0], points[1]);
            graphics.DrawLine(pen, points[2], points[3]);
        }

        protected void DrawMissile(Graphics graphics, IMissile missile)
        {
            //const double alpha = Math.PI * 11 / 12;
            double size = WorldScale / 6;// / 8;
            var pen = SignalPen;
                //OwnShip != null && missile.Nation == OwnShip.Nation ? ShipHulls.Friendly : ShipHulls.Hostile;
            var a = missile.Acceleration;
            if (a.Length > MathUtils.Epsilon)
                a.Length = size;
            var points = new[]
                {
                    WorldToDevice(graphics, missile.Position),
                    WorldToDevice(graphics, missile.Position + a),
                    WorldToDevice(graphics, missile.Position + a.Rotate(Math.PI/2) / 3),
                    WorldToDevice(graphics, missile.Position - a.Rotate(Math.PI/2) / 3),
                };
            if (!IsVisible(points))
                return;
            graphics.DrawLine(pen, points[0], points[1]);
            graphics.DrawLine(pen, points[2], points[3]);
        }

        protected void WorldDrawText(Graphics graphics, Brush brush, Vector origin, string text)
        {
            var p = WorldToDevice(graphics, origin);
            p = new PointF
            {
                X = p.X + TextMisplacement.X * graphics.DpiX,
                Y = p.Y + TextMisplacement.Y * graphics.DpiY,
            };
            if (IsVisible(p))
                graphics.DrawString(text, Font, brush, p);
        }

        protected void WorldDrawCircle(Graphics graphics, Pen pen, Vector origin, double radius)
        {
            var rx = WorldToDevice(graphics.DpiX, radius);
            var ry = WorldToDevice(graphics.DpiY, radius);
            var p = WorldToDevice(graphics, origin);
            if (rx <= 0 || ry <= 0)
                return;
            var rect = new RectangleF(p.X - rx, p.Y - ry, 2 * rx, 2 * ry);
            if (IsVisible(rect))
                graphics.DrawEllipse(pen, rect);
        }

        protected void WorldDrawPie(Graphics graphics, Pen pen, Vector origin, double radius, double medianAngle, double sweepAngle)
        {
            var rx = WorldToDevice(graphics.DpiX, radius);
            var ry = WorldToDevice(graphics.DpiY, radius);
            var p = WorldToDevice(graphics, origin);
            if (rx <= 0 || ry <= 0)
                return;
            var max = 2 * (m_client.Width + m_client.Height);
            if (IsVisible(p) && (rx + ry > max))
            {
                rx = ry = max;
            }
            var rect = new RectangleF(p.X - rx, p.Y - ry, 2 * rx, 2 * ry);
            if (IsVisible(rect))
                graphics.DrawPie(pen, rect,
                    (float)MathUtils.ToDegrees(medianAngle - sweepAngle/ 2 - Math.PI / 2 - Rotation),
                    (float)MathUtils.ToDegrees(sweepAngle));
        }


        [Flags]
        public enum DrawingOptions
        {
            None = 0,
            NoGrid = 0x01,
            MyMissileCircles = 0x02,
            FriendlyMissileCircles = 0x04,
            HostileMissileCircles = 0x08,
            MyVulnerableSectors = 0x10,
            FriendlyVulnerableSectors = 0x20,
            HostileVulnerableSectors = 0x40,
            FriendlySectorsByMyMissileRange = 0x1000
        };

        [Flags]
        public enum SelectableObjects
        {
            None = 0,
            Ships = 0x01,
            Stars = 0x02,
            Missiles = 0x04,
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

        public class BrushSet
        {
            public Brush Default = Brushes.Black;
            public Brush My { get; set; }
            public Brush Friendly { get; set; }
            public Brush Hostile { get; set; }

            public Brush Select(IShip OwnShip, IShip ship)
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
}
