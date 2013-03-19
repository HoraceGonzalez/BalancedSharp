using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalancedSharp.Tests.Integration
{
    [TestFixture]
    public class DebitApiTests
    {
        IBalancedService service;

        public DebitApiTests()
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
            var debit = account.Result.Debit(500);
            // status code for funds
            Assert.AreEqual(405, debit.StatusCode);
        }

        [Test]
        public void List_Success()
        {
            var result = this.service.CurrentMarketplace.Debits();
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
            var result = account.Result.Debits();
            var item = result.Result;
            Assert.IsNotNull(item.Items);
            Assert.IsNotNull(item.Limit);
            Assert.IsNotNull(item.Offset);
            Assert.IsNotNull(item.Total);
        }
    }
}
