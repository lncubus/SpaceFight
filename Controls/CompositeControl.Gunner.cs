using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SF.Space;

namespace SF.Controls
{
    partial class CompositeControl
    {
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

        private void DrawRacks(Graphics g)
        {
            var right = (Universe == null || Universe.Ship == null) ? defaultReloadingTimes : Universe.Ship.Right.GetReloadingTimes();
            var left = (Universe == null || Universe.Ship == null) ? defaultReloadingTimes : Universe.Ship.Left.GetReloadingTimes();
            double maxTime = right.Union(left).Select(pair => pair.Key.MissileClass.ReloadTime).Max();
            DrawRacks(g, 1, maxTime, right);
            DrawRacks(g, -1, maxTime, left);
        }

        private void DrawRacks(Graphics g, int direction, double maxTime, KeyValuePair<MissileRack, double[]>[] racks)
        {
            int k = 0;
            var height = m_size - MathUtils.Gold(m_size);
            var width = MathUtils.Gold(margin);
            foreach (KeyValuePair<MissileRack, double[]> rack in racks)
            {
                var missile = rack.Key.MissileClass;
                var reload = rack.Value;
                var m = missile.ReloadTime/maxTime;
                for (int j = 0; j < reload.Length; j++, k++)
                {
                    var x = m_center.X + direction*(m_size/2 - k*margin) + ((direction > 0) ? -width : 0);
                    var tube = new Rectangle
                    {
                        X = x,
                        Y = m_center.Y,
                        Width = width,
                        Height = (int)(m*height)
                    };
                    var r = reload[j]/missile.ReloadTime;
                    Pen pen;
                    if (r < 0)
                        pen = Palette.SignalPen;
                    else if (r > 0)
                        pen = Palette.BlackPen;
                    else
                        pen = Palette.NavyPen;
                    g.DrawRectangle(pen, tube);
                    if (r < 0)
                    {
                        g.DrawLine(pen, tube.Left, tube.Top, tube.Right, tube.Bottom);
                        g.DrawLine(pen, tube.Left, tube.Bottom, tube.Right, tube.Top);
                    }
                    else if (r > 0)
                    {
                        var y = tube.Top + (int)(r * tube.Height);
                        g.DrawLine(pen, tube.Left, y, tube.Right, y);
                    }
                }
            }
        }

        private void MissileControlMouseHit(Point point, double alpha)
        {
            //throw new NotImplementedException();
        }
    }
}