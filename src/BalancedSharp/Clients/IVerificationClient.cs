using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BalancedSharp.Clients
{
    /// <summary>
    /// Before you can debit a bank account you need to verify that you have access to it. 
    /// Balanced allows you to do this by creating a Verification for a Bank Account
    /// which will result in two random amounts being credited into the bank account. 
    /// The amounts of these two deposits must be sent back as the amount_1 and amount_2 params
    /// when subsequently updating this verification. 
    /// These deposits will appear on the bank accounts statement as Balanced verification.
    /// If you fail to verify the bank account you must create a new verification and begin 
    /// the process again. You can only create one verification at a time 
    /// and the trial deposits should show in the bank account within 2 business days.
    /// </summary>
    public interface IVerificationClient
    {
        /// <summary>
        /// Creates a new bank account verification.
        /// </summary>
        /// <param name="bankAccountUri">The bank account uri.</param>
        /// <returns>Verification details</returns>
        Status<Verification> Create(string bankAccountUri);

        /// <summary>
        /// Gets verification information for a bank account.
        /// </summary>
        /// <param name="verificationUri">The associated verification uri.</param>
        /// <returns>Verification details</returns>
        Status<Verification> Get(string verificationUri);

        /// <summary>
        /// Gets a list of all verification information for a bank account.
        /// </summary>
        /// <param name="verificationUri">The associated verification uri.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>A list of verification details</returns>
        Status<PagedList<Verification>> List(string verificationUri, int limit = 10, int offset = 0);

        /// <summary>
        /// Confirms the trial deposit amounts. For the test environment the trial deposit amounts are always 1 and 1.
        /// </summary>
        /// <param name="verificationUri">The associated verification uri.</param>
        /// <param name="amount1">The first tiral deposit amount.</param>
        /// <param name="amount2">The second trial deposit amount.</param>
        /// <returns>Verification details</returns>
        Status<Verification> Confirm(string verificationUri, int amount1, int amount2);
    }

    public class VerificationClient : IVerificationClient
    {
        IBalancedRest rest;

        public IBalancedService Service
        {
            get;
            set;
        }

        public VerificationClient(IBalancedService balanceService, IBalancedRest rest)
        {
            this.Service = balanceService;
            this.rest = rest;
        }

        public Status<Verification> Create(string bankAccountUri)
        {
            return rest.GetResult<Verification>(bankAccountUri, this.Service.Key, "", "post", null);
        }

        public Status<Verification> Get(string verificationUri)
        {
            return rest.GetResult<Verification>(verificationUri, this.Service.Key, "", "get", null);
        }

        public Status<PagedList<Verification>> List(string verificationUri, int limit = 10, int offset = 0)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("limit", limit.ToString());
            parameters.Add("offset", offset.ToString());

            return rest.GetResult<PagedList<Verification>>(verificationUri, this.Service.Key, "", "get", parameters);
        }

        public Status<Verification> Confirm(string verificationUri, int amount1, int amount2)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("amount_1", amount1.ToString());
            parameters.Add("amount_2", amount2.ToString());

            return rest.GetResult<Verification>(verificationUri, this.Service.Key, "", "put", parameters);
        }
    }
}
