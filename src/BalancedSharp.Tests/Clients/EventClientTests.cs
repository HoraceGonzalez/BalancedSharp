using BalancedSharp.Clients;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalancedSharp.Tests.Clients
{
    [TestFixture]
    public class EventClientTests
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
            this.service.Event.List(null, limit: 75);
            Assert.AreEqual("75", this.rest.Parameters["limit"]);
        }

        [Test]
        public void List_Offset()
        {
            this.service.Event.List(null, offset: 99);
            Assert.AreEqual("99", this.rest.Parameters["offset"]);
        }
    }
}
