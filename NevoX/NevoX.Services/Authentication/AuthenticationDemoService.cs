using System;
using System.Threading.Tasks;

namespace NevoX.Services.Authentication
{
    public class AuthenticationDemoService : IAuthenticationService
    {
        internal class AuthenticationDemoResult
        {
            public string Email { get; set; }
        }

        private const string EmailDemo = "demo";
        private const string PasswordDemo = "demo";

        private AuthenticationDemoResult AuthenticationResult { get; set; }

         
        public async Task<bool> AuthenticateAsync(string email, string password)
        {
            //Server call simulation
            await Task.Delay(2000);

            if (!string.IsNullOrEmpty(email) && email.Equals(EmailDemo, StringComparison.CurrentCultureIgnoreCase) &&
                !string.IsNullOrEmpty(password) &&
                password.Equals(PasswordDemo, StringComparison.CurrentCultureIgnoreCase))
            {
                AuthenticationResult = new AuthenticationDemoResult
                {
                    Email = "bob@foo.com"
                };
            }

            return IsAuthenticated;
        }

        public async Task<bool> LogoutAsync()
        {
            return true;
        }

        public async Task<string> GetTokenAsync()
        {
            return "";
        }

        public bool IsAuthenticated
        {
            get { return AuthenticationResult != null; }
        }
    }
}