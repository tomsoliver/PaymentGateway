using PaymentGateway.Models;

namespace PaymentGateway.Repository
{
    /// <summary>
    /// A CRUD style interface for accessing payment requests
    /// </summary>
    public interface IPaymentRequestRepository
    {
        /// <summary>
        /// Create a payment request
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Returns the id of the created payment</returns>
        string Create(IPaymentRequest request);

        /// <summary>
        /// Read the desired payment from the repository
        /// </summary>
        /// <param name="id">The id of the desired payment</param>
        /// <returns>Returns null if the request does not exist, or the payment request
        /// if it does</returns>
        SecuredPaymentRequest Read(string id);

        /// <summary>
        /// Update the state of the desired payment in the repository
        /// </summary>
        /// <param name="id">The id of the desired payment</param>
        /// <returns>Returns the id of the updated payment</returns>
        string UpdateResult(string id, bool result);
    }
}
