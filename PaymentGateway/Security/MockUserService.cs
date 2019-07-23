using System.Security;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace PaymentGateway.Security
{
    /// <summary>
    ///  Mocks a user service. This functionality would be provided by a security service
    /// </summary>
    // TODO: Unit Test
    public class MockUserService : IUserService
    {
        public async Task<string> Authenticate(string username, string password)
        {
            // Salt would usually be retrieved from database
            byte[] salt = new byte[16];
            new RNGCryptoServiceProvider().GetBytes(salt);

            // Derive hash, and compare to stored hash in database
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            // This is for the mock user service
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                throw new SecurityException("User is not recognised");

            return username;
        }
    }
}
