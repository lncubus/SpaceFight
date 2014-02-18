using System.Runtime.Serialization;

namespace SF.Space
{
    [DataContract]
    public struct Board
    {
        [DataMember]
        public double Accumulator;

        [DataMember]
        public double[] Launchers;
    }
}
