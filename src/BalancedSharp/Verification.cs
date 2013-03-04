using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BalancedSharp
{
    [DataContract]
    public class Verification
    {
        [DataMember(Name = "attempts")]
        public int Attempts { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "remaining_attempts")]
        public int RemainingAttempts { get; set; }

        [DataMember(Name = "state")]
        public string State { get; set; }

        [DataMember(Name = "uri")]
        public string Uri { get; set; }

        [DataMember(Name = "first_uri")]
        public string FirstUri { get; set; }

        //not sure about this one
        [DataMember(Name = "items")]
        public PagedList<Verification> Items { get; set; }

        [DataMember(Name = "last_uri")]
        public string LastUri { get; set; }

        [DataMember(Name = "limit")]
        public int Limit { get; set; }

        [DataMember(Name = "next_uri")]
        public string NextUri { get; set; }

        [DataMember(Name = "offset")]
        public int Offset { get; set; }

        [DataMember(Name = "previous_uri")]
        public string PreviousUri { get; set; }

        [DataMember(Name = "total")]
        public int Total { get; set; }
    }
}
