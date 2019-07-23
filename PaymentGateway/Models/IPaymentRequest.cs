using System;

namespace PaymentGateway.Models
{
    /// <summary>
    /// A payment request
    /// </summary>
    public interface IPaymentRequest
    {

        /// <summary>
        /// This identifier for this payment
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Users card number - A card number can be anywhere from 15 to 19 digits long
        /// TODO: Secure
        /// </summary>
        string CardNumber { get; }

        /// <summary>
        /// The expiry date as appears on card
        /// </summary>
        DateTime ExpiryDate { get; }

        /// <summary>
        /// The amount of money to be processed
        /// </summary>
        decimal Amount { get; }

        /// <summary>
        /// The currency the transaction is in, for example GBP or USD
        /// </summary>
        string CurrencyCode { get; }

        /// <summary>
        /// Card Verification Value as appears on card
        /// </summary>
        ushort Cvv { get; }
    }
}