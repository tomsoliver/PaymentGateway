using System;
using System.Collections.Generic;
using System.Linq;
using PaymentGateway.Models;

namespace PaymentGateway.Services
{
    public class PaymentRequestValidator : IPaymentRequestValidator
    {
        /// <summary>
        /// Validates the incoming payment request.
        /// </summary>
        /// <param name="reasons">A list of reasons indicating why the request is invalid. This is 
        /// only populated when the request is invalid.</param>
        // TODO: Verify validation messages
        public bool Validate(IPaymentRequest request, out IReadOnlyCollection<string> reasons)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            // Populates the reason lists with reasons why a payment is invalid. 
            // Allows us to return all validation reasons to the merchant for them to deal with
            var reasonList = new List<string>();
            
            if (string.IsNullOrEmpty(request.CardNumber))
                reasonList.Add("Card number must be populated");
            else if (!long.TryParse(request.CardNumber, out _))
                reasonList.Add("Card number must be numeric");
            else if (request.CardNumber.Length < 15 || request.CardNumber.Length > 19)
                reasonList.Add("Card number a valid length");

            // TODO: Check if validation is performed at banks end
            if (request.ExpiryDate < DateTime.Now)
                reasonList.Add("Expiry date must be in the future");

            // TODO: Check assumption with stakeholders
            if (request.Amount <= 0)
                reasonList.Add("Payment must have a value greater than 0");

            if (string.IsNullOrWhiteSpace(request.CurrencyCode))
                reasonList.Add("Currency Code must be populated");

            var cvvLength = request.Cvv.ToString().Length;
            if (cvvLength != 3 && cvvLength != 4)
                reasonList.Add("Cvv must be populated");

            reasons = reasonList;
            return !reasonList.Any();
        }
    }
}
