using System;
using System.ServiceModel;
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
        void SetAllSubsystemsStatuses(ShipStatus[] shipStatuses);
    }

    public interface IServerDamageCallbackContract
    {
        [OperationContract]
        bool DamageShip(Guid shipGuid, byte byteSeverity);

        [OperationContract]
        bool DestroyShip(Guid shipGuid);
    }

}


