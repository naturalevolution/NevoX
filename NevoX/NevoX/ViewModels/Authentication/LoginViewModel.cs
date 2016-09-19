using System;
using System.Threading.Tasks;
using Microsoft.Experimental.IdentityModel.Clients.ActiveDirectory;
using NevoX.Localization;
using NevoX.Models.Authentication;
using NevoX.Services.Authentication;
using NevoX.Statics;
using NevoX.ViewModels.Base;
using NevoX.Views.Splash;
using Xamarin.Forms;

namespace NevoX.ViewModels.Authentication
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IAuthenticationService _authenticationService; 

        private bool _isConnecting;
        private LoginModel _loginModel;
        private string _message;

        public LoginViewModel()
        {
            Title = "LoginViewModel";
            LoginModel = new LoginModel();
            _authenticationService = DependencyService.Get<IAuthenticationService>();
        }

        public LoginModel LoginModel
        {
            get { return _loginModel; }
            set
            {
                _loginModel = value;
                OnPropertyChanged("LoginModel");
            }
        }

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged("Message");
            }
        }

        public Command SaveCommand
        {
            get
            {
                return new Command(async () =>
                {
                    Message = TextResources.Authentication_Message_InProgress;

                    await App.ExecuteIfConnected(async () =>
                    {
                        // trigger the activity indicator through the IsConnecting property on the ViewModel
                        IsConnecting = true;

                        if (await Authenticate())
                        {
                            // Pop off the modally presented SplashPage.
                            // Note that we're not popping the ADAL auth UI here; that's done automatcially by the ADAL library when the Authenticate() method returns.
                            App.GoToRoot();

                            // Broadcast a message that we have sucessdully authenticated.
                            // This is mostly just for Android. We need to trigger Android to call the SalesDashboardPage.OnAppearing() method,
                            // because unlike iOS, Android does not call the OnAppearing() method each time that the Page actually appears on screen.
                            MessagingCenter.Send(this, MessagingServiceConstants.AUTHENTICATED);

                            Message = "Bienvenue, " + LoginModel.Email;
                        }
                        else
                        {
                            Message = TextResources.Authentication_Alert_Credential;
                            IsConnecting = false;
                        }
                    });
                });
            }
        }

        public Command CancelCommand
        {
            get
            {
                return new Command(() =>
                { 
                    IsConnecting = false;
                    App.Current.MainPage = new NavigationPage(new SplashPage());
                });
            }
        }


        public bool IsConnecting
        {
            get { return _isConnecting; }
            set
            {
                _isConnecting = value;
                OnPropertyChanged("IsConnecting");
            }
        }

        private async Task<bool> Authenticate()
        {
            var success = false;
            try
            {
                // The underlying call behind App.Authenticate() calls the ADAL library, which presents the login UI and awaits success.
                success = await _authenticationService.AuthenticateAsync(LoginModel.Email, LoginModel.Password);
            }
            catch (Exception ex)
            {
                if (ex is AdalException && ((ex as AdalException).ErrorCode == "authentication_canceled"))
                {
                    // Do nothing, just duck the exception. The user just cancelled out of the login UI.
                }
                else
                {
                    Message = TextResources.Authentication_Alert_Network;
                } 
            }
            finally
            {
                // When the App.Authenticate() returns, the login UI is hidden, regardless of success (for example, if the user taps "Cancel" in iOS).
                // This means the SplashPage will be visible again, so we need to make the sign in button clickable again by hiding the activity indicator (via the IsPresentingLoginUI property).
                IsConnecting = false;
            }

            return success;
        }
    }
}