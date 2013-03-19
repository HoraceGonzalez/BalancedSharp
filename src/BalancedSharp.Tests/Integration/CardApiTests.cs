using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalancedSharp.Tests.Integration
{
    [TestFixture]
    public class CardApiTests
    {
        IBalancedService service;

        public CardApiTests()
        {
        }

        [SetUp]
        public void Setup()
        {
            this.service = new BalancedService(Config.ApiKey);
        }

        [Test]
        public void Create_Success()
        {
            var result = this.service.CurrentMarketplace.CreateCard(
                new Card("5105105105105100", 2020, 12));
            var item = result.Result;
            Assert.IsNotNull(item);
            Assert.IsNull(item.Account);
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
            Assert.IsNull(item.Name);
            Assert.IsNotNull(item.Uri);
        }

        [Test]
        public void Get_Success()
        {
            var card = this.service.CurrentMarketplace.CreateCard(
                new Card("5105105105105100", 2020, 12));
            var result = this.service.Card.Get(card.Result.Uri);
            var item = result.Result;
            Assert.IsNotNull(item);
            Assert.IsNull(item.Account);
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
            Assert.IsNull(item.Name);
            Assert.IsNotNull(item.Uri);
        }

        [Test]
        public void List_Success()
        {
            var result = this.service.CurrentMarketplace.Cards();
            var item = result.Result;
            Assert.IsNotNull(item.Items);
            Assert.IsNotNull(item.Limit);
            Assert.IsNotNull(item.Offset);
            Assert.IsNotNull(item.Total);
        }

        [Test]
        public void Update_Success()
        {
            var card = this.service.CurrentMarketplace.CreateCard(
                new Card("5105105105105100", 2020, 12));
            card.Result.Meta = new Dictionary<string, string>
            {
                { "twitter.id", "1234987650" },
                { "facebook.user_id", "0192837465" },
                { "my-own-customer-id", "12345" }
            };
            var result = card.Result.Update();
            var item = result.Result;
            Assert.IsNotNull(item.Meta);
            Assert.AreEqual("1234987650", item.Meta["twitter.id"]);
        }

        [Test]
        public void Invalidate_Success()
        {
            var card = this.service.CurrentMarketplace.CreateCard(
                new Card("5105105105105100", 2020, 12) { IsValid = true });
            var result = card.Result.Invalidate();
            var item = result.Result;
            Assert.IsNotNull(item);
            Assert.IsNull(item.Account);
            Assert.IsNotNull(item.Brand);
            Assert.IsNotNull(item.CanDebit);
            Assert.IsNotNull(item.CardType);
            Assert.IsNotNull(item.CreatedOn);
            Assert.IsNotNull(item.ExpirationMonth);
            Assert.IsNotNull(item.ExpirationYear);
            Assert.IsNotNull(item.Hash);
            Assert.IsNotNull(item.Id);
            Assert.IsNotNull(item.IsValid);
            Assert.AreEqual(false, item.IsValid);
            Assert.IsNotNull(item.LastFourDigits);
            Assert.IsNull(item.Name);
            Assert.IsNotNull(item.Uri);
        }
    }
}
