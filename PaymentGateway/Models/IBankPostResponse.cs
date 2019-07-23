namespace PaymentGateway.Models
{
    /// <summary>
    /// The result of a post request to a bank
    /// </summary>
    public interface IBankPostResponse
    {
        /// <summary>
        /// Indicates if the post request was successful
        /// </summary>
        bool IsSuccess { get; }

        /// <summary>
        /// Provides a reason
        /// </summary>
        string ReasonPhrase { get; }

        /// <summary>
        /// A status code providing more information about the result
        /// </summary>
        int StatusCode { get; }
    }
}
