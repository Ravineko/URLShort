using System.ComponentModel.DataAnnotations;

namespace URLShort.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        // Додайте інші необхідні вам властивості для реєстрації
    }
}
