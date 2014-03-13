using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SF.Space;

namespace SF.Controls
{
    partial class CompositeControl
    {
        private double maxReloadTime;
        private KeyValuePair<MissileRack, double[]>[] right;
        private KeyValuePair<MissileRack, double[]>[] left;

        public event EventHandler<ValueEventArgs<Launch>> Fired;

        private struct Tube
        {
            public Rectangle rectangle;
            public double reloading;
            public int level;
            public string name;
        }

        private Tube[] rightRectangles;
        private Tube[] leftRectangles;

        private void CalculateMissiles()
        {
            right = (Universe == null || Universe.Ship == null) ? defaultReloadingTimes : Universe.Ship.Right.GetReloadingTimes();
            left = (Universe == null || Universe.Ship == null) ? defaultReloadingTimes : Universe.Ship.Left.GetReloadingTimes();
            maxReloadTime = right.Union(left).Select(pair => pair.Key.MissileClass.ReloadTime).Max();
            CalculateMissileRectangles(right, 1, ref rightRectangles);
            CalculateMissileRectangles(left, -1, ref leftRectangles);
        }

        private void CalculateMissileRectangles(KeyValuePair<MissileRack, double[]>[] racks, int direction, ref Tube[] rectangles)
        {
            int k = 0;
            var height = m_size - MathUtils.Gold(m_size);
            var width = MathUtils.Gold(margin);
            int n = racks.Sum(pair => pair.Value.Length);
            if (rectangles == null || rectangles.Length != n)
                rectangles = new Tube[n];
            foreach (KeyValuePair<MissileRack, double[]> rack in racks)
            {
                var missile = rack.Key.MissileClass;
                var reload = rack.Value;
                var m = missile.ReloadTime/maxReloadTime;
                for (int j = 0; j < reload.Length; j++, k++)
                {
                    var x = m_center.X + direction*(m_size/2 - k*margin) + ((direction > 0) ? -width : 0);
                    var r = reload[j] / missile.ReloadTime;
                    int l = r > 0 ? m_center.Y + (int) (r*m*height) : (r < 0 ? -1 : 0);
                    rectangles[k] = new Tube
                    {
                        level = l,
                        reloading = r,
                        name = missile.Name,
                        rectangle = new Rectangle
                        {
                            X = x,
                            Y = m_center.Y,
                            Width = width,
                            Height = (int) (m*height),
                        }
                    };
                }
            }
        }

        private void DrawRacks(Graphics g)
        {
            DrawRacks(g, rightRectangles);
            DrawRacks(g, leftRectangles);
        }

        private void DrawRackBacks(Graphics g)
        {
            DrawRackBacks(g, rightRectangles);
            DrawRackBacks(g, leftRectangles);
            var height = m_size - MathUtils.Gold(m_size);
            int n = Universe == null || Universe.Ship == null ? 19 : Universe.Ship.Missiles;
            var brush = (n == 0) ? Palette.SignalInk : Palette.BlackInk;
            g.DrawString("Ракет в погребе: " + n, Font, brush, m_center.X, m_center.Y - height, CenteredLayout);
        }

        private void DrawRackBacks(Graphics g, Tube[] rectangles)
        {
            foreach (var tube in rectangles)
            {
                Brush brush;
                if (tube.reloading < 0)
                    brush = Palette.RedInk;
                else if (tube.reloading > 0)
                    brush = Palette.YellowInk;
                else
                    brush = Palette.GreenInk;
                if (tube.reloading <= 0)
                    g.FillRectangle(brush, tube.rectangle);
                else if (tube.reloading > 0)
                    g.FillRectangle(brush, tube.rectangle.Left, tube.level, tube.rectangle.Width, tube.rectangle.Bottom - tube.level);
                g.DrawString(tube.name, Font, Palette.BlackInk, tube.rectangle, VerticalLayout);
            }
        }

        private void DrawRacks(Graphics g, Tube[] rectangles)
        {
            foreach (var tube in rectangles)
            {
                Pen pen;
                if (tube.reloading < 0)
                    pen = Palette.SignalPen;
                else if (tube.reloading > 0)
                    pen = Palette.BlackPen;
                else
                    pen = Palette.NavyPen;
                g.DrawRectangle(pen, tube.rectangle);
                if (tube.reloading < 0)
                {
                    g.DrawLine(pen, tube.rectangle.Left, tube.rectangle.Top, tube.rectangle.Right, tube.rectangle.Bottom);
                    g.DrawLine(pen, tube.rectangle.Left, tube.rectangle.Bottom, tube.rectangle.Right, tube.rectangle.Top);
                }
                else if (tube.level > 0)
                    g.DrawLine(pen, tube.rectangle.Left, tube.level, tube.rectangle.Right, tube.level);
            }
        }

        private bool MissileControlMouseHit(Point point, double alpha)
        {
            if (CheckTubes(false, point))
                return true;
            if (CheckTubes(true, point))
                return true;
            return false;
        }

        private bool CheckTubes(bool isLeft, Point point)
        {
            Tube[] tubes = isLeft ? leftRectangles : rightRectangles;
            for (int i = 0; i < tubes.Length; i++)
            {
                var tube = tubes[i];
                if (tube.rectangle.Contains(point) && MathUtils.NearlyEqual(tube.reloading, 0))
                {
                    Fire(isLeft, i);
                    return true;
                }
            }
            return false;
        }

        private void Fire(bool isLeft, int n)
        {
            var handler = Fired;
            if (handler != null)
            {
                var launch = new Launch
                {
                    isLeft = isLeft,
                    number = n,
                    target = Selected,
                };
                handler(this, new ValueEventArgs<Launch>(launch));
            }
        }

        private static KeyValuePair<MissileRack, double[]>[] defaultReloadingTimes =
        {
            new KeyValuePair<MissileRack, double[]>
            (
                new MissileRack
                {
                    Count = 3,
                    MissileClass = new MissileClass
                    {
                        Name = "Стрела",
                        ReloadTime = 120,
                    }
                },
                new []{ 0.0, 120.0, -1}
            ),
            new KeyValuePair<MissileRack, double[]>
            (
                new MissileRack
                {
                    Count = 2,
                    MissileClass = new MissileClass
                    {
                        Name = "Дротик",
                        ReloadTime = 180,
                    }
                },
                new []{ 0.0, 90.0 }
            ),
        };
    }
}