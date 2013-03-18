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
            var result = this.service.CurrentMarketplace.CreateAccount();
            var bankAccount = this.service.CurrentMarketplace.CreateBankAccount(new BankAccount()
            {
                Name = "myName",
                AccountNumber = "120923409",
                RoutingNumber = "029034080",
                Type = BankAccountType.Checking
            });
            var account = service.BankAccount.Get(result.Result.BankAccountsUri);
            var verification = account.Result.CreateVerification();
            Assert.IsNotNull(verification.Result);
        }
        
    }
}
