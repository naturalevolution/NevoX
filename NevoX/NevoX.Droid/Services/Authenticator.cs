using System;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using NevoX.Services.Authentication;
using Microsoft.Experimental.IdentityModel.Clients.ActiveDirectory;
using NevoX.Droid.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(Authenticator))]
namespace NevoX.Droid.Services
{
    public class Authenticator : IAuthenticator
    {
        public async Task<AuthenticationResult> Authenticate(string authority, string resource, string clientId, string returnUri)
        {
            var authContext = new AuthenticationContext(authority);
            if (authContext.TokenCache.ReadItems().Any())
                authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);

            var platformParams = new PlatformParameters((Activity)Forms.Context); 

            var authResult = await authContext.AcquireTokenAsync(new[] { resource }, null, clientId, new Uri(returnUri), platformParams);
            return authResult;
        }

        public async Task<bool> DeAuthenticate(string authority)
        {
            try
            {
                var authContext = new AuthenticationContext(authority);
                await Task.Factory.StartNew(() => {
                    authContext.TokenCache.Clear();
                });
            }
            catch (Exception ex)
            {
                //ex.WriteFormattedMessageToDebugConsole(this);
                return false;
            }
            return true;
        }

        public async Task<string> FetchToken(string authority)
        {
            try
            {
                return
                    (new AuthenticationContext(authority))
                        .TokenCache
                        .ReadItems()
                        .FirstOrDefault(x => x.Authority == authority).Token;
            }
            catch (Exception ex)
            {
                //ex.WriteFormattedMessageToDebugConsole(this);
            }
            return null;
        }
    }
}