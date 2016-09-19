using System.Threading.Tasks;
using Microsoft.Experimental.IdentityModel.Clients.ActiveDirectory;

namespace NevoX.Services.Authentication
{
    public interface IAuthenticator
    {
        Task<AuthenticationResult> Authenticate(string authority, string resource, string clientId, string returnUri);

        Task<bool> DeAuthenticate(string authority);

        Task<string> FetchToken(string authority);
    }
}