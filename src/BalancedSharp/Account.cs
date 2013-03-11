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

        public string PhoneNumber { get; set; }

        public string TaxId { get; set; }

        public string DateOfBirth { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string StreetAddress { get; set; }

        public string CountryCode { get; set; }

        public Status<PagedList<Hold>> Holds(int limit = 10, int offset = 10)
        {
            return this.Service.Hold.List(HoldsUri, limit, offset);
        }

        public Status<PagedList<Credit>> Credits(int limit = 10, int offset = 10)
        {
            return this.Service.Credit.List(CreditsUri, limit, offset);
        }

        public Status<PagedList<Debit>> Debits(int limit = 10, int offset = 10)
        {
            return this.Service.Debit.List(DebitsUri, limit, offset);
        }

        public Status<PagedList<Refund>> Refunds(int limit = 10, int offset = 10)
        {
            return this.Service.Refund.List(RefundsUri, limit, offset);
        }

        public Status<Credit> Credit(Credit credit)
        {
            return this.Service.Credit.CreateAccount(CreditsUri, credit.Amount, credit.Description,
                credit.Meta, credit.AppearsOnStatementAs, credit.DestinationUri, credit.BankAccount.Uri);
        }

        public Status<Debit> Debit(Debit debit)
        {
            return this.Service.Debit.Create(Uri, debit.Amount, debit.AppearsOnStatementAs, debit.Meta,
                debit.Description, debit.OnBehalfOf, debit.Hold.Uri, debit.Source.Uri);
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
