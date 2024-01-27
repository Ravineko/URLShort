using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using URLShort.Models;
using URLShort.Services;

namespace URLShort.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _authService.AuthenticateAsync(model.Email, model.Password);

            if (result.Status == AuthenticationResultStatus.Success)
            {
                // Успішна авторизація
                return Ok(new { Token = result.Token }); // Повертаємо токен або інші дані
            }
            else if (result.Status == AuthenticationResultStatus.Fail)
            {
                // Невдалий логін - повертаємо відповідне повідомлення
                return BadRequest(new { Message = result.Message });
            }
            else
            {
                // Інші випадки, якщо не Fail
                return StatusCode(500, new { Message = "An unexpected error occurred during login." });
            }
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            // Логіка реєстрації
            var registrationResult = await _authService.RegisterAsync(model.Email, model.Password);

            if (registrationResult.Success)
            {
                // Успішна реєстрація
                return Ok(new { Message = "Registration successful. You can now login." });
            }
            else
            {
                // Невдалий реєстрація - повертаємо відповідне повідомлення
                return BadRequest(new { Message = registrationResult.Message });
            }
        }

        // Інші методи контролера (реєстрація, виход і т.д.)
    }
}
