using System;
using System.ServiceModel;
using HonorInterfaces;

namespace SF.ServerLibrary.ServerDamageContract
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class ShipDamageService : IServerDamageContract
    {
        private Universe m_universe = SpaceServer.Universe;

        public void ServerConnect()
        {
            //Let's remember Armlet Server callback channel  - запиши его куда-нибудь, и дергай из него методы для сообщения информации мне на сервак

            m_universe.DamageServiceCallback = OperationContext.Current.GetCallbackChannel<IServerDamageCallbackContract>();
            
        }

        public void SetAllSubsystemsStatuses(ShipStatus[] shipStatuses)
        {
            //Итерируешься о кораблям в листе, сравниваешь со своими Guid-ами в шипах. Обращаешься к своему объекту Universe, проставляешь статусы
            // Обращайся - m_universe
          
            throw new NotImplementedException();
        }
    }
}
