using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalancedSharp.Tests.Integration
{
    [TestFixture]
    public class AccountsApiTests
    {
        IBalancedService service;

        public AccountsApiTests()
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
            var result = this.service.CurrentMarketplace.CreateAccount();
            Assert.IsNotNull(result.Result);
            Assert.IsNotNull(result.Result.BankAccountsUri);
            var item = result.Result;
            Assert.IsNotNull(item);
            Assert.IsNotNull(item.BankAccountsUri);
            Assert.IsNotNull(item.CardsUri);
            Assert.IsNotNull(item.CreditsUri);
            Assert.IsNotNull(item.DebitsUri);
            Assert.IsNotNull(item.HoldsUri);
            Assert.IsNull(item.EmailAddress);
            Assert.IsNotNull(item.Id);
            Assert.IsNotNull(item.RefundsUri);
            Assert.IsNotNull(item.TransactionsUri);
            Assert.IsNotNull(item.Uri);
        }

    }
}
