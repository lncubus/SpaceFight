using System.Runtime.Serialization;

namespace SF.Space
{
    [DataContract]
    public class Health
    {
        private double m_attack;
        private double m_defense;
        private double m_engine;
        private double m_navigation;
        private bool m_changed;

        public Health()
        {
            Attack = Defense = Engine = Navigation = 1;
            ResetChanged();
        }

        public double Rate
        {
            get
            {
                return (Attack + Defense + Engine + Navigation)/4;
            }
        }

        [DataMember]
        public double Attack
        {
            get
            {
                return m_attack;
            }
            set
            {
                ChangeValue(ref m_attack, value);
            }
        }

        [DataMember]
        public double Defense
        {
            get
            {
                return m_defense;
            }
            set
            {
                ChangeValue(ref m_defense, value);
            }
        }

        [DataMember]
        public double Engine
        {
            get
            {
                return m_engine;
            }
            set
            {
                ChangeValue(ref m_engine, value);
            }
        }

        [DataMember]
        public double Navigation
        {
            get
            {
                return m_navigation;
            }
            set
            {
                ChangeValue(ref m_navigation, value);
            }
        }

        public bool Changed
        {
            get
            {
                return m_changed;
            }
        }

        public void ResetChanged()
        {
            m_changed = false;
        }

        public void Crash()
        {
            Attack = Defense = Engine = Navigation = 0;
        }

        protected void ChangeValue(ref double member, double value)
        {
            m_changed |= !MathUtils.NearlyEqual(member, value);
            member = value;
        }
    }
}
