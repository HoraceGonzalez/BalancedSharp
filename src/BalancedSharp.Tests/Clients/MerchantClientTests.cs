using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalancedSharp.Tests.Clients
{
    [TestFixture]
    public class MerchantClientTests
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
        public void List_Uri()
        {
            this.service.Merchant.List(limit: 10, offset: 0);
            Assert.AreEqual("https://api.balancedpayments.com/v1/bank_accounts", this.rest.Uri);
        }
    }
}
