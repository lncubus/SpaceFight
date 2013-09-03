using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using HonorInterfaces;

namespace SF.ServerLibrary.ServerDamageContract
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IServerDamageCallbackContract))]
    public interface IServerDamageContract
    {
        [OperationContract]
        void ServerConnect();

        [OperationContract]
        void SetAllSubsystemsStatuses(List<ShipStatus> shipStatuses);
    }

    public interface IServerDamageCallbackContract
    {
        [OperationContract]
        bool DamageShip(Guid shipGuid, ShipSubsystemStatus subsystemStatus);

        [OperationContract]
        bool DestroyShip(Guid shipGuid);
    }

}


