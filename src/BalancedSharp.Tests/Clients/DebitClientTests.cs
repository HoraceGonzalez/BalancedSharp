using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalancedSharp.Tests.Clients
{
    [TestFixture]
    public class DebitClientTests
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
            this.service.Debit.Create(null, 9000);
            Assert.AreEqual("9000", this.rest.Parameters["amount"]);
        }

        [Test]
        public void New_Description()
        {
            this.service.Debit.Create(null, 9000, null, null, "Test description");
            Assert.AreEqual("Test description", this.rest.Parameters["description"]);
        }        

        [Test]
        public void ListAccount_Limit()
        {
            this.service.Debit.List(null, limit: 71);
            Assert.AreEqual("71", this.rest.Parameters["limit"]);
        }

        [Test]
        public void ListAccount_Offset()
        {
            this.service.Debit.List(null, offset: 49);
            Assert.AreEqual("49", this.rest.Parameters["offset"]);
        }

        [Test]
        public void Update_Description()
        {
            this.service.Debit.Update(null, null, description: "Hello I am a description");
            Assert.AreEqual("Hello I am a description", this.rest.Parameters["description"]);
        }
    }
}
