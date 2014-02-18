using System.Collections.Generic;
using System.ServiceModel;

namespace SF.Space
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServer" in both code and config file together.
    [ServiceContract(SessionMode=SessionMode.Required, ProtectionLevel=System.Net.Security.ProtectionLevel.None)]
    public interface IServer
    {
        [OperationContract(IsInitiating = false, IsTerminating = false)]
        bool Login(int idShip);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = true)]
        void Logout();

        [OperationContract(IsInitiating = true, IsTerminating = false)]
        int Connect(string password);

        [OperationContract(IsInitiating = false, IsTerminating = false)]
        ViewData GetView(int generation);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void SetHeadingTo(double value);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void SetRollTo(double value);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void SetThrustTo(double value);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void Fire(int idShipTo, int[] launchers);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void Launch(int idShip);
    }
}
