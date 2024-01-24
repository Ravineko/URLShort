using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using URLShort.Models;

namespace URLShort.Services
{
    public interface IAuthService
    {
        Task<AuthenticationResult> AuthenticateAsync(string email, string password);
        Task<string> GenerateTokenAsync(User user);
    }
}
