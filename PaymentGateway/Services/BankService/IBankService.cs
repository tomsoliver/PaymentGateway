using PaymentGateway.Models;
using System.Threading.Tasks;

namespace PaymentGateway.Services
{
    /// <summary>
    /// Sends data to a bank
    /// </summary>
    public interface IBankService
    {
        /// <summary>
        /// Post a payment request to a bank
        /// </summary>
        Task<IBankPostResponse> Post(IPaymentRequest request);
    }
}
