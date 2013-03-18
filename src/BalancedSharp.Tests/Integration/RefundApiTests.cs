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
        public void New_Success()
        {
            var account = this.service.CurrentMarketplace.CreateAccount();
            var card = service.CurrentMarketplace.CreateCard(new Card()
            {
                CardNumber = "0293840298340",
                ExpirationYear = 2020,
                ExpirationMonth = 10,
                SecurityCode = "234"
            });
            var debit = account.Result.CreateDebit(new Debit() {
                AppearsOnStatementAs = "Statement Text",
                Amount = 1000,
                Description = "Some description for the debit"                
            });
            var refund = debit.Result.CreateRefund(new Refund()
            {
                Description = "Refund description"
            });
            Assert.IsNotNull(refund.Result);
            Assert.IsNotNull(refund.Result.Account);
            Assert.IsNotNull(refund.Result.Amount);
            Assert.IsNotNull(refund.Result.AppearsOnStatementAs);
            Assert.IsNotNull(refund.Result.CreatedOn);
            Assert.IsNotNull(refund.Result.Debit);
            Assert.IsNotNull(refund.Result.Description);
            Assert.IsNotNull(refund.Result.Fee);
            Assert.IsNotNull(refund.Result.Id);            
            Assert.IsNotNull(refund.Result.Total);
            Assert.IsNotNull(refund.Result.TransactionNumber);
            Assert.IsNotNull(refund.Result.Uri);
        }

        [Test]
        public void Get_Success()
        {
            var account = this.service.CurrentMarketplace.CreateAccount();
            var debit = account.Result.CreateDebit(new Debit()
            {
                AppearsOnStatementAs = "Statement Text",
                Amount = 1000,
                Description = "Some description for the debit"
            });
            var refund = debit.Result.CreateRefund(new Refund()
            {
                Description = "Refund description"
            });
            var getRefund = service.Refund.Get(account.Result.RefundsUri);
            Assert.IsNotNull(getRefund.Result);
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
