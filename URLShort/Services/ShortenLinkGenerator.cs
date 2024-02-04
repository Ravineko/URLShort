using System.Security.Cryptography;
using System.Text;


namespace URLShort.Services
{
    public class ShortenLinkGenerator
    {
        public static string ShortenLink(string originalLink)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(originalLink));
                string hash = BitConverter.ToString(hashBytes).Replace("-", "").Substring(0, 8);
                return hash;
            }
        }
    }
}
