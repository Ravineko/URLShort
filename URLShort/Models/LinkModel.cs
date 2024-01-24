namespace URLShort.Models
{
    public class LinkModel
    {
        public int Id { get; set; }
        public string OriginalLink { get; set; }
        public string ShortenedLink { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
