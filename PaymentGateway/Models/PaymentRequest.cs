using System;

namespace PaymentGateway.Models
{
    /// <summary>
    /// A payment request
    /// </summary>
    public class PaymentRequest : IPaymentRequest
    {
        /// <summary>
        /// Create a payment request
        /// </summary>
        public PaymentRequest()
        {
        }

        /// <summary>
        /// Create a payment request
        /// </summary>
        public PaymentRequest(string cardNumber, DateTime expiryDate, decimal amount, string currencyCode, ushort cvv)
        {

            Id = Guid.NewGuid().ToString();
            CardNumber = cardNumber;
            ExpiryDate = expiryDate;
            Amount = amount;
            CurrencyCode = currencyCode;
            Cvv = cvv;
        }

        /// <summary>
        /// This identifier for this payment
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Users card number - A card number can be anywhere from 15 to 19 digits long
        /// TODO: Secure
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// The expiry date as appears on card
        /// </summary>
        public DateTime ExpiryDate { get; set; }

        /// <summary>
        /// The amount of money to be processed
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// The currency the transaction is in, for example GBP or USD
        /// </summary>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Card Verification Value as appears on card
        /// </summary>
        public ushort Cvv { get; set; }

        public override bool Equals(object obj)
        {
            return obj is PaymentRequest request &&
                   Id == request.Id &&
                   CardNumber == request.CardNumber &&
                   ExpiryDate == request.ExpiryDate &&
                   Amount == request.Amount &&
                   CurrencyCode == request.CurrencyCode &&
                   Cvv == request.Cvv;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, CardNumber, ExpiryDate, Amount, CurrencyCode, Cvv);
        }
    }
}
