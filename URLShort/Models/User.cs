using Microsoft.AspNetCore.Identity;

namespace URLShort.Models
{
    public class User : IdentityUser
    {
      
       
         public string DrivingLicense { get; set; }

    }
}
