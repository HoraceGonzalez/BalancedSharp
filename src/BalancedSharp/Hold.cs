﻿using System;
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

        public Status<Hold> Update(string description)
        {
            if (string.IsNullOrEmpty(Uri))
                throw new ArgumentException("Null or Empty", "Uri");
            if (string.IsNullOrEmpty(description))
                throw new ArgumentException("Null or Empty", "description");
            return this.Service.Hold.Update(Uri, description);
        }

        public Status<Debit> Capture()
        {
            if (string.IsNullOrEmpty(Uri))
                throw new ArgumentException("Null or Empty", "Uri");
            return this.Service.Hold.Capture(Uri);
        }

        public Status<Hold> Void()
        {
            if (string.IsNullOrEmpty(Uri))
                throw new ArgumentException("Null or Empty", "Uri");
            return this.Service.Hold.Delete(Uri);
        }

        public IBalancedService Service
        {
            get;
            set;
        }
    }
}
