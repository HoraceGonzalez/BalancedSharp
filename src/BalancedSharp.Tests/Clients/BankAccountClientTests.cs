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
            this.service = new BalancedService(
                "/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ",
                Config.ApiKey, this.rest);
        }

        [Test]
        public void Create_Uri()
        {
            this.service.Bank.Create("Johann Bernoulli", "9900000001", "121000358", BankAccountType.Checking, null);
            Assert.AreEqual("https://api.balancedpayments.com/v1/bank_accounts", this.rest.Uri);
        }

        [Test]
        public void Create_Params()
        {
            this.service.Bank.Create("Johann Bernoulli", "9900000001", "121000358", BankAccountType.Checking, null);
            Assert.AreEqual("121000358", this.rest.Parameters["routing_number"]);
            Assert.AreEqual("checking", this.rest.Parameters["type"]);
            Assert.AreEqual("Johann Bernoulli", this.rest.Parameters["name"]);
            Assert.AreEqual("9900000001", this.rest.Parameters["account_number"]);
        }

        [Test]
        public void Get_Uri()
        {
            this.service.Bank.Get("BA6ThbEt9vlVXtNB1K4C9VUs");
            Assert.AreEqual("https://api.balancedpayments.com/v1/bank_accounts/BA6ThbEt9vlVXtNB1K4C9VUs", this.rest.Uri);
        }

        [Test]
        public void List_Uri()
        {
            this.service.Bank.List(limit: 10, offset: 0);
            Assert.AreEqual("https://api.balancedpayments.com/v1/bank_accounts", this.rest.Uri);
        }

        [Test]
        public void List_Params()
        {
            this.service.Bank.List(limit: 10, offset: 0);
            Assert.AreEqual("10", this.rest.Parameters["limit"]);
            Assert.AreEqual("0", this.rest.Parameters["offset"]);
        }

        [Test]
        public void Delete_Params()
        {
            this.service.Bank.Delete("BA6EoZpQPS3SydnQ9Uya48VI");
            Assert.AreEqual("https://api.balancedpayments.com/v1/bank_accounts/BA6EoZpQPS3SydnQ9Uya48VI", this.rest.Uri);
        }
    }
}
