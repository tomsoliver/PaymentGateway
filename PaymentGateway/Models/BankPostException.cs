using System;

namespace PaymentGateway.Models
{
    public class BankPostException : Exception
    {
        public BankPostException()
        {
        }

        public BankPostException(string message) : base(message)
        {
        }

        public BankPostException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
