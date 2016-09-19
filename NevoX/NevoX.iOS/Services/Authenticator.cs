using System;
using System.Linq;
using Microsoft.Experimental.IdentityModel.Clients.ActiveDirectory;
using System.Threading.Tasks;
using NevoX.iOS.Services;
using NevoX.Services.Authentication;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(Authenticator))]
namespace NevoX.iOS.Services
{
    public class Authenticator : IAuthenticator
    {
        public async Task<AuthenticationResult> Authenticate(string authority, string resource, string clientId, string returnUri)
        {
            var authContext = new AuthenticationContext(authority);

            if (authContext.TokenCache.ReadItems().Any())
                authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);

            var topController = UIApplication.SharedApplication.KeyWindow.RootViewController;

            // ensures that the currently presented viewcontroller is acquired, even a modally presented one
            while (topController.PresentedViewController != null)
            {
                topController = topController.PresentedViewController;
            }

            var platformParams = new PlatformParameters(topController);

            var authResult = await authContext.AcquireTokenAsync(new []{resource }, null, clientId, new Uri(returnUri), platformParams);

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
                return true;
            }
            catch (Exception ex)
            {
                //ex.WriteFormattedMessageToDebugConsole(this);
            }
            return false;
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
               // ex.WriteFormattedMessageToDebugConsole(this);
            }
            return null;
        }
    }
}