using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalancedSharp.Tests.Clients
{
    [TestFixture]
    public class HoldClientTests
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
        public void Create_Amount()
        {
            this.service.Hold.Create(null, 9000);
            Assert.AreEqual("9000", this.rest.Parameters["amount"]);
        }

        [Test]
        public void List_Offset()
        {
            this.service.Hold.List(null, offset: 5);
            Assert.AreEqual("5", this.rest.Parameters["offset"]);
        }

        [Test]
        public void List_Limit()
        {
            this.service.Hold.List(null, limit: 10);
            Assert.AreEqual("10", this.rest.Parameters["limit"]);
        }

        [Test]
        public void Update_IsVoid()
        {
            this.service.Hold.Update(null, isVoid: true);
            Assert.AreEqual("true", this.rest.Parameters["is_void"]);
        }

        [Test]
        public void Capture_HoldUri()
        {
            this.service.Hold.Capture("/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/holds/HLZNKOsVAfHkmmsknB4zcOi");
            Assert.AreEqual("/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/holds/HLZNKOsVAfHkmmsknB4zcOi", this.rest.Parameters["hold_uri"]);
        }

        [Test]
        public void Delete_IsVoid()
        {
            this.service.Hold.Delete(null);
            Assert.AreEqual("true", this.rest.Parameters["is_void"]);
        }
    }
}
