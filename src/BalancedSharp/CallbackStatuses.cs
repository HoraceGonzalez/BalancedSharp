using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BalancedSharp
{
    [DataContract]
    public class CallbackStatuses
    {
        [DataMember(Name = "failed")]
        public int Failed { get; set; }

        [DataMember(Name = "pending")]
        public int Pending { get; set; }

        [DataMember(Name = "retrying")]
        public int Retrying { get; set; }

        [DataMember(Name = "succeeded")]
        public int Succeeded { get; set; }
    }
}
