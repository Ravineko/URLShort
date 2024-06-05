using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Security.Claims;


using URLShort.Models;

namespace URLShort.Services
{
    public enum RegistrationResultStatus
    {
        Success,
        Fail
    }
    public class RegistrationResult
    {
        public RegistrationResultStatus Status { get; set; }
        public string Message { get; set; }
    }
    public enum AuthenticationResultStatus
    {
        Success,
        Redirect,
        Fail
    }
    public class AuthenticationResult
    {
        public AuthenticationResultStatus Status { get; set; }
        public string Message { get; set; }
        public object State { get; set; }
        public string Token { get; set; }
    }
    public class AuthService : IAuthService
    {
        private readonly string _jwtSecretKey = "your_jwt_secret_key"; 
        private readonly string _jwtIssuer = "your_issuer"; 
        private readonly string _jwtAudience = "your_audience"; 

        private readonly ShortenerDbContext _dbContext;
        public AuthService(ShortenerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<RegistrationResult> RegisterAsync(string email, string password, string drivingLicense, string phoneNumber)
        {
            
            var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (existingUser != null)
            {
                return new RegistrationResult 
                {
                    Status = RegistrationResultStatus.Fail, 
                    Message = "Email is already registered." 
                };
            }

            
            var newUser = new User
            {
                Email = email,
                PhoneNumber = phoneNumber,
                DrivingLicense = drivingLicense,
               
            };

            
            newUser.PasswordHash = HashPassword(password);

           
            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync();

            return new RegistrationResult
            { 
                Status = RegistrationResultStatus.Success,
                Message = "Registration successful." 
            };
        }

        private string HashPassword(string password)
        {
            // Логіка хешування паролю (використовуйте власну логіку для безпечного збереження паролів)
            // В цьому прикладі використовується простий SHA256 хеш
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
        public async Task<AuthenticationResult> AuthenticateAsync(string email, string password)
        {
           
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user != null && VerifyPassword(password, user.PasswordHash))
            {
                
                var token = await GenerateTokenAsync(user);
                return new AuthenticationResult 
                { 
                  Status = AuthenticationResultStatus.Success,
                  State = user,
                  Token = token
                };
            }
            else
            {
                
                return new AuthenticationResult 
                {
                    Status = AuthenticationResultStatus.Fail,
                    Message = "Invalid email or password"
                };
            }
        }

        private bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            string enteredPasswordHash = HashPassword(enteredPassword);

           
            return string.Equals(enteredPasswordHash, storedPasswordHash, StringComparison.OrdinalIgnoreCase);

        }
        public async Task<string> GenerateTokenAsync(User user)
        {
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtIssuer,
                audience: _jwtAudience,
                claims: null, 
                expires: DateTime.UtcNow.AddHours(1), 
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
        public async Task<AuthenticationResult> LogoutAsync()
        {
           
            return new AuthenticationResult
            {
                Status = AuthenticationResultStatus.Success,
                Token = null
            };
        }
       
    }

}
