using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalancedSharp.Tests.Clients
{
    [TestFixture]
    public class CreditClientTests
    {
        BalancedService service;
        FakeRest rest;

        [SetUp]
        public void Setup()
        {
            this.rest = new FakeRest();
            this.service = new BalancedService(Config.ApiKey, this.rest);
        }

        [Test]
        public void New_Success()
        {
            int amount = 1000;
            var bankAccount = new BankAccount()
            {
                Name = "Tony Stark",
                AccountNumber = "9900000001",
                RoutingNumber = "121000358",
                Type = BankAccountType.Checking
            };

            var credit = this.service.Credit.New(amount, bankAccount);
            Assert.AreEqual(amount, credit.Amount);
            Assert.AreEqual(bankAccount.Name, credit.BankAccount.Name);
            Assert.AreEqual(bankAccount.AccountNumber, credit.BankAccount.AccountNumber);
            Assert.AreEqual(bankAccount.RoutingNumber, credit.BankAccount.RoutingNumber);
            Assert.AreEqual(bankAccount.Type, credit.BankAccount.Type);
        }

        [Test]
        public void Save_Uri()
        {
            int amount = 1000;
            var bankAccount = new BankAccount()
            {
                Name = "Tony Stark",
                AccountNumber = "9900000001",
                RoutingNumber = "121000358",
                Type = BankAccountType.Checking
            };

            var credit = this.service.Credit.New(amount, bankAccount);
            var result = credit.Save();
            Assert.AreEqual("https://api.balancedpayments.com/v1/credits", this.rest.Uri);
        }

        [Test]
        public void Save_Params()
        {
            int amount = 1000;
            var bankAccount = new BankAccount()
            {
                Name = "Tony Stark",
                AccountNumber = "9900000001",
                RoutingNumber = "121000358",
                Type = BankAccountType.Checking
            };

            var credit = this.service.Credit.New(amount, bankAccount);
            var result = credit.Save();
            Assert.AreEqual(amount.ToString(), this.rest.Parameters["amount"]);
            Assert.AreEqual(bankAccount.Name, this.rest.Parameters["bank_account[name]"]);
            Assert.AreEqual(bankAccount.RoutingNumber, this.rest.Parameters["bank_account[routing_number]"]);
            Assert.AreEqual(bankAccount.Type.ToString().ToLower(), this.rest.Parameters["bank_account[type]"]);
        }

        [Test]
        public void Get_Uri()
        {
            this.service.Credit.Get("/v1/credits/CRfZww6HK7lPyCX5WruUx3G");
            Assert.AreEqual("https://api.balancedpayments.com/v1/credits/CRfZww6HK7lPyCX5WruUx3G", this.rest.Uri);
        }

        [Test]
        public void List_Uri()
        {
            this.service.Credit.List(limit: 10, offset: 0);
            Assert.AreEqual("https://api.balancedpayments.com/v1/credits", this.rest.Uri);
        }

        [Test]
        public void List_Params()
        {
            this.service.Credit.List(limit: 10, offset: 0);
            Assert.AreEqual("10", this.rest.Parameters["limit"]);
            Assert.AreEqual("0", this.rest.Parameters["offset"]);
        }

        [Test]
        public void ExistingBankNew_Success()
        {
            int amount = 1000;
            var bankAccount = new BankAccount()
            {
                Name = "Tony Stark",
                AccountNumber = "9900000001",
                RoutingNumber = "121000358",
                Type = BankAccountType.Checking,
                Service = this.service
            };

            var credit = bankAccount.Credit(amount);
            Assert.AreEqual(amount, credit.Amount);
            Assert.AreEqual(bankAccount.Name, credit.BankAccount.Name);
            Assert.AreEqual(bankAccount.AccountNumber, credit.BankAccount.AccountNumber);
            Assert.AreEqual(bankAccount.RoutingNumber, credit.BankAccount.RoutingNumber);
            Assert.AreEqual(bankAccount.Type, credit.BankAccount.Type);
        }

        [Test]
        public void BankAccountList_Uri()
        {
            var bankAccount = new BankAccount()
            {
                Name = "Tony Stark",
                AccountNumber = "9900000001",
                RoutingNumber = "121000358",
                Type = BankAccountType.Checking,
                Uri = "/v1/bank_accounts/BA6ThbEt9vlVXtNB1K4C9VUs",
                Service = this.service
            };

            var credis = bankAccount.Credits(limit: 10, offset: 0);
            Assert.AreEqual("https://api.balancedpayments.com/v1/bank_accounts/BA6ThbEt9vlVXtNB1K4C9VUs/credits", this.rest.Uri);
        }

        [Test]
        public void BankAccountList_Params()
        {
            var bankAccount = new BankAccount()
            {
                Name = "Tony Stark",
                AccountNumber = "9900000001",
                RoutingNumber = "121000358",
                Type = BankAccountType.Checking,
                Uri = "/v1/bank_accounts/BA6ThbEt9vlVXtNB1K4C9VUs",
                Service = this.service
            };

            var credis = bankAccount.Credits(limit: 10, offset: 0);
            Assert.AreEqual("10", this.rest.Parameters["limit"]);
            Assert.AreEqual("0", this.rest.Parameters["offset"]);
        }

        [Test]
        public void AccountList_Uri()
        {
            var account = new Account
            {
                Uri = "/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/accounts/AC3z0Z98UsRL1DERqFlb9wu",
                Service = this.service
            };

            var credits = account.Credits(limit: 10, offset: 0);
            Assert.AreEqual("https://api.balancedpayments.com/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/accounts/AC3z0Z98UsRL1DERqFlb9wu/credits", this.rest.Uri);
        }

        [Test]
        public void AccountList_Params()
        {
            var account = new Account
            {
                Uri = "/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/accounts/AC3z0Z98UsRL1DERqFlb9wu",
                Service = this.service
            };

            var credits = account.Credits(limit: 10, offset: 0);
            Assert.AreEqual("10", this.rest.Parameters["limit"]);
            Assert.AreEqual("0", this.rest.Parameters["offset"]);
        }
    }
}
