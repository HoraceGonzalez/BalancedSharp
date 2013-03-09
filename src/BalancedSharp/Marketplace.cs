using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BalancedSharp
{
    public class Marketplace : IBalancedServiceObject
    {
        [DataMember(Name = "in_escrow")]
        public float InEscrow { get; set; }

        [DataMember(Name = "owner_account")]
        public Account OwnerAccount { get; set; }

        [DataMember(Name = "callbacks_uri")]
        public string CallBacksUri { get; set; }

        [DataMember(Name = "domain_url")]
        public string DomainUrl { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "transactions_uri")]
        public string TransactionsUri { get; set; }

        [DataMember(Name = "support_email_address")]
        public string SupportEmailAddress { get; set; }

        [DataMember(Name = "uri")]
        public string Uri { get; set; }

        [DataMember(Name = "events_uri")]
        public string EventsUri { get; set; }

        [DataMember(Name = "accounts_uri")]
        public string AccountsUri { get; set; }

        [DataMember(Name = "support_phone_number")]
        public string SupportPhoneNumber { get; set; }

        [DataMember(Name = "refunds_uri")]
        public string RefundsUri { get; set; }

        [DataMember(Name = "meta")]
        public Dictionary<string, string> Meta { get; set; }

        [DataMember(Name = "debits_uri")]
        public string DebitsUri { get; set; }

        [DataMember(Name = "holds_uri")]
        public string HoldsUri { get; set; }

        [DataMember(Name = "bank_accounts_uri")]
        public string BackAccountsUri { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "credits_uri")]
        public string CreditsUri { get; set; }

        [DataMember(Name = "cards_uri")]
        public string CardsUri { get; set; }

        public Status<Credit> Credit(Credit credit)
        {
            return null;
        }

        public Status<PagedList<Credit>> Credits(int limit = 10, int offset = 10)
        {
            return null;
        }

        public Status<PagedList<Debit>> Debits(int limit = 10, int offset = 10)
        {
            return null;
        }

        public Status<PagedList<Hold>> Holds(int limit = 10, int offest = 10)
        {
            return null;
        }

        public Status<PagedList<Refund>> Refunds(int limit = 10, int offest = 10)
        {
            return null;
        }

        public Status<PagedList<Event>> Events(int limit = 10, int offest = 10)
        {
            return null;
        }

        public Status<BankAccount> BankAccount(BankAccount bankAccount)
        {
            return null;
        }

        public Status<PagedList<BankAccount>> BankAccounts(int limit = 10, int offest = 10)
        {
            return null;
        }

        public Status<Account> Account(Account account)
        {
            return null;
        }

        public Status<Account> UnderwriteIndividual(Account account)
        {
            return null;
        }

        public Status<Account> UnderwriteMerchant(Account account)
        {
            return null;
        }

        public IBalancedService Service
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
