using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace BalancedSharp
{
    [DataContract]
    public class Event
    {
        [DataMember(Name = "callback_statuses")]
        public CallbackStatus CallbackStatuses { get; set; }

        [DataMember(Name = "entity")]
        public BankAccount Entity { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "occurred_at")]
        public DateTime OccurredAt { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "uri")]
        public string Uri { get; set; }

        [DataMember(Name = "first_uri")]
        public string FirstUri { get; set; }
        
        //not sure about this one
        //its multiple "events", that contain all the above
        //members. part of the 'list all events' method 
        [DataMember(Name = "items")]
        public PagedList<Event> Items { get; set; }

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
