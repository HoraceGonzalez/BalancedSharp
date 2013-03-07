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
                "/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ",
                Config.ApiKey, this.rest);
        }

        [Test]
        public void New_Success()
        {
            string cardUri = "/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/cards";
            string cardNumber = "5105105105105100";
            int expYear = 2020;
            int expMonth = 12;

            var card = service.Card.New(
                cardUri,
                cardNumber,
                expYear,
                expMonth
            );
            Assert.AreEqual(cardUri, card.Uri);
            Assert.AreEqual(cardNumber, card.CardNumber);
            Assert.AreEqual(expYear, card.ExpirationYear);
            Assert.AreEqual(expMonth, card.ExpirationMonth);
        }

        [Test]
        public void Save_Uri()
        {
            string cardUri = "/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/cards";
            string cardNumber = "5105105105105100";
            int expYear = 2020;
            int expMonth = 12;

            var card = service.Card.New(
                cardUri,
                cardNumber,
                expYear,
                expMonth
            );
            var result = card.Save();
            Assert.AreEqual("https://api.balancedpayments.com/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/cards", this.rest.Uri);
        }

        [Test]
        public void Save_Params()
        {
            string cardUri = "/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/cards";
            string cardNumber = "5105105105105100";
            int expYear = 2020;
            int expMonth = 12;

            var card = service.Card.New(
                cardUri,
                cardNumber,
                expYear,
                expMonth
            );
            var result = card.Save();
            Assert.AreEqual(cardNumber, this.rest.Parameters["card_number"]);
            Assert.AreEqual(expYear.ToString(), this.rest.Parameters["expiration_year"]);
            Assert.AreEqual(expMonth.ToString(), this.rest.Parameters["expiration_month"]);
        }

        [Test]
        public void Save_RequestMethod()
        {
            string cardUri = "/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/cards";
            string cardNumber = "5105105105105100";
            int expYear = 2020;
            int expMonth = 12;

            var card = service.Card.New(
                cardUri,
                cardNumber,
                expYear,
                expMonth
            );
            card.Save();
            Assert.AreEqual("post", this.rest.Method);

            cardUri = "/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/cards/CC7tyXCnrfA6pU8DLAGu7V8k";
            card = service.Card.New(
                cardUri,
                cardNumber,
                expYear,
                expMonth
            );
            card.Save();
            Assert.AreEqual("put", this.rest.Method);
        }

        [Test]
        public void Get_Uri()
        {
            this.service.Card.Get("/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/cards/CC7tyXCnrfA6pU8DLAGu7V8k");
            Assert.AreEqual("https://api.balancedpayments.com/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/cards/CC7tyXCnrfA6pU8DLAGu7V8k", this.rest.Uri);
        }

        [Test]
        public void List_Uri()
        {
            this.service.Card.List(limit: 10, offset: 0);
            Assert.AreEqual("https://api.balancedpayments.com/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/cards", this.rest.Uri);
        }

        [Test]
        public void List_Params()
        {
            this.service.Card.List(limit: 10, offset: 0);
            Assert.AreEqual("10", this.rest.Parameters["limit"]);
            Assert.AreEqual("0", this.rest.Parameters["offset"]);
        }

        [Test]
        public void Invalidate_Uri()
        {
            string cardUri = "/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/cards/CC7tyXCnrfA6pU8DLAGu7V8k";
            string cardNumber = "5105105105105100";
            int expYear = 2020;
            int expMonth = 12;

            var card = service.Card.New(
                cardUri,
                cardNumber,
                expYear,
                expMonth
            );
            card.Invalidate().Save();
            Assert.AreEqual("https://api.balancedpayments.com/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/cards/CC7tyXCnrfA6pU8DLAGu7V8k", this.rest.Uri);
        }

        [Test]
        public void Invalidate_Params()
        {
            string cardUri = "/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/cards/CC7tyXCnrfA6pU8DLAGu7V8k";
            string cardNumber = "5105105105105100";
            int expYear = 2020;
            int expMonth = 12;

            var card = service.Card.New(
                cardUri,
                cardNumber,
                expYear,
                expMonth
            );
            card.Invalidate().Save();
            Assert.AreEqual("false", this.rest.Parameters["is_valid"]);
        }
    }
}
