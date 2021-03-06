﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SF.Space;

namespace SF.Controls
{
    public partial class CompositeControl : SpaceGridControl
    {
        private int DpiX, DpiY;
        private int margin;
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

        private ControlMode mode;// = ControlMode.Pilot;

        public ControlMode Mode
        {
            get
            {
                return mode;
            }
            set
            {
                if (mode == value)
                    return;
                mode = value;
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
        
        private void CalculateScale()
        {
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
        }

        protected override void DrawContents(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.HighQuality;
            DpiX = (int)g.DpiX;
            DpiY = (int)g.DpiY;
            margin = m_size / 24;
            CalculateScale();
            switch (Mode)
            {
                case ControlMode.Pilot:
                    CalculateCompass();
                    DrawCompassBack(g);
                    break;
                case ControlMode.Gunner:
                    CalculateMissiles();
                    DrawRackBacks(g);
                    break;
            }
            g.FillRectangle(Palette.ControlPaper, plusButton);
            g.FillRectangle(Palette.ControlPaper, minusButton);
            base.DrawContents(g);
            DrawScale(g);
            switch (Mode)
            {
                case ControlMode.Pilot:
                    DrawCompass(g);
                    break;
                case ControlMode.Gunner:
                    DrawRacks(g);
                    break;
            }
            if (Universe.Ship.ControlShip != null)
            {
                var c = Universe.Ship.ControlShip;
                var message = string.Format(
                    "Повреждения\nДвигатель:\t{0}%\nУправление:\t{1}%\nРакеты:\t\t{2}%\nЗащита:\t\t{3}%",
                    (int)(100*c.EngineDamage),
                    (int)(100*c.NavigationDamage),
                    (int)(100*c.AttackDamage),
                    (int)(100*c.DefenseDamage)
                    );
                var size = g.MeasureString(message, Font);
                var rect = new RectangleF
                {
                    X = (float) (m_size/24.0),
                    Y = (float) (m_size - size.Height - m_size/24.0),
                    Size = size,
                };
                g.DrawString(message, Font, Palette.BlackInk, rect);
            }
        }

        private void DrawScale(Graphics g)
        {
            var scale = MathUtils.NumberToText(WorldScale, unit);
            g.DrawRectangle(Palette.BlackPen, plusButton);
            g.DrawRectangle(Palette.BlackPen, minusButton);
            g.DrawString("+", Font, Palette.BlackInk, plusButton, CenteredLayout);
            g.DrawString("-", Font, Palette.BlackInk, minusButton, CenteredLayout);
            g.DrawString(scale, Font, Palette.BlackInk, scaleLabel, CenteredLayout);
            g.DrawLines(Palette.BlackPen, scaleRuler);
        }

        protected override void MouseHit(Point point, double alpha, MouseEventType type)
        {
            base.MouseHit(point, alpha, type);
            if (plusButton.Contains(point) && type == MouseEventType.MouseUp)
                ZoomIn();
            else if (minusButton.Contains(point) && type == MouseEventType.MouseUp)
                ZoomOut();
            else switch (Mode)
            {
                case ControlMode.Pilot:
                    CompassMouseHit(point, alpha);
                    break;
                case ControlMode.Gunner:
                    if (Selected == null || Universe.Ship == Selected)
                        TacticMouseHit(point, alpha, type);
                    else if (!MissileControlMouseHit(point))
                        TacticMouseHit(point, alpha, type);
                    break;
                case ControlMode.Tactic:
                    TacticMouseHit(point, alpha, type);
                    break;
            }
        }

        private void TacticMouseHit(Point point, double alpha, MouseEventType type)
        {
            if (MouseEventType.MouseUp == type)
                Selected = SelectParticle(point);
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
