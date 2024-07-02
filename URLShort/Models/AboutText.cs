using System.ComponentModel.DataAnnotations;

namespace URLShort.Models
{
    
    public class AboutText
    {
        [Required]
        public string Text { get; set; }
    }
}
