using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SF.ServerLibrary
{
    public class UniverseSingleton
    {
        private static Universe instance;

        public UniverseSingleton(Universe _instance)
        {
            instance = _instance;
        }

        public static Universe Instance
        {
            get
            {
                if (instance == null)
                {
                    throw new Exception("Universe Singleton initialization error");
                }
                return instance;
            }
        }
    }
}
