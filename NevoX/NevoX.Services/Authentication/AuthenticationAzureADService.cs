using System;
using System.Threading.Tasks;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.Experimental.IdentityModel.Clients.ActiveDirectory;
using Xamarin.Forms;

namespace NevoX.Services.Authentication
{
    public class AuthenticationAzureADService : IAuthenticationService
    {
        private readonly IAuthenticator _authenticator;
        private readonly IConfigFetcher _configFetcher;

        private string _azureAuthenticationClientId;
        private string _azureGraphApiClientId;
        private string _resourceUri;
        private string _returnUri;
        private string _tenantAuthority;

        public AuthenticationAzureADService()
        {
            _configFetcher = DependencyService.Get<IConfigFetcher>();

            _authenticator = DependencyService.Get<IAuthenticator>();
        }

        private AuthenticationResult AuthenticationResult { get; set; }

        public async Task<bool> AuthenticateAsync(string email, string password)
        {
            await GetConfigAsync();

            // prompts the user for authentication
            AuthenticationResult =
                await
                    _authenticator.Authenticate(_tenantAuthority, _resourceUri, _azureAuthenticationClientId, _returnUri);

            var accessToken = await GetTokenAsync();

            // instantiate an ActiveDirectoryClient to query the Graph API
            var activeDirectoryGraphApiClient = new ActiveDirectoryClient(
                new Uri(new Uri(_resourceUri), _azureGraphApiClientId),
                () => Task.FromResult(accessToken)
            );

            return IsAuthenticated;
        }

        public async Task<bool> LogoutAsync()
        {
            await GetConfigAsync();

            await Task.Factory.StartNew(async () =>
            {
                var success = await _authenticator.DeAuthenticate(_tenantAuthority);
                if (!success)
                    throw new Exception("Failed to DeAuthenticate!");
                AuthenticationResult = null;
            });
            return true;
        }

        public async Task<string> GetTokenAsync()
        {
            await GetConfigAsync();

            return await _authenticator.FetchToken(_tenantAuthority);
        }

        public bool IsAuthenticated
        {
            get { return AuthenticationResult != null; }
        }

        private async Task GetConfigAsync()
        {
            if (_azureAuthenticationClientId == null)
                _azureAuthenticationClientId =
                    await _configFetcher.GetAsync("azureActiveDirectoryAuthenticationClientId", true);

            if (_azureGraphApiClientId == null)
                _azureGraphApiClientId = await _configFetcher.GetAsync("azureActiveDirectoryGraphApiClientId", true);

            if (_tenantAuthority == null)
                _tenantAuthority = await _configFetcher.GetAsync("azureActiveDirectoryAuthenticationTenantAuthorityUrl");

            if (_returnUri == null)
                _returnUri = await _configFetcher.GetAsync("azureActiveDirectoryAuthenticationReturnUri");

            if (_resourceUri == null)
                _resourceUri = await _configFetcher.GetAsync("azureActiveDirectoryAuthenticationResourceUri");
        }
    }
}