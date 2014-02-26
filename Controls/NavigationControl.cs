using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
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

        private int halfSize;
        private int bandWidth;
        private int margin;
        private Rectangle bigField;
        private Rectangle smallField;

        private void Calculate()
        {
            halfSize = m_size / 2;
            bigField = new Rectangle
            {
                X = m_center.X - halfSize,
                Y = m_center.Y - halfSize,
                Width = m_size,
                Height = m_size,
            };
            bandWidth = m_size / 12;
            margin = bandWidth/2;
            bigField.Inflate(-margin, -margin);
            smallField = bigField;
            smallField.Inflate(-bandWidth, -bandWidth);
        }

        protected override void DrawContents(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            base.DrawContents(e);
            if (Universe == null || Universe.Ship == null)
                return;
            Calculate();
//            e.Graphics.DrawEllipse(Palette.WhitePaper, bigField);
            var path = new GraphicsPath();
            path.AddEllipse(bigField);
            path.AddEllipse(smallField);
            e.Graphics.FillPath(Palette.ControlPaper, path);
            e.Graphics.DrawEllipse(Palette.BlackPen, bigField);
            e.Graphics.DrawEllipse(Palette.BlackPen, smallField);
            const int N = 12;
            var smallRadius = halfSize - bandWidth;
            for (int i = 1; i <= N; i++)
            {
                var p = GetXY(smallRadius, i*(360/N));
                e.Graphics.DrawString(i.ToString(), Font, Palette.BlackInk, p, CenteredLayout);
            }
            var r1 = halfSize - bandWidth - margin;
            var r4 = halfSize - margin;
            var r2 = r1 + bandWidth/4;
            var r3 = r4 - bandWidth/4;
            for (int i = 0; i < N * 2; i++)
            {
                var pen = Palette.BlackPencil;
                var a = i*(180/N);
                if (i%2 == 1)
                    e.Graphics.DrawLine(pen, GetXY(r1, a), GetXY(r4, a));
                else
                {
                    e.Graphics.DrawLine(pen, GetXY(r1, a), GetXY(r2, a));
                    e.Graphics.DrawLine(pen, GetXY(r3, a), GetXY(r4, a));
                }
            }

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
    }
}
