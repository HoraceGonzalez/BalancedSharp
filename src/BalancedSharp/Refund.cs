using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BalancedSharp
{
    [DataContract]
    public class Refund : IBalancedServiceObject
    {
        [DataMember(Name = "account")]
        public Account Account { get; set; }

        [DataMember(Name = "amount")]
        public int Amount { get; set; }

        [DataMember(Name = "appears_on_statement_as")]
        public string AppearsOnStatementAs { get; set; }

        [DataMember(Name = "created_at")]
        public DateTime CreatedOn { get; set; }

        [DataMember(Name = "debit")]
        public Debit Debit { get; set; }

        [DataMember(Name = "fee")]
        public int Fee { get; set; }        

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "meta")]
        public Dictionary<string, string> Meta { get; set; }

        [DataMember(Name = "transaction_number")]
        public string TransactionNumber { get; set; }

        [DataMember(Name = "uri")]
        public string Uri { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "first_uri")]
        public string FirstUri { get; set; }

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

        public Status<Refund> Update()
        {
            return this.Service.Refund.Update(Uri, this.Description, this.Meta);
        }

        public IBalancedService Service
        {
            get;
            set;
        }
    }
}
