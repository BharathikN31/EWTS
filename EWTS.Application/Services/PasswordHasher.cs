using System.Security.Cryptography;
using System.Text;

namespace EWTS.Application.Services
{
    public class PasswordHasher
    {
        // 🔹 Hash Password
        public string Hash(string password)
        {
            using var sha256 = SHA256.Create();

            var bytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = sha256.ComputeHash(bytes);

            return Convert.ToBase64String(hashBytes);
        }

        // 🔹 Verify Password
        public bool Verify(string password, string storedHash)
        {
            var hashOfInput = Hash(password);
            return hashOfInput == storedHash;
        }
    }
}