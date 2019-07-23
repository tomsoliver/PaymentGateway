using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PaymentGateway.Models;

namespace PaymentGateway.Infrastructure
{
    /// <summary>
    /// Post a payment request to a bank via http
    /// </summary>
    public class HttpBankService : IBankService
    {
        private readonly ILogger _logger;

        public HttpBankService(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Post a payment request to a bank via http
        /// </summary>
        public async Task Post(IPaymentRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
