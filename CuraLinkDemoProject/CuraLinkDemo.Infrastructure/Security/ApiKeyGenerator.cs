using System.Security.Cryptography;
using System.Text;

namespace CuraLinkDemoProject.CuraLinkDemo.Infrastructure.Security
{
    public class ApiKeyGenerator
    {
        public static string GenerateApiKey()
        {
            var keyBytes = Guid.NewGuid().ToByteArray();
            return Convert.ToBase64String(keyBytes);
        }

        public static string ComputeHash(string apiKey)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(apiKey);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
