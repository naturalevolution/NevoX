using System.Threading.Tasks;
using NevoX.Services.Authentication;
using NevoX.ViewModels.Splash;
using NevoX.Views.Authentication;
using NevoX.Views.Base;
using NevoX.Views.Home;
using Xamarin.Forms;

namespace NevoX.Views.Splash
{
    /// <summary>
    ///     This class definition just gives us a way to reference ModelBoundContentPage<T> in the XAML of this Page.
    /// </summary>
    public abstract class SplashPageXaml : ModelBoundContentPage<SplashViewModel> {}

    public partial class SplashPage : SplashPageXaml
    { 
        public SplashPage()
        {
            InitializeComponent();

            BindingContext = new SplashViewModel(); 

            SignInButton.Command = ViewModel.SignInCommand;
            NewAccountButton.Command = ViewModel.NewAccountCommand;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            NavigationPage.SetHasNavigationBar(this, false);
             
            // pause for a moment before animations
            await Task.Delay(App.AnimationSpeedFast);

            // Sequentially animate the login buttons. ScaleTo() makes them "grow" from a singularity to the full button size.
            await SignInButton.ScaleTo(1, (uint) App.AnimationSpeedSlow, Easing.BounceIn);
            await NewAccountButton.ScaleTo(1, (uint) App.AnimationSpeedSlow, Easing.Linear);
             
        }
    }
}