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

        public Status<PagedList<Hold>> Holds(int limit = 10, int offset = 0)
        {
            return this.Service.Hold.List(HoldsUri, limit, offset);
        }

        public Status<PagedList<Credit>> Credits(int limit = 10, int offset = 0)
        {
            return this.Service.Credit.List(CreditsUri, limit, offset);
        }

        public Status<PagedList<Debit>> Debits(int limit = 10, int offset = 0)
        {
            return this.Service.Debit.List(DebitsUri, limit, offset);
        }

        public Status<PagedList<Refund>> Refunds(int limit = 10, int offset = 0)
        {
            return this.Service.Refund.List(RefundsUri, limit, offset);
        }

        public Status<Credit> Credit(int amount, string description = null, Dictionary<string, string> meta = null,
            string appearsOnStatementAs = null, string destinationUri = null, string bankAccountUri = null)
        {
            return this.Service.Credit.CreateAccount(CreditsUri, amount, description,
                meta, appearsOnStatementAs, destinationUri, bankAccountUri);
        }

        public Status<Debit> Debit(Debit debit)
        {
            return this.Service.Debit.Create(Uri, debit.Amount, debit.AppearsOnStatementAs, debit.Meta,
                debit.Description, debit.OnBehalfOf, debit.Hold.Uri, debit.Source.Uri);
        }

        public Status<Hold> CreateHold(int amount, string appearsOnStatementAs = null, string description = null,
            Dictionary<string, string> meta = null, string sourceUri = null, string cardUri = null)
        {
            return this.Service.Hold.Create(HoldsUri, amount, Uri, appearsOnStatementAs, description,
                meta, sourceUri, cardUri);
        }

        public Status<Account> AddCard(string cardUri)
        {
            return this.Service.Account.AddCard(Uri, cardUri);
        }

        public Status<Account> AddBankAccount(string bankAccountUri)
        {
            return this.Service.Account.AddBankAccount(Uri, bankAccountUri);
        }

        public IBalancedService Service
        {
            get;
            set;
        }
    }
}
