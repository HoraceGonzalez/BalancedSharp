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
            this.service.Bank.Create("Johann Bernoulli", "9900000001", "121000358", BankAccountType.Checking, null);
        }

        [Test]
        public void Create_Uri()
        {
            Assert.AreEqual(this.rest.Uri, "https://api.balancedpayments.com/v1/bank_accounts");
        }

        [Test]
        public void Create_Params()
        {
            Assert.AreEqual(this.rest.Parameters["routing_number"], "121000358");
            Assert.AreEqual(this.rest.Parameters["type"], "checking");
            Assert.AreEqual(this.rest.Parameters["name"], "Johann Bernoulli");
            Assert.AreEqual(this.rest.Parameters["account_number"], "9900000001");
        }
    }
}
