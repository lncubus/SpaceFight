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
    public partial class IndicatorControl : UserControl
    {
        public IndicatorControl()
        {
            InitializeComponent();
            labelLocationTitle.Text += ", " + LocationUnit;
            labelSpeedTitle.Text += ", " + SpeedUnit;
            UpdateControl();
        }

        private Vector m_speed;
        private Vector m_position;
        public const string SpeedUnit = "км/с";
        public const string LocationUnit = "км";

        public Vector Speed
        {
            get { return m_speed; }
            set
            {
                m_speed = value;
                UpdateControl();
            }
        }

        public Vector Position
        {
            get { return m_position; }
            set
            {
                m_position = value;
                UpdateControl();
            }
        }

        private void UpdateControl()
        {
            labelSpeedX.Text = MathUtils.NumberToText(Speed.X, string.Empty);
            labelSpeedY.Text = MathUtils.NumberToText(Speed.Y, string.Empty);
            labelSpeedFull.Text = MathUtils.NumberToText(Speed.Length, string.Empty);
            labelLocationX.Text = MathUtils.NumberToText(Position.X, string.Empty);
            labelLocationY.Text = MathUtils.NumberToText(Position.Y, string.Empty);
        }
    }
}
