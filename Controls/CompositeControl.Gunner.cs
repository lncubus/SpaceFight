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
//            var rightRacks = (Universe == null || Universe.Ship == null) ? defaultRacks : Universe.Ship.Class.Right;
//            var leftRacks = (Universe == null || Universe.Ship == null) ? defaultRacks : Universe.Ship.Class.Left;
            var right = (Universe == null || Universe.Ship == null) ? defaultReloadingTimes : Universe.Ship.Right.GetReloadingTimes();
            var left = (Universe == null || Universe.Ship == null) ? defaultReloadingTimes : Universe.Ship.Left.GetReloadingTimes();
            DrawRacks(g, 1, right);
            DrawRacks(g, -1, left);
            //Universe.Ship.Right.Reloading
            //var heading = (Universe == null || Universe.Ship == null) ? 1.0 : Universe.Ship.Heading;
            //var headingTo = (Universe == null || Universe.Ship == null) ? 1.1 : Universe.Ship.HeadingTo;
            //var speed = (Universe == null || Universe.Ship == null) ? Vector.Direction(1.3) : Universe.Ship.Speed;
            //var roll = (Universe == null || Universe.Ship == null) ? 2.7 : Universe.Ship.Roll;
            //var rollTo = (Universe == null || Universe.Ship == null) ? 2.9 : Universe.Ship.RollTo;
            //var thrust = (Universe == null || Universe.Ship == null) ? 1.9 : Universe.Ship.Thrust;
            //var thrustTo = (Universe == null || Universe.Ship == null) ? 2.1 : Universe.Ship.ThrustTo;
            //var thrustMax = (Universe == null || Universe.Ship == null) ? 4.0 : Universe.Ship.Class.MaximumAcceleration;

        }

        private void DrawRacks(Graphics g, int direction, KeyValuePair<MissileRack, double[]>[] racks)
        {
            double maxTime = racks.Select(pair => pair.Key.MissileClass.ReloadTime).Max();
            int k = 0;
            var size = new Size
            {
                Width = MathUtils.Gold(margin),
                Height = m_size - MathUtils.Gold(m_size),
            };
            for (int i = 0; i < racks.Length; i++)
            {
                var reload = racks[i].Value;
                for (int j = 0; j < reload.Length; j++, k++)
                {
                    Rectangle tube = new Rectangle
                    {
                        X = m_center.X + direction * (m_size/2 - k * margin), 
                        Y = m_center.Y, 
                        Size = size,
                    };
                    g.DrawRectangle(Palette.BlackPencil, tube);
                }
            }

            //int k = 0;
            //for (int i = 0; i < right.Length; i++)
            //{
                
            //    var r = right.Reloading[i]/right.;
            //}
            
            
        }
    }
}