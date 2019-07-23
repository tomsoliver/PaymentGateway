using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PaymentGateway.Models;

namespace PaymentGateway.Infrastructure
{
    /// <summary>
    /// A mock bank service for the purpose of testing
    /// </summary>
    public class MockBankService : IBankService
    {
        private readonly ILogger _logger;

        public MockBankService(ILogger logger)
        {
            _logger = logger;
            _logger?.LogTrace($"Creating a {nameof(MockBankService)}");
        }

        public bool Success { get; set; }

        /// <summary>
        /// Post a payment request to a bank via http
        /// </summary>
        public async Task Post(IPaymentRequest request)
        {
            _logger?.LogTrace($"Simulating post to a bank for request {request.Id}");

            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (!Success) throw new BankPostException("Posting to bank failed");
        }
    }
}
