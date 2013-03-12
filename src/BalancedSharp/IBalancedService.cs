using BalancedSharp.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp
{
    public interface IBalancedService
    {
        IAccountClient Account { get; }

        IBankAccountClient BankAccount { get; }
        
        ICardClient Card { get; }
        
        ICreditClient Credit { get; }
        
        IDebitClient Debit { get; }
        
        IEventClient Event { get; }
        
        IHoldClient Hold { get; }
        
        IRefundClient Refund { get; }
        
        IVerificationClient Verification { get; }

        IMerchantClient Merchant { get; }

        string Key { get; }

        Marketplace CurrentMarketplace { get; }

    }

    public class BalancedService : IBalancedService
    {
        IAccountClient accountClient;
        IBankAccountClient bankClient;
        ICardClient cardClient;
        IHoldClient holdClient;
        ICreditClient creditClient;
        IDebitClient debitClient;
        IRefundClient refundClient;
        IEventClient eventClient;
        IVerificationClient verificationClient;
        IMerchantClient merchantClient;
        string key;

        public BalancedService(string key, IBalancedRest rest)
        {
            this.accountClient = new AccountClient(this, rest);
            this.bankClient = new BankAccountClient(this, rest);
            this.cardClient = new CardClient(this, rest);
            this.holdClient = new HoldClient(this, rest);
            this.creditClient = new CreditClient(this, rest);
            this.debitClient = new DebitClient(this, rest);
            this.refundClient = new RefundClient(this, rest);
            this.eventClient = new EventClient(this, rest);
            this.verificationClient = new VerificationClient(this, rest);
            this.merchantClient = new MerchantClient(this, rest);
            this.key = key;
        }

        public BalancedService(string key) :
            this(key, new HttpWebRequestRest(new DcJsonBalancedSerializer())) { }

        public IAccountClient Account
        {
            get { return this.accountClient; }
        }

        public IBankAccountClient BankAccount
        {
            get { return this.bankClient; }
        }

        public ICardClient Card
        {
            get { return this.cardClient; }
        }

        public IHoldClient Hold
        {
            get { return this.holdClient; }
        }

        public ICreditClient Credit
        {
            get { return this.creditClient; }
        }

        public IDebitClient Debit
        {
            get { return this.debitClient; }
        }

        public IRefundClient Refund
        {
            get { return this.refundClient; }
        }

        public IEventClient Event
        {
            get { return this.eventClient; }
        }

        public IVerificationClient Verification
        {
            get { return this.verificationClient; }
        }

        public IMerchantClient Merchant
        {
            get { return this.merchantClient; }
        }

        public string Key
        {
            get { return this.key; }
        }

        private Merchant currentMerchant;
        private Merchant CurrentMerchant
        {
            get
            {
                if (currentMerchant == null)
                {
                    currentMerchant = this.merchantClient.List()
                        .Result.Items.First();
                }
                return currentMerchant;
            }
        }

        public Marketplace CurrentMarketplace
        {
            get { return CurrentMerchant.Marketplace; }
        }

        public Status<BankAccount> CreateBankAccount(BankAccount bankAccount)
        {
            return this.BankAccount.Create("https://api.balancedpayments.com/v1/bank_accounts",
                bankAccount.Name, bankAccount.AccountNumber, bankAccount.RoutingNumber, bankAccount.Type,
                bankAccount.Meta);
        }

        public Status<PagedList<BankAccount>> BankAccounts(int limit = 10, int offset = 10)
        {
            return this.BankAccount.List("https://api.balancedpayments.com/v1/bank_accounts", limit, offset);
        }

        public Status<PagedList<Event>> Events(int limit = 10, int offset = 10)
        {
            return this.Event.List("https://api.balancedpayments.com/v1/events", limit, offset);
        }
    }
}
