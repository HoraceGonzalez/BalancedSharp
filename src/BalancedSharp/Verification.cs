using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BalancedSharp
{
    [DataContract]
    public class Verification : IBalancedServiceObject
    {
        [DataMember(Name = "attempts")]
        public int Attempts { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "remaining_attempts")]
        public int AttemptsLeft { get; set; }

        [DataMember(Name = "state")]
        public string State { get; set; }

        [DataMember(Name = "uri")]
        public string Uri { get; set; }

        public IBalancedService Service
        {
            get;
            set;
        }
    }
}
