using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using SF.Space;

namespace SF.Controls
{
    public partial class MissileControl : UserControl
    {
        public MissileControl()
        {
            InitializeComponent();
        }

        private MissileClass m_missileClass;
        private ShipClass m_shipClass;
        private Board m_board;

        public MissileClass MissileClass
        {
            get { return m_missileClass; }
            set
            {
                if (m_missileClass == value)
                    return;
                m_missileClass = value;
                UpdateControls();
            }
        }

        public ShipClass ShipClass
        {
            get { return m_shipClass; }
            set
            {
                if (m_shipClass == value)
                    return;
                m_shipClass = value;
                UpdateControls();
            }
        }

        public Board Board
        {
            get { return m_board; }
            set
            {
                m_board = value;
                UpdateControls();
            }
        }

        public int[] Selected
        {
            get { return GetSelection().ToArray(); }
        }

        private IEnumerable<int> GetSelection()
        {
            for (int i = 0; i < dataGridViewMissiles.RowCount; i++)
                if ((bool)dataGridViewMissiles.Rows[i].Cells[columnSelected.Index].Value)
                    yield return i;
        }

        private int Updating;
        
        public void UpdateControls()
        {
            try
            {
                Updating++;
                InternalUpdateControls();
            }
            finally
            {
                Updating--;
            }
        }

        private void InternalUpdateControls()
        {
            var selected = GetSelection().ToList();
            var selectedAll = dataGridViewMissiles.RowCount == selected.Count;
            dataGridViewMissiles.RowCount = Board.Launchers == null ? 0 : Board.Launchers.Length;
            if (Board.Launchers != null)
                for (int i = 0; i < Board.Launchers.Length; i++)
                {
                    dataGridViewMissiles.Rows[i].Cells[columnSelected.Index].Value = selectedAll || selected.Contains(i);
                    dataGridViewMissiles.Rows[i].Cells[columnName.Index].Value = MissileClass == null ? string.Empty : MissileClass.Name;
                }
            var accumulator = Board.Accumulator < 0 ? -1 : (ShipClass == null || ShipClass.RechargeTime <= MathUtils.Epsilon) ? 0 : 1 - (Board.Accumulator / ShipClass.RechargeTime);
            progressBarAccumulator.Value = (int)(progressBarAccumulator.Maximum*accumulator);
            progressBarAccumulator.ForeColor = GetColor(accumulator);
            dataGridViewMissiles.Invalidate();
        }

        private Color GetColor(double accumulator)
        {
            if (accumulator < 0)
                return Color.Gray;
            if (accumulator < 0.3)
                return Color.LightCoral;
            if (accumulator < 0.8)
                return Color.SandyBrown;
            if (accumulator < 1)
                return Color.Yellow;
            return Color.LightGreen;
        }

        private void dataGridViewMissiles_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            int n = e.RowIndex;
            if (Board.Launchers == null || e.ColumnIndex == 0 || n < 0 || n >= Board.Launchers.Length)
                return;
            e.PaintBackground(e.CellBounds, false);
            var load = (ShipClass == null || ShipClass.ReloadTime <= MathUtils.Epsilon) ? 0 : 1 - (Board.Launchers[n] / ShipClass.ReloadTime);
            var brush = new SolidBrush(GetColor(load));
            var rect = new Rectangle(e.CellBounds.X, e.CellBounds.Y, (int) (e.CellBounds.Width*load), e.CellBounds.Height);
            e.Graphics.FillRectangle(brush, rect);
            e.PaintContent(e.CellBounds);
            e.Handled = true;
        }
    }

}
