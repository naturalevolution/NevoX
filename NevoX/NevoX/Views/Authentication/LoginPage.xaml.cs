using System.Threading.Tasks;
using NevoX.ViewModels.Authentication;
using NevoX.Views.Base;
using Xamarin.Forms;

namespace NevoX.Views.Authentication
{
    /// <summary>
    ///     This class definition just gives us a way to reference ModelBoundContentPage<T> in the XAML of this Page.
    /// </summary>
    public abstract class LoginPageXaml : ModelBoundContentPage<LoginViewModel> {}

    public partial class LoginPage : LoginPageXaml
    {
        public LoginPage()
        {
            InitializeComponent(); 

            BindingContext = new LoginViewModel();

            SignInButton.Command = ViewModel.SaveCommand;
            CancelButton.Command = ViewModel.CancelCommand; 
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
             
            // pause for a moment before animations
            await Task.Delay(App.AnimationSpeedFast);
            // Sequentially animate the login buttons. ScaleTo() makes them "grow" from a singularity to the full button size.
            await SignInButton.ScaleTo(1, (uint)App.AnimationSpeedSlow, Easing.SinIn);
            await CancelButton.ScaleTo(1, (uint)App.AnimationSpeedSlow, Easing.BounceIn);
        }
    }
}