using System;
using SF.Space;

namespace SF.ServerLibrary
{
    public sealed class LinearValue
    {
        public const double TimeEpsilon = 1E-6; // 86 ms

        public double Time0 { get; private set; }
        public double Time1 { get; private set; }
        public double Speed { get; private set; }
        public int Direction { get; private set; }
        public double ToValue { get; private set; }
        public double FromValue { get; private set; }
        public double Modulo { get; private set; }

        public LinearValue(double t, double value, double valueTo, double speed, double modulo)
        {
            Modulo = modulo;
            Speed = speed;
            Reset(t, value);
            Set(t, valueTo);
        }

        public void Reset(double t, double val)
        {
            Direction = 0;
            Time0 = t;
            Time1 = double.PositiveInfinity;
            FromValue = val;
            ToValue = val;
        }

        public void Set(double t, double goesTo)
        {
            double currentValue = Get(t);
            double diff = goesTo - currentValue;
            Direction = diff < 0 ? -1 : 1;
            diff = Math.Abs(diff);
            if (Modulo > 0)
            {
                if (diff > Modulo)
                    diff -= Math.Floor(diff / Modulo) * Modulo;
                if (diff > Modulo / 2)
                {
                    diff = Modulo - diff;
                    Direction = -Direction;
                }
            }
            if (MathUtils.NearlyEqual(Speed, 0))
            {
                Time0 = t;
                Time1 = double.PositiveInfinity;
                FromValue = currentValue;
                ToValue = goesTo;
            }
            double tau = diff / Speed;
            if (tau <= TimeEpsilon)
                Reset(t, goesTo);
            else
            {
                Time0 = t;
                Time1 = t + tau;
                FromValue = currentValue;
                ToValue = goesTo;
            }
        }

        public double Get(double t)
        {
            if (double.IsInfinity(Time1) || double.IsNaN(Time1))
                return ToValue;
            if (WillReset(t))
            {
                Reset(t, ToValue);
                return ToValue;
            }
            return FromValue + Direction * Speed * (t - Time0);
        }

        public void Respeed(double t, double speed)
        {
            var toValue = ToValue;
            var fromValue = Get(t);
            Reset(t, fromValue);
            Speed = speed;
            Set(t, toValue);
        }

        public bool WillReset(double t)
        {
            if (double.IsInfinity(Time1) || double.IsNaN(Time1))
                return false;
            if (t + TimeEpsilon >= Time1)
                return true;
            return false;
        }
    }
}
