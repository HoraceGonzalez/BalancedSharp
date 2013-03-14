using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalancedSharp.Tests.Integration
{
    [TestFixture]
    public class MarketplaceClientTests
    {
        IBalancedService service;

        public MarketplaceClientTests() { }

        [SetUp]
        public void Setup()
        {
            this.service = new BalancedService(Config.ApiKey);
        }

        [Test]
        public void Get_Success()
        {
            var result = this.service.Marketplace.List();

            // check the result
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Result);
            Assert.IsNotNull(result.Result.Items);
            Assert.AreEqual(1, result.Result.Items.Count);
        }
    }
}
