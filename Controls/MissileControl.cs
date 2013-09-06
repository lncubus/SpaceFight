using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
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
            var accumulator = ShipClass == null ? 0 : 1 - (Board.Accumulator/ShipClass.RechargeTime);
            progressBarAccumulator.Value = (int)(progressBarAccumulator.Maximum*accumulator);
            if (accumulator < 0.3)
                progressBarAccumulator.ForeColor = Color.Crimson;
            else if (accumulator < 0.8)
                progressBarAccumulator.ForeColor = Color.Chocolate;
            else if (accumulator < 1)
                progressBarAccumulator.ForeColor = Color.Yellow;
            else
                progressBarAccumulator.ForeColor = Color.Green;
        }
    }

}
