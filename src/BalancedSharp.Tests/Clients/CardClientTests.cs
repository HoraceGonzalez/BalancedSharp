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
            this.service = new BalancedService(Config.ApiKey, this.rest);
        }

        [Test]
        public void Create_Params()
        {
            string cardsUri = "https://api.balancedpayments.com/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/cards";
            string cardNumber = "5105105105105100";
            int expirationYear = 2020;
            int expirationMonth = 8;

            this.service.Card.Create(cardsUri, cardNumber, expirationYear, expirationMonth);
            Assert.AreEqual(cardNumber.ToString(), this.rest.Parameters["card_number"]);
            Assert.AreEqual(expirationYear.ToString(), this.rest.Parameters["expiration_year"]);
            Assert.AreEqual(expirationMonth.ToString(), this.rest.Parameters["expiration_month"]);
        }

        [Test]
        public void List_Params()
        {
            string cardsUri = "https://api.balancedpayments.com/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/cards";
            int limit = 10;
            int offset = 0;
            this.service.Card.List(cardsUri, limit: limit, offset: offset);
            Assert.AreEqual(limit.ToString(), this.rest.Parameters["limit"]);
            Assert.AreEqual(offset.ToString(), this.rest.Parameters["offset"]);
        }

        //[Test]
        //public void Update_Params()
        //{
        //    string cardsUri = "https://api.balancedpayments.com/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/cards/CC7tyXCnrfA6pU8DLAGu7V8k";
        //    string cardNumber = "5105105105105100";
        //    int expirationYear = 2020;
        //    int expirationMonth = 8;

        //    this.service.Card.Update(cardsUri, cardNumber, expirationYear, expirationMonth);
        //    Assert.AreEqual(cardNumber.ToString(), this.rest.Parameters["card_number"]);
        //    Assert.AreEqual(expirationYear.ToString(), this.rest.Parameters["expiration_year"]);
        //    Assert.AreEqual(expirationMonth.ToString(), this.rest.Parameters["expiration_month"]);
        //}

        [Test]
        public void Invalidate_Params()
        {
            string cardsUri = "https://api.balancedpayments.com/v1/marketplaces/TEST-MP6E3EVlPOsagSdcBNUXWBDQ/cards/CC7tyXCnrfA6pU8DLAGu7V8k";
            this.service.Card.Invalidate(cardsUri);
            Assert.AreEqual("false", this.rest.Parameters["is_valid"]);
        }
    }
}
