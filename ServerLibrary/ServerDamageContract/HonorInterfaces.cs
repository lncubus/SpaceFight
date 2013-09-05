using System;

namespace HonorInterfaces
{
    public enum RanmaRepairSeverity
    {
        Ready = 0,
        Easy = 1,
        Medium = 2,
        Hard = 3
    }

    public enum RandirSubsytsem
    {
        // Атака
        Attack = 0,
        // Оборона
        Defense = 1,
        // Импеллер
        Wedge = 2,
        // Навигация
        Navigation = 3,
        // Жизнеобеспечение
        Life = 4,
        // Шлюзы ЛАКов
        Gate = 5,
        // Паруса Варш-кой
        Sails = 6,
        // Реактор
        Reactor = 7,
    }

    public class ShipSubsystemStatus
    {
        public RandirSubsytsem SubSystem { get; set; }
        public RanmaRepairSeverity Severity { get; set; }
    }

    public class ShipStatus
    {
        public Guid ShipGuid;
        public ShipSubsystemStatus[] SubsystemStatuses;
    }
}
