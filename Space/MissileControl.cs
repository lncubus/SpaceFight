using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SF.Space
{
    [DataContract]
    public class MissileControl
    {
        [DataMember]
        public int Id { get; set; }
        [IgnoreDataMember]
        [XmlIgnore]
        public Missile Arrow { get; set; }
        [IgnoreDataMember]
        [XmlIgnore]
        public IShip Target { get; set; }
        [DataMember]
        public int IdTarget { get; set; }
        [IgnoreDataMember]
        [XmlIgnore]
        public IShip Origin { get; set; }
        [DataMember]
        public int IdOrigin { get; set; }
        [DataMember]
        public double Thrust { get; set; }
        [DataMember]
        public double Started { get; set; }
        [DataMember]
        public Ecm Jammer { get; set; }
        [DataMember]
        public double Remaining { get; set; }

        public void Move(double time, double dt)
        {
            if (time - Started > Remaining)
                Arrow = null;
            if (Arrow == null)
                return;
            var s = Target.Position - Arrow.Position;
            var v = Target.Speed - Arrow.Speed;
            var a = Target.Acceleration;
            var h = s.Argument;
            var h1 = Vector.Direction(h);
            var a1 = a * h1 - Thrust;
            var va1 = v * h1 / a1;
            var sa1 = s * h1 / a1;
            // t^2 + 2 va1 t + 2 sa1
            var d = va1 * va1 - 2 * sa1;
            if (d < 0)
                return;
            d = Math.Sqrt(d);
            var eta = va1 >= d ? va1 - d : va1 + d;
            var h2 = h1.Rotate(Math.PI / 2);
            var full = s + (v * h2 * eta + a * h2 * eta * eta / 2) * h2;
            Arrow.Acceleration = Thrust*Vector.Direction(full.Argument);
            Arrow.Position += Arrow.Speed*dt + Arrow.Acceleration*dt*dt/2;
            Arrow.Speed += Arrow.Acceleration * dt;
        }
    }
}
