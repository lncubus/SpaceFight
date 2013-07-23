
namespace SF.Space
{
    internal class Helm : IHelm
    {
        protected Dynamics Dynamics { get { return ((Ship)Ship).Dynamics; } }

        public double RollTo { get { return Dynamics.RollTo; } set { Dynamics.RollTo = value; } }
        public double HeadingTo { get { return Dynamics.HeadingTo; } set { Dynamics.HeadingTo = value; } }
        public double AccelerateTo { get { return Dynamics.AccelerateTo; } set { Dynamics.AccelerateTo = value; } }
        public IShip Ship { get; set; }
    }
}
