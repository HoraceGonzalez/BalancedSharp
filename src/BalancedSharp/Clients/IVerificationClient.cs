using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp.Clients
{
    public interface IVerificationClient
    {
        Status<Verification> Create(string bankAccountId);

        Status<Verification> Get(string bankAccountId, string verificationId);

        Status<PagedList<Verification>> List(string bankAccountId, int limit = 10, int offset = 0);

        Status<Verification> Confirm(string bankAccountId, string verificationId, int amount1, int amount2);
    }

    public class VerificationClient : IVerificationClient
    {
        IBalancedService balanceService;

        public VerificationClient(IBalancedService balanceService)
        {
            this.balanceService = balanceService;
        }

        public Status<Verification> Create(string bankAccountId)
        {
            throw new NotImplementedException();
        }

        public Status<Verification> Get(string bankAccountId, string verificationId)
        {
            throw new NotImplementedException();
        }

        public Status<PagedList<Verification>> List(string bankAccountId, int limit = 10, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public Status<Verification> Confirm(string bankAccountId, string verificationId, int amount1, int amount2)
        {
            throw new NotImplementedException();
        }
    }
}
