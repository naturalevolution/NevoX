using System.Threading.Tasks;

namespace NevoX.Services.Authentication
{
    public interface IAuthenticationService
    { 
        Task<bool> AuthenticateAsync(string email, string password);

        Task<bool> LogoutAsync();

        Task<string> GetTokenAsync();

        bool IsAuthenticated { get; } 
    }
}