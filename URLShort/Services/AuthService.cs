using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

using URLShort.Models;

namespace URLShort.Services
{
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
        private readonly string _jwtSecretKey = "your_jwt_secret_key"; // Замініть на власний секретний ключ
        private readonly string _jwtIssuer = "your_issuer"; // Замініть на власний ісуючий
        private readonly string _jwtAudience = "your_audience"; // Замініть на власний аудиторію

        private readonly ShortenerDbContext _dbContext;

        public AuthService(ShortenerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
     
        public async Task<AuthenticationResult> AuthenticateAsync(string email, string password)
        {
            // Логіка аутентифікації, перевірка пароля, тощо.

            // Приклад використання контексту бази даних для отримання користувача за email
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user != null && VerifyPassword(password, user.PasswordHash))
            {
                // Користувач успішно аутентифікований
                return new AuthenticationResult { Status = AuthenticationResultStatus.Success, State = user };
            }
            else
            {
                // Невдала аутентифікація
                return new AuthenticationResult { Status = AuthenticationResultStatus.Fail, Message = "Invalid email or password" };
            }
        }

        private bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            // Логіка перевірки паролю (наприклад, за допомогою бібліотеки BCrypt)
            // Повертає true, якщо пароль вірний, інакше false
            return true;
        }
        public async Task<string> GenerateTokenAsync(User user)
        {
            // Логіка генерації JWT токену
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtIssuer,
                audience: _jwtAudience,
                claims: null, // Додайте власні клейми, якщо необхідно
                expires: DateTime.UtcNow.AddHours(1), // Час життя токена
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
        // Інші методи аутентифікації, реєстрації, вихіду, тощо.
    }

}
