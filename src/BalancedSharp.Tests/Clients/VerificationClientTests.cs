using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalancedSharp.Tests.Clients
{
    [TestFixture]
    public class VerificationClientTests
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
        public void List_Limit()
        {
            this.service.Verification.List(null, limit: 7);
            Assert.AreEqual("7", this.rest.Parameters["limit"]);
        }

        [Test]
        public void List_OffSet()
        {
            this.service.Verification.List(null, offset: 8);
            Assert.AreEqual("8", this.rest.Parameters["offset"]);
        }

        [Test]
        public void Confirm_Amount1()
        {
            this.service.Verification.Confirm(null, 25, 1);
            Assert.AreEqual("25", this.rest.Parameters["amount_1"]);
            Assert.AreEqual("1", this.rest.Parameters["amount_2"]);
        }
    }
}
