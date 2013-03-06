using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalancedSharp.Tests.Clients
{
    [TestFixture]
    public class BankAccountClientTests
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
            string name = "Johann Bernoulli";
            string accountNumber = "9900000001";
            string routingNumber = "121000358";
            BankAccountType type = BankAccountType.Checking;

            var account = this.service.BankAccount.New(name, accountNumber, routingNumber, type);
            Assert.AreEqual(name, account.Name);
            Assert.AreEqual(accountNumber, account.AccountNumber);
            Assert.AreEqual(routingNumber, account.RoutingNumber);
            Assert.AreEqual(type, account.Type);
            Assert.AreEqual(this.service, account.Service);
        }

        [Test]
        public void Save_Uri()
        {
            string name = "Johann Bernoulli";
            string accountNumber = "9900000001";
            string routingNumber = "121000358";
            BankAccountType type = BankAccountType.Checking;

            var account = this.service.BankAccount.New(name, accountNumber, routingNumber, type);
            var result = account.Save();
            Assert.AreEqual("https://api.balancedpayments.com/v1/bank_accounts", this.rest.Uri);
        }

        [Test]
        public void Save_Params()
        {
            string name = "Johann Bernoulli";
            string accountNumber = "9900000001";
            string routingNumber = "121000358";
            BankAccountType type = BankAccountType.Checking;

            var account = this.service.BankAccount.New(name, accountNumber, routingNumber, type);
            var result = account.Save();
            Assert.AreEqual(routingNumber, this.rest.Parameters["routing_number"]);
            Assert.AreEqual("checking", this.rest.Parameters["type"]);
            Assert.AreEqual(name, this.rest.Parameters["name"]);
            Assert.AreEqual(accountNumber, this.rest.Parameters["account_number"]);
        }

        [Test]
        public void Get_Uri()
        {
            this.service.BankAccount.Get("/v1/bank_accounts/BA6ThbEt9vlVXtNB1K4C9VUs");
            Assert.AreEqual("https://api.balancedpayments.com/v1/bank_accounts/BA6ThbEt9vlVXtNB1K4C9VUs", this.rest.Uri);
        }

        [Test]
        public void List_Uri()
        {
            this.service.BankAccount.List(limit: 10, offset: 0);
            Assert.AreEqual("https://api.balancedpayments.com/v1/bank_accounts", this.rest.Uri);
        }

        [Test]
        public void List_Params()
        {
            this.service.BankAccount.List(limit: 10, offset: 0);
            Assert.AreEqual("10", this.rest.Parameters["limit"]);
            Assert.AreEqual("0", this.rest.Parameters["offset"]);
        }

        [Test]
        public void Delete_Uri()
        {
            BankAccount account = new BankAccount()
            {
                Uri = "/v1/bank_accounts/BA6EoZpQPS3SydnQ9Uya48VI",
                Service = this.service
            };
            account.Delete();
            Assert.AreEqual("https://api.balancedpayments.com/v1/bank_accounts/BA6EoZpQPS3SydnQ9Uya48VI", this.rest.Uri);
        }
    }
}
