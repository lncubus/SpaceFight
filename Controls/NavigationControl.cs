using System.Drawing;
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
        private int bigRadius;
        private int smallRadius;
        private int bandWidth;
        private int halfBandWidth;
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
            bandWidth = m_size / 8;
            halfBandWidth = bandWidth / 2;
            smallField = bigField;
            smallField.Inflate(-bandWidth, -bandWidth);
            bigRadius = m_size / 2 - halfBandWidth;
            smallRadius = (m_size - 3 * bandWidth) / 2;
        }

        protected override void DrawContents(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            base.DrawContents(e);
            Calculate();
//            e.Graphics.DrawEllipse(Palette.WhitePaper, bigField);
            e.Graphics.DrawEllipse(Palette.BlackPen, bigField);
            e.Graphics.DrawEllipse(Palette.BlackPen, smallField);
            for (int i = 0; i < 360; i += 5)
            {
                var pen = i % 30 == 0 || i % 45 == 0 ? Palette.BlackPen : Palette.BlackPencil;
                var p = GetXY(halfSize - bandWidth / 3, i);
                var q = GetXY(halfSize, i);
                e.Graphics.DrawLine(pen, p, q);
            }
            const int N = 12;
            for (int i = 1; i <= N; i++)
            {
                var p = GetXY(bigRadius, 360 * i / N);
                e.Graphics.DrawString(i.ToString(), Font, Palette.BlackInk, p, CenteredLayout);
            }
            e.Graphics.DrawString("N", Font, Palette.BlackInk, GetXY(smallRadius, 0), CenteredLayout);
            e.Graphics.DrawString("W", Font, Palette.BlackInk, GetXY(smallRadius, -90), CenteredLayout);
            e.Graphics.DrawString("S", Font, Palette.BlackInk, GetXY(smallRadius, 180), CenteredLayout);
            e.Graphics.DrawString("E", Font, Palette.BlackInk, GetXY(smallRadius, 90), CenteredLayout);
//            DrawArrow(e.Graphics, Palette.SupportPen, m_bigRadius, m_bigRadius, Secondary, 174);
//            DrawArrow(e.Graphics, Palette.NavyPen, m_bigRadius, m_bigRadius, Heading, 170);
//            DrawArrow(e.Graphics, Palette.SignalPen, m_bigRadius, m_halfSize, HeadingTo, 10);
        }
    }
}
