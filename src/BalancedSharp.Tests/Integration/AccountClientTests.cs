using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalancedSharp.Tests.Integration
{
    [TestFixture]
    public class AccountClientTests
    {
        BalancedService service;

        public AccountClientTests()
        {

        }

        [SetUp]
        public void Setup()
        {
            this.service = new BalancedService(Config.MarketplaceUri, Config.ApiKey);
        }

        [Test]
        public void Create_Success()
        {
            var result = this.service.Account.Create();
           
            Assert.AreEqual(200, result.StatusCode);
           
            var item = result.Result;
            Assert.IsNotNull(item);
            Assert.IsNotNull(item.BankAccountsUri);
            Assert.IsNotNull(item.CardsUri);
            Assert.IsNotNull(item.CreatedOn);
            Assert.IsNotNull(item.CreditsUri);
            Assert.IsNotNull(item.DebitsUri);
            Assert.IsNotNull(item.HoldsUri);
            Assert.IsNull(item.EmailAddress);
            Assert.IsNotNull(item.Id);
            Assert.IsNotNull(item.RefundsUri);
            Assert.IsNotNull(item.TransactionsUri);
            Assert.IsNotNull(item.Uri);
        }
        
        [Test]
        public void Add_Card_Success()
        {
            var account = this.service.Account.Create();
            Assert.Inconclusive("Not Implmented");
        }

        [Test]
        public void Add_BankAccount_Success()
        {
            Assert.Inconclusive("Not Implmented");
        }
    }
}
