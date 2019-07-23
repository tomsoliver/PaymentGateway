namespace PaymentGateway.Models
{
    public class BankPostResponse : IBankPostResponse
    {
        /// <summary>
        /// Indicates if the post request was successful
        /// </summary>
        public bool IsSuccess { set; get; }

        /// <summary>
        /// Provides a reason
        /// </summary>
        public string ReasonPhrase { set; get; }

        /// <summary>
        /// A status code providing more information about the result
        /// </summary>
        public int StatusCode { set; get; }
    }
}
