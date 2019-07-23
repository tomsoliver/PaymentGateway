using System.Threading.Tasks;

namespace PaymentGateway.Security
{
    /// <summary>
    /// A service for handling users
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Authenticate via username and password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        // TODO: Implement User
        Task<string> Authenticate(string username, string password);
    }
}
