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

        public class LauncherWrapper
        {
            public MissileClass Class;

            public bool Selected { get; set; }

            public string Name
            {
                get { return Class.Name; }
                set { throw new InvalidOperationException("Not allowed"); }
            }

            public double Loaded { get; set; }
        }

        private MissileClass m_class;
        private double[] m_launchers;
        private BindingList<LauncherWrapper> m_launchersList;

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
            get { return m_launchers ?? ;  }
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
            if (m_launchersList == null)
                yield break;
            for (int i = 0; i < m_launchersList.Count; i++)
            {
                if (m_launchersList[i].Selected)
                    yield return i;
                i++;
            }
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
            int index = 0;
            var selected = GetSelection().ToList();
            var selectedAll = m_launchersList == null || m_launchersList.Count == selected.Count;
            m_launchersList = new BindingList<LauncherWrapper>();
            if (Launchers != null)
                foreach(var loading in Launchers)
                    m_launchersList.Add(
                        new LauncherWrapper
                        {
                            Class = Class,
                            Loaded = loading,
                            Selected = selectedAll || selected.Contains(index++),
                        });
            dataGridViewMissiles.DataSource = m_launchersList;
        }
    }

}
