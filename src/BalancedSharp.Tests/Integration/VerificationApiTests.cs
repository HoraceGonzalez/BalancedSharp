using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BalancedSharp.Tests.Integration
{
    [TestFixture]
    public class VerificationApiTests
    {
        IBalancedService service;
       

        public VerificationApiTests()
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
            var bankAccount = this.service.CurrentMarketplace.CreateBankAccount(new BankAccount()
            {
                Name = "myName",
                AccountNumber = "120923409",
                RoutingNumber = "029034080",
                Type = BankAccountType.Checking
            });
            var verification = bankAccount.Result.CreateVerification();
            Assert.IsNotNull(verification.Result);
            Assert.IsNotNull(verification.Result.Attempts);
            Assert.IsNotNull(verification.Result.AttemptsLeft);
            Assert.IsNotNull(verification.Result.Id);
            Assert.IsNotNull(verification.Result.State);
            Assert.IsNotNull(verification.Result.Uri);
        }

        [Test]
        public void Get_Success()
        {
            var result = this.service.CurrentMarketplace.CreateAccount();
            var bankAccount = this.service.CurrentMarketplace.CreateBankAccount(new BankAccount()
            {
                Name = "myName",
                AccountNumber = "120923409",
                RoutingNumber = "029034080",
                Type = BankAccountType.Checking
            });
            var verification = service.Verification.Get(bankAccount.Result.VerificationUri);
            Assert.IsNotNull(verification.Result);
        }

        [Test]
        public void List_Success()
        {
            var result = this.service.CurrentMarketplace.CreateAccount();
            var bankAccount = this.service.CurrentMarketplace.CreateBankAccount(new BankAccount()
            {
                Name = "myName",
                AccountNumber = "120923409",
                RoutingNumber = "029034080",
                Type = BankAccountType.Checking
            });
            var verifications = bankAccount.Result.Verifications(limit: 15);
            Assert.IsNotNull(verifications.Result);
        }

        [Test]
        public void Confirm_Success()
        {
            var result = this.service.CurrentMarketplace.CreateAccount();
            var bankAccount = this.service.CurrentMarketplace.CreateBankAccount(new BankAccount()
            {
                Name = "myName",
                AccountNumber = "120923409",
                RoutingNumber = "029034080",
                Type = BankAccountType.Checking
            });
            var verification = service.Verification.Get(bankAccount.Result.VerificationUri);
            var confirm = verification.Result.Confirm(1, 2);
            Assert.IsNotNull(confirm);
        }
        
    }
}
