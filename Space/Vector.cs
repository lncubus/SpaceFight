using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace SF.Space
{
    [DataContract]
    public struct Vector : IEquatable<Vector>
    {
        [DataMember]
        [XmlAttribute("X")]
        public double X;
        [DataMember]
        [XmlAttribute("Y")]
        public double Y;

        public static readonly Vector Zero = new Vector(0, 0);
        public static readonly Vector EX = new Vector(1, 0);
        public static readonly Vector EY = new Vector(0, 1);

        /// <summary>
        /// This is a direction vector when angle is an angle from OY axis in clockwise direction.
        /// No the same as normal mathematical angle.
        /// </summary>
        /// <param name="angle"></param>
        public static Vector Direction(double angle)
        {
            return new Vector(Math.Sin(angle), Math.Cos(angle));
        }

        public Vector(double[] coordinates)
            : this(coordinates[0], coordinates[1])
        {
        }

        public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double SquareLength
        {
            get { return this * this; }
        }

        [XmlIgnore]
        public double Length
        {
            get { return Math.Sqrt(SquareLength); }
            set
            {
                var len = Length;
                if (len <= MathUtils.Epsilon)
                    throw new ArithmeticException("Vector length is too small");
                X = X * value / len;
                Y = Y * value / len;            
            }
        }

        [XmlIgnore]
        public double Argument
        {
            get
            {
                // this order, see Direction
                if (Length <= MathUtils.Epsilon)
                    return 0;
                return Math.Atan2(X, Y);
            }
        }

        public Vector Rotate(double theta)
        {
            return new Vector
            {
                X =  X * Math.Cos(theta) + Y * Math.Sin(theta),
                Y = -X * Math.Sin(theta) + Y * Math.Cos(theta)
            };
        }

        public override string ToString()
        {
            return string.Format("({0:0.000}, {1:0.000})", X, Y);
        }

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y);
        }

        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.X - b.X, a.Y - b.Y);
        }

        public static double operator *(Vector a, Vector b)
        {
            return a.X * b.X + a.Y * b.Y;
        }

        public static Vector operator -(Vector a)
        {
            return new Vector(-a.X, -a.Y);
        }

        public static Vector operator +(Vector a)
        {
            return new Vector(a.X, a.Y);
        }

        public static Vector operator /(Vector a, double f)
        {
            return new Vector(a.X / f, a.Y / f);
        }

        public static Vector operator *(double f, Vector a)
        {
            return a*f;
        }

        public static Vector operator *(Vector a, double f)
        {
            return new Vector(a.X * f, a.Y * f);
        }

        public static bool operator ==(Vector a, Vector b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Vector a, Vector b)
        {
            return !(a.Equals(b));
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                // Suitable nullity checks etc, of course :)
                hash = hash * 23 + X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            return (obj is Vector) && Equals((Vector)obj);
        }

        public bool Equals(Vector other)
        {
            return X == other.X && Y == other.Y;
        }
    }
}
