using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalancedSharp.Tests.Integration
{
    [TestFixture]
    public class CardClientTests
    {
        BalancedService service;

        [SetUp]
        public void Setup()
        {
            this.service = new BalancedService(Config.MarketplaceUri, Config.ApiKey);
        }

        [Test]
        public void Tokenize_Success()
        {
            var result = this.service.Card.Tokenize("5105105105105100", 2020, 12);

            Assert.AreEqual(200, result.StatusCode);

            var item = result.Result;
            Assert.IsNotNull(item.Brand);
            Assert.IsNotNull(item.CanDebit);
            Assert.IsNotNull(item.CardType);
            Assert.IsNotNull(item.CreatedOn);
            Assert.IsNotNull(item.ExpirationMonth);
            Assert.IsNotNull(item.ExpirationYear);
            Assert.IsNotNull(item.Hash);
            Assert.IsNotNull(item.Id);
            Assert.IsNotNull(item.IsValid);
            Assert.IsNotNull(item.LastFourDigits);
            Assert.IsNotNull(item.Uri);
        }

        [Test]
        public void Get_Success()
        {
            var result = this.service.Card.Tokenize("5105105105105100", 2020, 12);
            Assert.AreEqual(200, result.StatusCode);

            //var card = this.service.Card.Get(result.Result.Id);
            //Assert.AreEqual(200, card.StatusCode);

            //var item = card.Result;
            //Assert.IsNotNull(item.Brand);
            //Assert.IsNotNull(item.CanDebit);
            //Assert.IsNotNull(item.CardType);
            //Assert.IsNotNull(item.CreatedOn);
            //Assert.IsNotNull(item.ExpirationMonth);
            //Assert.IsNotNull(item.ExpirationYear);
            //Assert.IsNotNull(item.Hash);
            //Assert.IsNotNull(item.Id);
            //Assert.IsNotNull(item.IsValid);
            //Assert.IsNotNull(item.LastFourDigits);
            //Assert.IsNotNull(item.Uri);
        }
    }
}
