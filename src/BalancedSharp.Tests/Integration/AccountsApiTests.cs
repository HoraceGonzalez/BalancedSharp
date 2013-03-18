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

        [Test]
        public void AddCard_Success()
        {
            var account = this.service.CurrentMarketplace.CreateAccount();
            var card = this.service.CurrentMarketplace.CreateCard(
                new Card("5105105105105100", 2020, 12) { IsValid = true });
            var result = account.Result.AddCard(card.Result.Uri);
            var item = result.Result;
            Assert.IsNotNull(item);
            Assert.IsNotNull(item.BankAccountsUri);
            Assert.IsNotNull(item.CardsUri);
            Assert.IsNotNull(item.CreatedOn);
            Assert.IsNotNull(item.CreditsUri);
            Assert.IsNotNull(item.DebitsUri);
            Assert.IsNull(item.EmailAddress);
            Assert.IsNotNull(item.HoldsUri);
            Assert.IsNotNull(item.Id);
            Assert.IsNotNull(item.RefundsUri);
            Assert.IsNotNull(item.TransactionsUri);
            Assert.IsNotNull(item.Uri);
        }

        [Test]
        public void AddBankAccount_Success()
        {
            var account = this.service.CurrentMarketplace.CreateAccount();
            var bankAccount = this.service.CurrentMarketplace.CreateBankAccount(
                new BankAccount("Johann Bernoulli", "9900000001", "121000358", BankAccountType.Checking));
            var result = account.Result.AddBankAccount(bankAccount.Result.Uri);
            var item = result.Result;
            Assert.IsNotNull(item);
            Assert.IsNotNull(item.BankAccountsUri);
            Assert.IsNotNull(item.CardsUri);
            Assert.IsNotNull(item.CreatedOn);
            Assert.IsNotNull(item.CreditsUri);
            Assert.IsNotNull(item.DebitsUri);
            Assert.IsNull(item.EmailAddress);
            Assert.IsNotNull(item.HoldsUri);
            Assert.IsNotNull(item.Id);
            Assert.IsNotNull(item.RefundsUri);
            Assert.IsNotNull(item.TransactionsUri);
            Assert.IsNotNull(item.Uri);
        }

        [Test]
        public void UnderwriteIndividual_Success()
        {
            var person = new Person("Timmy Q. CopyPasta", "1989-12", "+14089999999", "121 Skriptkid Row", "94110");
            var result = this.service.CurrentMarketplace.UnderwriteIndividual(person);
            var item = result.Result;
            Assert.IsNotNull(item);
            Assert.IsNotNull(item.BankAccountsUri);
            Assert.IsNotNull(item.CardsUri);
            Assert.IsNotNull(item.CreatedOn);
            Assert.IsNotNull(item.CreditsUri);
            Assert.IsNotNull(item.DebitsUri);
            Assert.IsNull(item.EmailAddress);
            Assert.IsNotNull(item.HoldsUri);
            Assert.IsNotNull(item.Id);
            Assert.IsNotNull(item.RefundsUri);
            Assert.IsNotNull(item.TransactionsUri);
            Assert.IsNotNull(item.Uri);
        }

        [Test]
        public void UnderwriteMerchant_Success()
        {
            var person = new Person("Timmy Q. CopyPasta", "1989-12", "+14089999999", "121 Skriptkid Row", "94110");
            var business = new Business("Skripts4Kids", "+140899188155", "91111", "555 VoidMain Road", "211111111", person);
            var result = this.service.CurrentMarketplace.UnderwriteMerchant(business);
            var item = result.Result;
            Assert.IsNotNull(item);
            Assert.IsNotNull(item.BankAccountsUri);
            Assert.IsNotNull(item.CardsUri);
            Assert.IsNotNull(item.CreatedOn);
            Assert.IsNotNull(item.CreditsUri);
            Assert.IsNotNull(item.DebitsUri);
            Assert.IsNull(item.EmailAddress);
            Assert.IsNotNull(item.HoldsUri);
            Assert.IsNotNull(item.Id);
            Assert.IsNotNull(item.RefundsUri);
            Assert.IsNotNull(item.TransactionsUri);
            Assert.IsNotNull(item.Uri);
        }
    }
}
