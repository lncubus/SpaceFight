namespace SF.Space
{
    public interface IHelm
    {
        IShip Ship { get; }
        double HeadingTo { get; set; }
        double RollTo { get; set; }
        double AccelerateTo { get; set; }
    }
}
