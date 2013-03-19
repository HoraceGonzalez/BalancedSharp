using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BalancedSharp.Tests.Integration
{
    [TestFixture]
    public class RefundApiTests
    {
        IBalancedService service;

        public RefundApiTests()
        {

        }

        [SetUp]
        public void SetUp()
        {
            this.service = new BalancedService(Config.ApiKey);
        }

        [Test]
        public void List_Success()
        {
            var refunds = service.CurrentMarketplace.Refunds();
            Assert.IsNotNull(refunds.Result);
        }

        [Test]
        public void ListByAccount_Success()
        {
            var account = this.service.CurrentMarketplace.CreateAccount();
            var refunds = account.Result.Refunds();
            Assert.IsNotNull(refunds.Result);
        }

        [Test]
        public void Update_Success()
        {
            var refund = service.Refund.Get(service.CurrentMarketplace.RefundsUri);
            refund.Result.Description = "New Refund Description";
            refund.Result.Update();
            Assert.AreEqual("New Refund Description", refund.Result.Description);
        }

    }
}
