using System;

namespace SF.Space
{
    public enum ShipType
    {
        LAC = 0,
        CLAC = 11,
    }

    public enum StarType
    {
        Planet = 0,
        Habitable = 1,
        Inhabited = 3,
        Gas = 4,
        Star = 8,
    }

    public enum ParticleType
    {
        None = 0,
        Ship = 1,
        Star = 2,
        Missile = 3,
    }

    public enum MouseEventType
    {
        MouseMove = 0,
        MouseUp = 1,
        MouseDown = 2,
    }

    public enum ControlMode
    {
        None = 0,
        Pilot = 1,
        Gunner = 2,
        Tactic = 3,
        Defense = 4,
    }

    [Flags]
    public enum DrawingOptions
    {
        None = 0,
        NoGrid = 0x01,
        MyMissileCircles = 0x02,
        FriendlyMissileCircles = 0x04,
        HostileMissileCircles = 0x08,
        MyVulnerableSectors = 0x10,
        FriendlyVulnerableSectors = 0x20,
        HostileVulnerableSectors = 0x40,
        FriendlySectorsByMyMissileRange = 0x1000
    };

    [Flags]
    public enum SelectableObjects
    {
        None = 0,
        Ships = 0x01,
        Stars = 0x02,
        Missiles = 0x04,
    };

    public class Launch
    {
        public bool isLeft;
        public int number;
        public IParticle target;
    }
}