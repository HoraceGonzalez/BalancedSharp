using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BalancedSharp
{
    [DataContract]
    public class Hold : IBalancedServiceObject 
    {
        [DataMember(Name = "account")]
        public Account Account { get; set; }

        [DataMember(Name = "amount")]
        public int Amount { get; set; }

        [DataMember(Name = "created_at")]
        public DateTime CreatedOn { get; set; }

        [DataMember(Name = "debit")]
        public int Debit { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "expires_at")]
        public DateTime ExpiresOn { get; set; }

        [DataMember(Name = "fee")]
        public int Fee { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "is_void")]
        public bool IsVoid { get; set; }

        [DataMember(Name = "meta")]
        public Dictionary<string, string> Meta { get; set; }

        [DataMember(Name = "source")]
        public Card Source { get; set; }

        [DataMember(Name = "transaction_number")]
        public string TransactionNumber { get; set; }

        [DataMember(Name = "uri")]
        public string Uri { get; set; }

        public string AppearsOnStatementAs { get; set; }

        public Hold()
        {
        }

        public Hold(int amount)
        {
            this.Amount = amount;
        }

        public Status<Hold> Update()
        {
            return this.Service.Hold.Update(Uri, Description, Meta, IsVoid, AppearsOnStatementAs);
        }

        public Status<Debit> Capture()
        {
            return Capture(null, null);
        }

        public Status<Debit> Capture(string appearsOnStatementAs, string description)
        {
            return this.Service.Hold.Capture(Uri, appearsOnStatementAs, description);
        }

        public Status<Hold> Void()
        {
            return Void(null, null);
        }

        public Status<Hold> Void(string appearsOnStatementAs, string description)
        {
            return this.Service.Hold.Void(Uri, appearsOnStatementAs, description);
        }

        public IBalancedService Service
        {
            get;
            set;
        }
    }
}
