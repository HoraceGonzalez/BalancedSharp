using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalancedSharp.Tests.Integration
{
    [TestFixture]
    public class BankAccountApiTests
    {
        IBalancedService service;

        public BankAccountApiTests()
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
            var result = this.service.CurrentMarketplace.CreateBankAccount(
                new BankAccount("Johann Bernoulli", "9900000001", "121000358", BankAccountType.Checking));
            var item = result.Result;
            Assert.IsNotNull(item);
            Assert.IsNotNull(item.AccountNumber);
            Assert.IsNotNull(item.BankName);
            Assert.IsNotNull(item.CanDebit);
            Assert.IsNotNull(item.CreatedOn);
            Assert.IsNotNull(item.CreditsUri);
            Assert.IsNotNull(item.Fingerprint);
            Assert.IsNotNull(item.Id);
            Assert.IsNotNull(item.Name);
            Assert.IsNotNull(item.RoutingNumber);
            Assert.IsNotNull(item.Type);
            Assert.IsNotNull(item.Uri);
            Assert.IsNotNull(item.VerificationsUri);
        }

        [Test]
        public void Get_Success()
        {
            var bankAccount = this.service.CurrentMarketplace.CreateBankAccount(
                new BankAccount("Johann Bernoulli", "9900000001", "121000358", BankAccountType.Checking));
            var result = this.service.BankAccount.Get(bankAccount.Result.Uri);
            var item = result.Result;
            Assert.IsNotNull(item);
            Assert.IsNotNull(item.AccountNumber);
            Assert.IsNotNull(item.BankName);
            Assert.IsNotNull(item.CanDebit);
            Assert.IsNotNull(item.CreatedOn);
            Assert.IsNotNull(item.CreditsUri);
            Assert.IsNotNull(item.Fingerprint);
            Assert.IsNotNull(item.Id);
            Assert.IsNotNull(item.Name);
            Assert.IsNotNull(item.RoutingNumber);
            Assert.IsNotNull(item.Type);
            Assert.IsNotNull(item.Uri);
            Assert.IsNotNull(item.VerificationsUri);
        }

        [Test]
        public void List_Success()
        {
            var result = this.service.CurrentMarketplace.BankAccounts();
            var item = result.Result;
            Assert.IsNotNull(item.Items);
            Assert.IsNotNull(item.Limit);
            Assert.IsNotNull(item.Offset);
            Assert.IsNotNull(item.Total);
        }

        [Test]
        public void Delete_Success()
        {
            var bankAccount = this.service.CurrentMarketplace.CreateBankAccount(
                new BankAccount("Johann Bernoulli", "9900000001", "121000358", BankAccountType.Checking));
            var result = bankAccount.Result.Delete();
            Assert.IsNotNull(result.Error);
            Assert.IsNotNull(result.StatusCode);
        }
    }
}
