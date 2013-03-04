using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BalancedSharp
{
    [DataContract]
    public class CallbackStatus
    {
        [DataMember]
        public int failed { get; set; }

        [DataMember]
        public int pending { get; set; }

        [DataMember]
        public int retrying { get; set; }

        [DataMember]
        public int succeeded { get; set; }
    }
}
