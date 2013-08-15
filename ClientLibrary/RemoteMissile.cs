using SF.Space;
namespace SF.ClientLibrary
{
    internal class RemoteMissile : IMissile
    {
        private MissileDefinition m_that;

        public RemoteMissile(MissileDefinition def)
        {
            this.m_that = def;
        }

        public void Update(MissileDefinition def)
        {
            this.m_that = def;
        }

        public MissileClass Class
        {
            get
            {
                return Catalog.Instance.GetMissileClass(this.ClassName);
            }
        }

        public string Nation
        {
            get { return m_that.Nation; }
        }

        public string ClassName
        {
            get { return m_that.ClassName; }
        }

        public Vector S
        {
            get { return m_that.Position; }
        }

        public Vector V
        {
            get { return m_that.Speed; }
        }

        public Vector A
        {
            get { return m_that.Acceleration; }
        }

        public bool IsDead
        {
            get { return false; }
        }
    }
}
