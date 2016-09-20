using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calligraphy;
using NevoX.Localization;
using NevoX.Services.Authentication;
using NevoX.ViewModels;
using NevoX.Views.Authentication;
using NevoX.Views.Base;
using NevoX.Views.Splash;
using Xamarin.Forms;
using Plugin.Connectivity;

namespace NevoX
{
    public partial class App : Application
    {
        private static Application _currentApp;

        private static Application CurrentApp
        {
            get { return _currentApp; } 
        }

        public static int AnimationSpeedFast = 250;
        public static int AnimationSpeedSlow = 3000;

        private IAuthenticationService _AuthenticationService;

        public App()
        {
            DependencyService.Register<AuthenticationDemoService>(); 

            InitializeComponent();

            TextResources.Culture = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();

            _currentApp = this;

            _AuthenticationService = DependencyService.Get<IAuthenticationService>();

            // If the App.IsAuthenticated property is false, modally present the SplashPage.
            if (!_AuthenticationService.IsAuthenticated)
            {
                MainPage = new WelcomePage();
            }
            else
            {
                GoToRoot();
            }
        }
        public static async Task ExecuteIfConnected(Func<Task> actionToExecuteIfConnected)
        {
            if (IsConnected)
            {
                await actionToExecuteIfConnected();
            }
            else
            {
                await ShowNetworkConnectionAlert();
            }
        }

        static async Task ShowNetworkConnectionAlert()
        {
            await CurrentApp.MainPage.DisplayAlert(
                TextResources.NetworkConnection_Alert_Title,
                TextResources.NetworkConnection_Alert_Message,
                TextResources.NetworkConnection_Alert_Confirm);
        }

        public static bool IsConnected
        {
            get { return true; } //CrossConnectivity.Current.IsConnected; }
        }


        public static void GoToRoot()
        { 
            CurrentApp.MainPage = new RootPage(); 
        }

        protected override void OnStart()
        {
            // Handle when your app starts 
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
