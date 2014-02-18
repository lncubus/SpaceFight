namespace SF.Space
{
    public interface IHelm : IShip
    {
        Health Health { get; }
        double HeadingTo { get; set; }
        double RollTo { get; set; }
        double ThrustTo { get; set; }
        Board Right { get; set; }
        Board Left { get; set; }
        string Carrier { get; }
    }
}
