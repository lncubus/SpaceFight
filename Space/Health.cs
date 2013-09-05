using System.Runtime.Serialization;

namespace SF.Space
{
    [DataContract]
    public struct Health
    {
        [DataMember] // Атака
        public byte Attack;

        [DataMember] // Оборона
        public byte Defense;

        [DataMember] // Импеллер
        public byte Wedge;

        [DataMember] // Навигация
        public byte Navigation;

        [DataMember] // Жизнеобеспечение
        public byte Life;

        [DataMember] // Шлюзы ЛАКов
        public byte Gate;

        [DataMember] // Паруса Варш-кой
        public byte Sails;

        [DataMember] // Реактор
        public byte Reactor;
    }
}
