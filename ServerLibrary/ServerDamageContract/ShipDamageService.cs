using System;
using System.Collections.Generic;
using System.ServiceModel;
using HonorInterfaces;

namespace SF.ServerLibrary.ServerDamageContract
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class ShipDamageService : IServerDamageContract
    {


        public Universe m_universe;

        public void ServerConnect()
        {
            //Let's remember Armlet Server callback channel
            IServerDamageContract callbackChannel = OperationContext.Current.GetCallbackChannel<IServerDamageContract>();
            
        }

        public void SetAllSubsystemsStatuses(List<ShipStatus> shipStatuses)
        {
            //Итерируешься о кораблям в листе, сравниваешь со своими Guid-ами в шипах. Обращаешься к своему объекту Universe, проставляешь статусы
            throw new NotImplementedException();
        }
    }
}
