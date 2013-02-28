using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp.Clients
{
    public interface IVerificationClient
    {
        Status<Verification> CreateVerification(string bankAccountId);

        Status<Verification> GetVerification(string bankAccountId);

        Status<PagedList<Verification>> ListVerifications(string bankAccountId, int limit = 10, int offset = 0);

        Status<Verification> ConfirmVerification(string bankAccountId, int amount1, int amount2);
    }

    public class VerificationClient : IVerificationClient
    {
        IBalancedService balanceService;

        public VerificationClient(IBalancedService balanceService)
        {
            this.balanceService = balanceService;
        }

        public Status<Verification> CreateVerification(string bankAccountId)
        {
            throw new NotImplementedException();
        }

        public Status<Verification> GetVerification(string bankAccountId)
        {
            throw new NotImplementedException();
        }

        public Status<PagedList<Verification>> ListVerifications(string bankAccountId, int limit = 10, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public Status<Verification> ConfirmVerification(string bankAccountId, int amount1, int amount2)
        {
            throw new NotImplementedException();
        }
    }
}
