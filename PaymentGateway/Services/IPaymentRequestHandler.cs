using PaymentGateway.Models;
using System.Threading.Tasks;

namespace PaymentGateway.Services
{
    /// <summary>
    /// Handles a payment request
    /// </summary>
    public interface IPaymentRequestHandler
    {
        /// <summary>
        /// Handles a payment request
        /// </summary>
        /// <returns>Returns the id of the created request</returns>
        Task<string> HandleRequest(IPaymentRequest request);
    }
}
