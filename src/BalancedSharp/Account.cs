using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BalancedSharp
{
    [DataContract]
    public class Account : IBalancedServiceObject
    {
        [DataMember(Name = "bank_accounts_uri")]
        public string BankAccountsUri { get; set; }

        [DataMember(Name = "cards_uri")]
        public string CardsUri { get; set; }

        [DataMember(Name = "created_at")]
        public string CreatedOn { get; set; }

        [DataMember(Name = "credits_uri")]
        public string CreditsUri { get; set; }

        [DataMember(Name = "debits_uri")]
        public string DebitsUri { get; set; }

        [DataMember(Name = "email_address")]
        public string EmailAddress { get; set; }

        [DataMember(Name = "holds_uri")]
        public string HoldsUri { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "meta")]
        public Dictionary<string, string> Meta { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "refunds_uri")]
        public string RefundsUri { get; set; }

        [DataMember(Name = "roles")]
        public string[] Roles { get; set; }

        [DataMember(Name = "transactions_uri")]
        public string TransactionsUri { get; set; }

        [DataMember(Name = "uri")]
        public string Uri { get; set; }

        public Status<PagedList<Credit>> Credits(int limit = 10, int offset = 0)
        {
            return this.Service.Credit.List(this.Uri, limit, offset);
        }

        //public Status<Debit> Debit(int? amount = null, string appearsOnStatementAs = null, Dictionary<string, string> meta = null, string description = null,
        //    string onBehalfOfUri = null, string holdUri = null, string sourceUri = null)
        //{
        //    return this.Service.Debit.New(this.Uri, amount, appearsOnStatementAs, meta, description,
        //        onBehalfOfUri, holdUri, sourceUri);
        //}

        //public Status<PagedList<Debit>> List(int limit = 10, int offset = 0)
        //{
        //    return this.Service.Debit.List(this.Uri, limit, offset);
        //}
        
        public IBalancedService Service
        {
            get;
            set;
        }
    }
}
