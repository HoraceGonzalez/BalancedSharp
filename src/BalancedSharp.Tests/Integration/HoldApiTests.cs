using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalancedSharp.Tests.Integration
{
    [TestFixture]
    public class HoldApiTests
    {
        IBalancedService service;

        public HoldApiTests()
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
            var account = this.service.CurrentMarketplace.CreateAccount();
            var result = account.Result.CreateHold(5000);
            // status code for no funding
            Assert.AreEqual(409, result.StatusCode);
        }

        [Test]
        public void List_Success()
        {
            var result = this.service.CurrentMarketplace.Holds();
            var item = result.Result;
            Assert.IsNotNull(item.Items);
            Assert.IsNotNull(item.Limit);
            Assert.IsNotNull(item.Offset);
            Assert.IsNotNull(item.Total);
        }

        [Test]
        public void ListAccount_Success()
        {
            var account = this.service.CurrentMarketplace.CreateAccount();
            var result = account.Result.Holds();
            var item = result.Result;
            Assert.IsNotNull(item.Items);
            Assert.IsNotNull(item.Limit);
            Assert.IsNotNull(item.Offset);
            Assert.IsNotNull(item.Total);
        }
    }
}
