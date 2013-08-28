using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace SF.Space
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServer" in both code and config file together.
    [ServiceContract(SessionMode=SessionMode.Required, ProtectionLevel=System.Net.Security.ProtectionLevel.None)]
    public interface IServer
    {
        [OperationContract(IsInitiating = false, IsTerminating = false)]
        bool Login(string nation, string ship);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = true)]
        void Logout();

        [OperationContract(IsInitiating = true, IsTerminating = false)]
        void Connect(string password);

        [OperationContract(IsInitiating = false, IsTerminating = false)]
        KeyValuePair<string, string[]>[] GetShipNames();

        [OperationContract(IsInitiating = false, IsTerminating = false)]
        CatalogDefinition GetCatalog();

        [OperationContract(IsInitiating = false, IsTerminating = false)]
        View GetView();

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void SetHeadingTo(double value);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void SetRollTo(double value);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void SetThrustTo(double value);

        [OperationContract(IsOneWay = true, IsInitiating = false, IsTerminating = false)]
        void Fire(bool left, string to, int number);
    }
}
