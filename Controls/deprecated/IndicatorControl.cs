using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
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
            labelAccelerationTitle.Text += ", " + AccelerationUnit;
            UpdateControl();
        }

        private Vector m_acceleration;
        private Vector m_speed;
        private Vector m_position;
        public const string AccelerationUnit = "км/с²";
        public const string SpeedUnit = "км/с";
        public const string LocationUnit = "км";

        public Vector Acceleration
        {
            get { return m_acceleration; }
            set
            {
                m_acceleration = value;
                UpdateControl();
            }
        }

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
            textAccelerationX.Text = MathUtils.NumberToText(Acceleration.X, string.Empty);
            textAccelerationX.SelectAll();
            textAccelerationY.Text = MathUtils.NumberToText(Acceleration.Y, string.Empty);
            textAccelerationY.SelectAll();
            textAccelerationFull.Text = MathUtils.NumberToText(Acceleration.Length, string.Empty);
            textAccelerationFull.SelectAll();
            textAccelerationAngle.Text = (Acceleration.Length <= MathUtils.Epsilon ? 0 : MathUtils.ToDegreesInt(Acceleration.Argument)).ToString(CultureInfo.InvariantCulture);
            textAccelerationAngle.SelectAll();
            textSpeedX.Text = MathUtils.NumberToText(Speed.X, string.Empty);
            textSpeedX.SelectAll();
            textSpeedY.Text = MathUtils.NumberToText(Speed.Y, string.Empty);
            textSpeedY.SelectAll();
            textSpeedFull.Text = MathUtils.NumberToText(Speed.Length, string.Empty);
            textSpeedFull.SelectAll();
            textSpeedAngle.Text = (Speed.Length <= MathUtils.Epsilon ? 0 : MathUtils.ToDegreesInt(Speed.Argument)).ToString(CultureInfo.InvariantCulture);
            textSpeedAngle.SelectAll();
            textLocationX.Text = MathUtils.NumberToText(Position.X, string.Empty);
            textLocationX.SelectAll();
            textLocationY.Text = MathUtils.NumberToText(Position.Y, string.Empty);
            textLocationY.SelectAll();
        }
    }
}
