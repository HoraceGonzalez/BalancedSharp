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
        public string BankAccountsUri { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "credits_uri")]
        public string CreditsUri { get; set; }

        [DataMember(Name = "cards_uri")]
        public string CardsUri { get; set; }

        public Status<PagedList<BankAccount>> BankAccounts(int limit = 10, int offset = 10)
        {
            return this.Service.BankAccount.List(BankAccountsUri, limit, offset);
        }

        public Status<PagedList<Card>> Cards(int limit = 10, int offset = 0)
        {
            return this.Service.Card.List(CardsUri, limit, offset);
        }

        public Status<PagedList<Credit>> Credits(int limit = 10, int offset = 10)
        {
            return this.Service.Credit.List(CreditsUri, limit, offset);
        }

        public Status<PagedList<Debit>> Debits(int limit = 10, int offset = 10)
        {
            return this.Service.Debit.List(DebitsUri, limit, offset);
        }

        public Status<PagedList<Hold>> Holds(int limit = 10, int offset = 10)
        {
            return this.Service.Hold.List(HoldsUri, limit, offset);
        }

        public Status<PagedList<Refund>> Refunds(int limit = 10, int offset = 10)
        {
            return this.Service.Refund.List(RefundsUri, limit, offset);
        }

        public Status<PagedList<Event>> Events(int limit = 10, int offset = 10)
        {
            return this.Service.Event.List(EventsUri, limit, offset);
        }

        public Status<BankAccount> CreateBankAccount(BankAccount bankAccount)
        {
            if (string.IsNullOrWhiteSpace(bankAccount.Name))
                throw new ArgumentNullException("BankAccount.Name");
            if (string.IsNullOrWhiteSpace(bankAccount.AccountNumber))
                throw new ArgumentNullException("BankAccount.AccountNumber");
            if (string.IsNullOrWhiteSpace(bankAccount.RoutingNumber))
                throw new ArgumentNullException("BankAccount.RoutingNumber");
            return this.Service.BankAccount.Create(BankAccountsUri, bankAccount.Name,
                bankAccount.AccountNumber, bankAccount.RoutingNumber, bankAccount.Type, bankAccount.Meta);
        }

        public Status<Account> CreateAccount()
        {
            return this.Service.Account.Create(AccountsUri);
        }

        public Status<Account> UnderwriteIndividual(Account account)
        {
            if (string.IsNullOrWhiteSpace(account.PhoneNumber))
                throw new ArgumentNullException("Account.PhoneNumber");
            return this.Service.Account.UnderwriteAsIndividual(AccountsUri, account.PhoneNumber,
                account.EmailAddress, account.Meta, account.TaxId, account.DateOfBirth, account.Name,
                account.City, account.PostalCode, account.StreetAddress, account.CountryCode);
        }

        public Status<Account> UnderwriteMerchant(Account account)
        {
            return null;
        }

        public Status<Card> CreateCard(Card card)
        {
            if (string.IsNullOrWhiteSpace(card.CardNumber))
                throw new ArgumentNullException("Card.CardNumber");
            return this.Service.Card.Create(CardsUri, card.CardNumber, card.ExpirationYear, card.ExpirationMonth,
                card.SecurityCode, card.Name, card.PhoneNumber, card.City, card.PostalCode, card.StreetAddress,
                card.CountryCode, card.Meta, card.IsValid);
        }

        public Status<Credit> Credit(Credit credit)
        {
            if (string.IsNullOrWhiteSpace(credit.BankAccount.Name))
                throw new ArgumentNullException("Credit.BankAccount.Name");
            if (string.IsNullOrWhiteSpace(credit.BankAccount.AccountNumber))
                throw new ArgumentNullException("Credit.BankAccount.AccountNumber");
            if (string.IsNullOrWhiteSpace(credit.BankAccount.RoutingNumber))
                throw new ArgumentNullException("Credit.BankAccount.RoutingNumber");
            return this.Service.Credit.CreateNewBank(CreditsUri, credit.Amount, credit.BankAccount.Name,
                credit.BankAccount.AccountNumber, credit.BankAccount.RoutingNumber, credit.BankAccount.Type.ToString().ToLower(),
                credit.Meta, credit.Description);
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
