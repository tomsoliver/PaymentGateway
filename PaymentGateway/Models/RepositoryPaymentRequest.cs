using System;
using System.Linq;

namespace PaymentGateway.Models
{
    /// <summary>
    /// A model for storing payment requests in a repository
    /// </summary>
    public class SecuredPaymentRequest
    {
        /// <summary>
        /// A model for storing payment requests in a repository
        /// </summary>
        public SecuredPaymentRequest(IPaymentRequest request)
        {
            Id = request.Id;
            ExpiryDate = request.ExpiryDate;
            Amount = request.Amount;
            CurrencyCode = request.CurrencyCode;
            Cvv = "***";
            RequestTime = request.RequestTime;

            // Take only the last 4 digits, the rest will be stars
            var digitsToSkip = request.CardNumber.Length - 4;
            var stars = string.Join(string.Empty, Enumerable.Range(0, digitsToSkip).Select(s => "*"));
            var last4CardNumbers = string.Join(string.Empty, request.CardNumber.Skip(digitsToSkip).Take(4));
            CardNumber = stars + last4CardNumbers;
        }

        /// <summary>
        /// This identifier for this payment
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Users card number - A card number can be anywhere from 15 to 19 digits long
        /// TODO: Secure
        /// </summary>
        public string CardNumber { get; }

        /// <summary>
        /// The expiry date as appears on card
        /// </summary>
        public DateTime ExpiryDate { get; }

        /// <summary>
        /// The amount of money to be processed
        /// </summary>
        public decimal Amount { get; }

        /// <summary>
        /// The currency the transaction is in, for example GBP or USD
        /// </summary>
        public string CurrencyCode { get; }

        /// <summary>
        /// Card Verification Value as appears on card
        /// </summary>
        public string Cvv { get; }

        /// <summary>
        /// The time the request was made
        /// </summary>
        public DateTime RequestTime { get; }

        /// <summary>
        /// The result of the transacion
        /// </summary>
        /// TODO: Update to enum with results like invalid, failed...
        public bool Result { get; set; }
    }
}
