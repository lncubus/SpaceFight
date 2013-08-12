using SF.Space;
namespace SF.ClientLibrary
{
    internal class RemoteMissle : IMissle
    {
        private MissleDefinition m_that;

        public RemoteMissle(MissleDefinition def)
        {
            this.m_that = def;
        }

        public void Update(MissleDefinition def)
        {
            this.m_that = def;
        }

        public MissleClass Class
        {
            get
            {
                return Catalog.Instance.GetMissleClass(this.ClassName);
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
    }
}
