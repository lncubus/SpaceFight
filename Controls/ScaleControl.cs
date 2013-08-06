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
    public partial class ScaleControl : UserControl
    {
        public const double DefaultMinValue = 1000;
        public const double DefaultMaxValue = 1000000000;
        private int m_first = 1;
        private int m_zeros = 7;
        private string m_unit = "км";

        public string Unit
        {
            get
            {
                return m_unit;
            }
            set
            {
                m_unit = value;
                UpdateDigits();
            }
        }

        public double MaxValue { get; set; }
        public double MinValue { get; set; }

        public double Value
        {
            get
            {
                return Math.Round(m_first * Math.Pow(10, m_zeros));
            }
            set
            {
                if (value  < MinValue)
                    value = MinValue;
                else if (value > MaxValue)
                    value = MaxValue;
                m_zeros = (int)Math.Truncate(Math.Log10(value));
                double first = value / Math.Pow(10, m_zeros);
                if (first < 1.5)
                    m_first = 1;
                else if (first < 3.5)
                    m_first = 2;
                else if (first < 7.5)
                    m_first = 5;
                else
                {
                    m_first = 1;
                    m_zeros++;
                }
            }
        }

        public ScaleControl()
        {
            InitializeComponent();
            MinValue = DefaultMinValue;
            MaxValue = DefaultMaxValue;
            UpdateDigits();
        }

        private void buttonZoomIn_Click(object sender, EventArgs e)
        {
            Value /= 3.3;
        }

        private void buttonZoomOut_Click(object sender, EventArgs e)
        {
            Value *= 3.3;
        }

        private void UpdateDigits()
        {
            labelScale.Text = MathUtils.NumberToText(Value, Unit);
        }
    }
}
