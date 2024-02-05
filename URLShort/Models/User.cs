using Microsoft.AspNetCore.Identity;

namespace URLShort.Models
{
    public class User : IdentityUser
    {
        // Додайте інші властивості користувача, які вам потрібні
        // Наприклад, додайте власний Id, ім'я, прізвище, тощо., в залежності від ваших потреб

       
         public string DrivingLicense { get; set; }

    }
}
