﻿using System.Runtime.Serialization;

namespace SF.Space
{
    [DataContract]
    public class Nation : INamed
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
    }
}
