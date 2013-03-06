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

        string BaseUri { get; }

        string Version { get; }

        string Key { get; }

        string MarketplaceUrl { get; }
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
        string marketplaceUrl;

        public BalancedService(string marketplaceUrl, string key, IBalancedRest rest)
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
            this.marketplaceUrl = marketplaceUrl;
        }

        public BalancedService(string marketplaceUrl, string key) :
            this(marketplaceUrl, key, new HttpWebRequestRest(new DcJsonBalancedSerializer())) { }

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

        public string BaseUri
        {
            get { return "https://api.balancedpayments.com"; }
        }

        public string Version
        {
            get { return "v1"; }
        }

        public string Key
        {
            get { return this.key; }
        }

        public string MarketplaceUrl
        {
            get { return this.marketplaceUrl; }
        }
    }
}
