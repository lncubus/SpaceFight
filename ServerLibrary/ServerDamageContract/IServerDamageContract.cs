using System;
using System.ServiceModel;
using HonorInterfaces;

namespace SF.ServerLibrary.ServerDamageContract
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IServerDamageCallbackContract))]
    public interface IServerDamageContract
    {
        [OperationContract(IsOneWay = true)]
        void ServerConnect();

        [OperationContract(IsOneWay = true)]
        void SetAllSubsystemsStatuses(ShipStatus[] shipStatuses);
    }

    public interface IServerDamageCallbackContract
    {
        [OperationContract(IsOneWay = true)]
        void DamageShip(Guid shipGuid, byte byteSeverity);

        [OperationContract(IsOneWay = true)]
        void DestroyShip(Guid shipGuid);
    }

}


