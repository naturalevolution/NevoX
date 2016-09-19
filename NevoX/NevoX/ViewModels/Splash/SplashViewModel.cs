using NevoX.ViewModels.Base;
using NevoX.Views.Authentication;
using Xamarin.Forms;

namespace NevoX.ViewModels.Splash
{
    public class SplashViewModel : BaseViewModel
    {
        private Command _newAccountButton;
        private Command _signInButton;

        public SplashViewModel()
        {
            Title = "SplashViewModel";
        }


        /// <summary>
        ///     Command to LoginPage
        /// </summary>
        public Command SignInCommand
        {
            get
            {
                return new Command(() => App.Current.MainPage = new NavigationPage(new LoginPage()));
            }
        }

        /// <summary>
        ///     Command to NewAccountPage
        /// </summary>
        public Command NewAccountCommand
        {
            get
            {
                return new Command(() => App.GoToRoot());
            }
        }
    }
}