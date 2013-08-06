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

        private enum FirstDigit
        {
            Zero = 0,
            One = 1,
            Two = 2,
            Five = 5
        }

        private FirstDigit m_first = FirstDigit.One;
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
                return Math.Round((int)m_first * Math.Pow(10, m_zeros));
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
                    m_first = FirstDigit.One;
                else if (first < 3.5)
                    m_first = FirstDigit.Two;
                else if (first < 7.5)
                    m_first = FirstDigit.Five;
                else
                {
                    m_first = FirstDigit.One;
                    m_zeros++;
                }
                UpdateDigits();
                EventHandler handler = OnValueChanged;
                if (handler != null)
                    handler(this, null);
            }
        }

        public event EventHandler OnValueChanged;

        public ScaleControl()
        {
            InitializeComponent();
            MinValue = DefaultMinValue;
            MaxValue = DefaultMaxValue;
            UpdateDigits();
        }

        private void buttonZoomIn_Click(object sender, EventArgs e)
        {
            ZoomIn();
        }

        private void buttonZoomOut_Click(object sender, EventArgs e)
        {
            ZoomOut();
        }

        private void ZoomIn()
        {
            double scale = Value;
            switch (m_first)
            {
                case FirstDigit.Zero :
                    scale = MinValue;
                    break;
                case FirstDigit.One :
                case FirstDigit.Two :
                    scale /= 2;
                    break;
                case FirstDigit.Five :
                    scale /= 2.5;
                    break;
            }
            Value = scale;
        }

        private void ZoomOut()
        {
            double scale = Value;
            switch (m_first)
            {
                case FirstDigit.Zero:
                    scale = MinValue;
                    break;
                case FirstDigit.One:
                case FirstDigit.Five:
                    scale *= 2;
                    break;
                case FirstDigit.Two:
                    scale *= 2.5;
                    break;
            }
            Value = scale;
        }

        private void UpdateDigits()
        {
            labelScale.Text = MathUtils.NumberToText(Value, Unit);
        }
    }
}
