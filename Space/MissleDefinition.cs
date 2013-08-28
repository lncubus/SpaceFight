using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SF.Space
{
    [DataContract]
    public class MissileDefinition : IMissile
    {
        [XmlIgnore]
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string ClassName { get; set; }
        [DataMember]
        public string Nation { get; set; }
        [DataMember]
        public Vector Acceleration { get; set; }
        [DataMember]
        public Vector Position { get; set; }
        [DataMember]
        public Vector Speed { get; set; }
        [DataMember]
        public double Weight { get; set; }
        [DataMember]
        public double Radius { get; set; }

        public static MissileDefinition Store(IMissile missile)
        {
            return new MissileDefinition
            {
                Id = missile.Id,
                ClassName = missile.Class.Name,
                Nation = missile.Nation,
                Acceleration = missile.Acceleration,
                Speed = missile.Speed,
                Position = missile.Position,
                Weight = missile.Weight,
                Radius = missile.Radius,
            };
        }

        public MissileClass Class
        {
            get { return Catalog.Instance.GetMissileClass(ClassName); }
        }

        public bool IsDead
        {
            get { return false; }
        }

        public string Name
        {
            get { return ClassName; }
        }
    }
}
