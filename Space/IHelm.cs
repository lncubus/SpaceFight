namespace SF.Space
{
    public interface IHelm : IShip
    {
        double HeadingTo { get; set; }
        double RollTo { get; set; }
        double ThrustTo { get; set; }
        byte[] Health { get; }
    }
}
