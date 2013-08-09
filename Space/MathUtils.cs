using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SF.Space
{
    public static class MathUtils
    {
        public const double Epsilon = 1E-12;
        public static readonly double Ln10 = Math.Log(10);

        public static string NumberToText(double value, string unit)
        {
            return value.ToString("N0") + " " + unit;
        }

        public static int Round(double a)
        {
            return (int)Math.Round(a);
        }

        public static double ToDegrees(double a)
        {
            return 180 * a / Math.PI;
        }

        public static double ToRadians(double d)
        {
            return d * Math.PI / 180;
        }

        public static int ToDegreesInt(double a)
        {
            return Round(ToDegrees(a));
        }

        public static bool NearlyEqual(double a, double b)
        {
            return NearlyEqual(a, b, Epsilon);
        }

        public static bool NearlyEqual(double a, double b, double epsilon)
        {
            if (a == b)
                return true;
            var diff = Math.Abs(a - b);
            var sum = Math.Abs(a) + Math.Abs(b);
            return diff <= sum * epsilon;
        }

        public static double Linear(double min, double max, double lambda)
        {
            return min * (1 - lambda) + max * lambda;
        }

        public static double LimitedLinear(double min, double max, double lambda)
        {
            if (lambda < 0)
                lambda = 0;
            if (lambda > 1)
                lambda = 1;
            return Linear(min, max, lambda);
        }
    }
}
