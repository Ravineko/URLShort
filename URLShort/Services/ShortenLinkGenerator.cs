namespace URLShort.Services
{
    public class ShortenLinkGenerator
    {
        public static string ShortenLink(string originalLink)
        {
            byte[] bytes = Guid.NewGuid().ToByteArray();
            string uniqueId = Convert.ToBase64String(bytes)
                .Replace("/", "_")
                .Replace("+", "-")
                .Substring(0, 8);

            return uniqueId;
        }
    }
}
