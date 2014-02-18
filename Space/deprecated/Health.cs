using System.Runtime.Serialization;

namespace SF.Space
{
    [DataContract]
    public class Health
    {
        public int Subsystems
        {
            get
            {
                InitializeIfNeeded();
                return m_values.Length;
            }
        }

        private double[] m_values;
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
                return (Attack + Defense + Engine + Navigation) / 4;
            }
        }

        [DataMember]
        public double Attack
        {
            get
            {
                InitializeIfNeeded();
                return m_values[Subsystem.Attack];
            }
            set
            {
                InitializeIfNeeded();
                ChangeValue(ref m_values[Subsystem.Attack], value);
            }
        }

        [DataMember]
        public double Defense
        {
            get
            {
                InitializeIfNeeded();
                return m_values[Subsystem.Defense];
            }
            set
            {
                InitializeIfNeeded();
                ChangeValue(ref m_values[Subsystem.Defense], value);
            }
        }

        [DataMember]
        public double Engine
        {
            get
            {
                InitializeIfNeeded();
                return m_values[Subsystem.Wedge];
            }
            set
            {
                InitializeIfNeeded();
                ChangeValue(ref m_values[Subsystem.Wedge], value);
            }
        }

        [DataMember]
        public double Navigation
        {
            get
            {
                InitializeIfNeeded();
                return m_values[Subsystem.Navigation];
            }
            set
            {
                InitializeIfNeeded();
                ChangeValue(ref m_values[Subsystem.Navigation], value);
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

        public void Damage(double damage, int subsystem)
        {
            InitializeIfNeeded();
            while (Rate > 0 && damage > 0)
            {
                if (m_values[subsystem % Subsystems] > damage)
                {
                    m_values[subsystem % Subsystems] -= damage;
                    damage = 0;
                }
                else
                {
                    damage -= m_values[subsystem % Subsystems];
                    m_values[subsystem % Subsystems] = 0;
                }
                subsystem++;
            }
            m_changed = true;
        }

        protected void ChangeValue(ref double member, double value)
        {
            if (value < 0)
                value = 0;
            m_changed |= !MathUtils.NearlyEqual(member, value);
            member = value;
        }

        private void InitializeIfNeeded()
        {
            if (m_values == null || m_values.Length == 0)
                m_values = new double[Subsystem.Navigation + 1];
        }
    }
}
