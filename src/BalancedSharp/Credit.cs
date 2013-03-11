using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BalancedSharp
{
    [DataContract]
    public class Credit : IBalancedServiceObject
    {
        [DataMember(Name = "amount")]
        public int Amount { get; set; }

        [DataMember(Name = "bank_account")]
        public BankAccount BankAccount { get; set; }

        [DataMember(Name = "created_at")]
        public DateTime CreatedOn { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "meta")]
        public Dictionary<string, string> Meta { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "uri")]
        public string Uri { get; set; }

        public string AppearsOnStatementAs { get; set; }

        public string DestinationUri { get; set; }

        public IBalancedService Service
        {
            get;
            set;
        }
    }
}
