using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalancedSharp.Tests.Clients
{
    [TestFixture]
    public class RefundClientTest
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
        public void New_Amount()
        {
            this.service.Refund.Create(null, amount:100);
            Assert.AreEqual("100", this.rest.Parameters["amount"]);
        }

        [Test]
        public void New_Description()
        {
            this.service.Refund.Create(null, description:"This is a test");
            Assert.AreEqual("This is a test", this.rest.Parameters["description"]);
        }

        [Test]
        public void List_Limit()
        {
            this.service.Refund.List(null, limit:5);
            Assert.AreEqual("5", this.rest.Parameters["limit"]);
        }

        [Test]
        public void List_Offset()
        {
            this.service.Refund.List(null, offset:10);
            Assert.AreEqual("10", this.rest.Parameters["offset"]);
        }

        [Test]
        public void Update_Description()
        {
            this.service.Refund.Update(null, description:"this is a test");
            Assert.AreEqual("this is a test", this.rest.Parameters["description"]);
        }
    }
}
