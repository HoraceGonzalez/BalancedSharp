using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalancedSharp.Tests.Clients
{
    [TestFixture]
    public class CardClientTests
    {
        BalancedService service;
        FakeRest rest;

        [SetUp]
        public void Setup()
        {
            this.rest = new FakeRest();
            this.service = new BalancedService(
                Config.MarketplaceUri,
                Config.ApiKey, this.rest);
        }

        [Test]
        public void Tokenize_Uri()
        {
            this.service.Card.Tokenize("123456789", 2015, 7);
            Assert.AreEqual("https://api.balancedpayments.com/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/cards", this.rest.Uri);
        }

        [Test]
        public void Tokenize_CardNumber()
        {
            this.service.Card.Tokenize("987654321", 2020, 2);
            Assert.AreEqual("987654321", this.rest.Parameters["card_number"]);
        }

        [Test]
        public void Get_Uri()
        {
            this.service.Card.Get("CC7FHSFn8oRlSJSAjiBpRtLa");
            Assert.AreEqual("https://api.balancedpayments.com/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/cards/CC7FHSFn8oRlSJSAjiBpRtLa", this.rest.Uri);
        }

        [Test]
        public void List_Uri()
        {
            this.service.Card.List();
            Assert.AreEqual("https://api.balancedpayments.com/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/cards", this.rest.Uri);
        }

        [Test]
        public void List_Limit()
        {
            this.service.Card.List(limit: 25);
            Assert.AreEqual("25", this.rest.Parameters["limit"]);
        }

        [Test]
        public void List_Offset()
        {
            this.service.Card.List(offset: 5);
            Assert.AreEqual("5", this.rest.Parameters["offset"]);
        }

        [Test]
        public void Update_Uri()
        {
            this.service.Card.Update("CC7FHSFn8oRlSJSAjiBpRtLa");
            Assert.AreEqual("https://api.balancedpayments.com/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/cards/CC7FHSFn8oRlSJSAjiBpRtLa", this.rest.Uri);
        }

        [Test]
        public void Invalidate_Uri()
        {
            this.service.Card.Invalidate("CC7FHSFn8oRlSJSAjiBpRtLa");
            Assert.AreEqual("https://api.balancedpayments.com/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/cards/CC7FHSFn8oRlSJSAjiBpRtLa", this.rest.Uri);
        }

        [Test]
        public void Invalidate_IsValid()
        {
            this.service.Card.Invalidate("CC7FHSFn8oRlSJSAjiBpRtLa");
            Assert.AreEqual("false", this.rest.Parameters["is_valid"]);
        }
    }
}
