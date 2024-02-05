using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using URLShort.Models;

namespace URLShort.Services
{
    public interface IAuthService
    {
        Task<AuthenticationResult> AuthenticateAsync(string email, string password);
        Task<RegistrationResult> RegisterAsync(string email, string password, string firstName, string lastName); 
        Task<string> GenerateTokenAsync(User user);
    }
}
