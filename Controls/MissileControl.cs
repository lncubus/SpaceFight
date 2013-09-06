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

        private MissileClass m_class;
        private double[] m_launchers;

        public MissileClass Class
        {
            get { return m_class; }
            set
            {
                if (m_class == value)
                    return;
                m_class = value;
                UpdateControls();
            }
        }

        public double[] Launchers
        {
            get { return m_launchers;  }
            set
            {
                m_launchers = value;
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
            dataGridViewMissiles.RowCount = Launchers == null ? 0 : Launchers.Length;
            if (Launchers != null)
                for (int i = 0; i < Launchers.Length; i++)
                {
                    dataGridViewMissiles.Rows[i].Cells[columnSelected.Index].Value = selectedAll || selected.Contains(i);
                    dataGridViewMissiles.Rows[i].Cells[columnName.Index].Value = Class == null ? string.Empty : Class.Name;
                }
        }
        //int index = 0;
        //public class LauncherWrapper
        //{
        //    public MissileClass Class;

        //    public bool Selected { get; set; }

        //    public string Name
        //    {
        //        get { return Class == null ? string.Empty : Class.Name; }
        //        set { throw new InvalidOperationException("Not allowed"); }
        //    }

        //    public double Loaded { get; set; }
        //}
        //private BindingList<LauncherWrapper> m_launchersList;
        //m_launchersList = new BindingList<LauncherWrapper>();
        //dataGridViewMissiles.DataSource = m_launchersList;
    }

}
