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
                "/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ",
                Config.ApiKey, this.rest);
        }

        [Test]
        public void Create_Uri()
        {
            this.service.Bank.Create("Johann Bernoulli", "9900000001", "121000358", BankAccountType.Checking, null);
            Assert.AreEqual(this.rest.Uri, "https://api.balancedpayments.com/v1/bank_accounts");
        }

        [Test]
        public void Create_Params()
        {
            this.service.Bank.Create("Johann Bernoulli", "9900000001", "121000358", BankAccountType.Checking, null);
            Assert.AreEqual(this.rest.Parameters["routing_number"], "121000358");
            Assert.AreEqual(this.rest.Parameters["type"], "checking");
            Assert.AreEqual(this.rest.Parameters["name"], "Johann Bernoulli");
            Assert.AreEqual(this.rest.Parameters["account_number"], "9900000001");
        }

        [Test]
        public void Get_Uri()
        {
            this.service.Bank.Get("BA6ThbEt9vlVXtNB1K4C9VUs");
            Assert.AreEqual(this.rest.Uri, "https://api.balancedpayments.com/v1/bank_accounts/BA6ThbEt9vlVXtNB1K4C9VUs");
        }

        [Test]
        public void List_Uri()
        {
            this.service.Bank.List(limit: 10, offset: 0);
            Assert.AreEqual(this.rest.Uri, "https://api.balancedpayments.com/v1/bank_accounts");
        }

        [Test]
        public void List_Params()
        {
            this.service.Bank.List(limit: 10, offset: 0);
            Assert.AreEqual(this.rest.Parameters["limit"], "10");
            Assert.AreEqual(this.rest.Parameters["offset"], "0");
        }

        [Test]
        public void Delete_Params()
        {
            this.service.Bank.Delete("BA6EoZpQPS3SydnQ9Uya48VI");
            Assert.AreEqual(this.rest.Uri, "https://api.balancedpayments.com/v1/bank_accounts/BA6EoZpQPS3SydnQ9Uya48VI");
        }
    }
}
