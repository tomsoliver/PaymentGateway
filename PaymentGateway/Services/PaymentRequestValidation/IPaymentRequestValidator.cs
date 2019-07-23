using PaymentGateway.Models;
using System.Collections.Generic;

namespace PaymentGateway.Services.PaymentRequestValidation
{
    /// <summary>
    /// Validates incoming payment requests
    /// </summary>
    public interface IPaymentRequestValidator
    {
        /// <summary>
        /// Validates the incoming payment request
        /// </summary>
        /// <param name="reasons">A list of reasons indicating why the request is invalid. This is 
        /// only populated when the request is invalid.</param>
        bool Validate(IPaymentRequest request, out IReadOnlyCollection<string> reasons);
    }
}
