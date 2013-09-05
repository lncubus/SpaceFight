using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HonorInterfaces
{
    public enum RanmaRepairSeverity
    {
        Ready = 0,
        Easy = 1,
        Medium = 2,
        Hard = 3
    }

    public class ShipSubsystemStatus
    {
        public int SubSystemNum { get; set; }
        public RanmaRepairSeverity Severity { get; set; }
    }

    public class ShipStatus
    {
        public Guid ShipGuid;
        public List<HonorInterfaces.ShipSubsystemStatus> SubsystemStatuses;
    }
}
