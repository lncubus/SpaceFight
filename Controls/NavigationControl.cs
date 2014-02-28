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
        private Rectangle[] bands;
        private int[] bandRadius;
        private Rectangle plusButton, minusButton;

        private void Calculate()
        {
            margin = m_size / 24;
            bandRadius = new int[8];
            bands = new Rectangle[bandRadius.Length];
            for (int i = 0; i < bandRadius.Length; i++)
            {
                var r = m_size/2 - margin*i;
                bandRadius[i] = r;
                bands[i] = new Rectangle
                {
                    X = m_center.X - r,
                    Y = m_center.Y - r,
                    Width = 2 * r,
                    Height = 2 * r,
                };
            }
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
            //smallField = bigField;
            //smallField.Inflate(-bandWidth, -bandWidth);
            //rollRadius = m_size/6;
            //rollCenter = new Point
            //{
            //    X = margin + rollRadius,
            //    Y = ClientRectangle.Height - (margin + rollRadius),
            //};
            //rollField = new Rectangle
            //{
            //    X = margin,
            //    Y = ClientRectangle.Height - (margin + 2*rollRadius),
            //    Width = 2*rollRadius,
            //    Height = 2*rollRadius,
            //};
            var path = new GraphicsPath();
            path.AddEllipse(bands[1]);
            path.AddEllipse(bands[3]);
            compass = new Region(path);
            path = new GraphicsPath();
            path.AddEllipse(bands[5]);
            path.AddEllipse(bands[7]);
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
            //roller.GetRegionScans()
            DrawCompassFace(e);
            DrawRolloverFace(e);
            e.Graphics.DrawRectangle(Palette.BlackPen, plusButton);
            e.Graphics.DrawRectangle(Palette.BlackPen, minusButton);
            e.Graphics.DrawString("+", Font, Palette.BlackInk, plusButton, CenteredLayout);
            e.Graphics.DrawString("-", Font, Palette.BlackInk, minusButton, CenteredLayout);
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

        private void DrawRolloverFace(PaintEventArgs e)
        {
            e.Graphics.DrawEllipse(Palette.BlackPen, bands[5]);
            e.Graphics.DrawEllipse(Palette.BlackPen, bands[7]);
            e.Graphics.DrawString("0", Font, Palette.BlackInk, GetXY(m_center, bandRadius[6], 0), CenteredLayout);
            e.Graphics.DrawString("-90", Font, Palette.BlackInk, GetXY(m_center, bandRadius[6], -Math.PI / 2), CenteredLayout);
            e.Graphics.DrawString("180", Font, Palette.BlackInk, GetXY(m_center, bandRadius[6], Math.PI), CenteredLayout);
            e.Graphics.DrawString("90", Font, Palette.BlackInk, GetXY(m_center, bandRadius[6], Math.PI / 2), CenteredLayout);
        }

        private void DrawCompassFace(PaintEventArgs e)
        {
            e.Graphics.DrawEllipse(Palette.BlackPen, bands[1]);
            e.Graphics.DrawEllipse(Palette.BlackPen, bands[3]);
            const int N = 12;
            for (int i = 1; i <= N; i++)
            {
                var p = GetXY(m_center, bandRadius[2], 2*i*Math.PI/N);
                e.Graphics.DrawString(i.ToString(), Font, Palette.BlackInk, p, CenteredLayout);
            }
            var r2 = bandRadius[1] - margin/2;
            var r3 = bandRadius[3] + margin/2;
            for (int i = 0; i < N*2; i++)
            {
                var pen = Palette.BlackPencil;
                var a = i*Math.PI/N;
                if (i%2 == 1)
                    e.Graphics.DrawLine(pen, GetXY(m_center, bandRadius[1], a), GetXY(m_center, bandRadius[3], a));
                else
                {
                    e.Graphics.DrawLine(pen, GetXY(m_center, bandRadius[1], a), GetXY(m_center, r2, a));
                    e.Graphics.DrawLine(pen, GetXY(m_center, r3, a), GetXY(m_center, bandRadius[3], a));
                }
            }
        }

        protected override void MouseHit(Point point, double alpha)
        {
            base.MouseHit(point, alpha);
            if (plusButton.Contains(point))
                ZoomIn();
            else if (minusButton.Contains(point))
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
            //throw new NotImplementedException();
        }

        private void ZoomOut()
        {
            //throw new NotImplementedException();
        }
    }
}
