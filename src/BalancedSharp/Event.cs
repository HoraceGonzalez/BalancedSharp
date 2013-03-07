using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace BalancedSharp
{
    [DataContract]
    public class Event : IBalancedServiceObject
    {
        [DataMember(Name = "callback_statuses")]
        public CallbackStatuses CallbackStatuses { get; set; }

        [DataMember(Name = "entity")]
        public BankAccount Entity { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "occurred_at")]
        public DateTime OccurredOn { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "uri")]
        public string Uri { get; set; }

        public IBalancedService Service
        {
            get;
            set;
        }
    }
}
