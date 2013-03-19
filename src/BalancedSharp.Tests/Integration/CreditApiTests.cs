using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalancedSharp.Tests.Integration
{
    [TestFixture]
    public class CreditApiTests
    {
        IBalancedService service;

        public CreditApiTests()
        {
        }

        [SetUp]
        public void Setup()
        {
            this.service = new BalancedService(Config.ApiKey);
        }

        [Test]
        public void CreditNewBank_Success()
        {
            var result = service.CurrentMarketplace.CreditNewBankAccount(
                1000,
                new BankAccount()
                {
                    Name = "Johann Bernoulli",
                    AccountNumber = "9900000001",
                    RoutingNumber = "121000358",
                    Type = BankAccountType.Checking
                });
            // error code for insufficient funds
            Assert.AreEqual(409, result.StatusCode);
        }

        [Test]
        public void CreditExistingBank_Success()
        {
            var bankAccount = this.service.CurrentMarketplace.CreateBankAccount(
                new BankAccount("Johann Bernoulli", "9900000001", "121000358", BankAccountType.Checking));
            var result = bankAccount.Result.Credit(1000);
            // error code for insufficient funds
            Assert.AreEqual(409, result.StatusCode);
        }

        [Test]
        public void CreditAccount_Success()
        {
            var account = this.service.CurrentMarketplace.CreateAccount();
            var result = account.Result.Credit(1000);
            var item = result.Result;
            // error code for insufficient funds
            Assert.AreEqual(409, result.StatusCode);
        }

        [Test]
        public void List_Success()
        {
            var result = this.service.CurrentMarketplace.Credits();
            var item = result.Result;
            Assert.IsNotNull(item.Items);
            Assert.IsNotNull(item.Limit);
            Assert.IsNotNull(item.Offset);
            Assert.IsNotNull(item.Total);
        }

        [Test]
        public void ListBankAccount_Success()
        {
            var bankAccount = this.service.CurrentMarketplace.CreateBankAccount(
                new BankAccount("Johann Bernoulli", "9900000001", "121000358", BankAccountType.Checking));
            var result = bankAccount.Result.Credits();
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
            var result = account.Result.Credits();
            var item = result.Result;
            Assert.IsNotNull(item.Items);
            Assert.IsNotNull(item.Limit);
            Assert.IsNotNull(item.Offset);
            Assert.IsNotNull(item.Total);
        }
    }
}
