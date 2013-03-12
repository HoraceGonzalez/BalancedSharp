using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BalancedSharp
{
    [DataContract]
    public class BankAccount : IBalancedServiceObject
    {
        public BankAccount()
        {

        }

        public BankAccount(string accountNumber, string routingNumber, 
            BankAccountType type, string name) : this()
        {
            this.AccountNumber = accountNumber;
            this.RoutingNumber = routingNumber;
            this.Type = type;
            this.Name = name;
        }

        [DataMember(Name = "account_number")]
        public string AccountNumber { get; set; }

        [DataMember(Name = "bank_name")]
        public string BankName { get; set; }

        [DataMember(Name = "can_debit")]
        public bool CanDebit { get; set; }

        [DataMember(Name = "created_at")]
        public string CreatedOn { get; set; }

        [DataMember(Name = "credits_uri")]
        public string CreditsUri { get; set; }

        [DataMember(Name = "fingerprint")]
        public string Fingerprint { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "meta")]
        public Dictionary<string, string> Meta { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "routing_number")]
        public string RoutingNumber { get; set; }

        [DataMember(Name = "type")]
        public BankAccountType Type { get; set; }

        [DataMember(Name = "uri")]
        public string Uri { get; set; }

        [DataMember(Name = "verification_uri")]
        public string VerificationUri { get; set; }

        [DataMember(Name = "verifications_uri")]
        public string VerificationsUri { get; set; }

        public Status<Credit> Credit(Credit credit)
        {
            return this.Service.Credit.CreateBank(CreditsUri, credit.Amount, credit.Description);
        }

        public Status<PagedList<Credit>> Credits(int limit = 10, int offset = 10)
        {
            return this.Service.Credit.List(CreditsUri, limit, offset);
        }

        public Status<Verification> CreateVerification()
        {
            return this.Service.Verification.Create(VerificationUri);
        }

        public Status<Verification> Verification()
        {
            return this.Service.Verification.Get(VerificationUri);
        }

        public Status<PagedList<Verification>> Verifications(int limit = 10, int offset = 10)
        {
            return this.Service.Verification.List(VerificationsUri, limit, offset);
        }

        public Status Delete()
        {
            return this.Service.BankAccount.Delete(Uri);
        }

        public IBalancedService Service
        {
            get;
            set;
        }
    }
}
