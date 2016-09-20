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



            /*
             

            <Style x:Key="BtnWhite" TargetType="Button">
                <Setter Property="TextColor" Value="{x:Static statics:Palette._002}" />
                <Setter Property="FontSize" Value="Medium" />
                <Setter Property="HorizontalOptions" Value="FillAndExpand" />
                <Setter Property="BackgroundColor" Value="White" />
            </Style>

            <Style x:Key="BtnTransparent" TargetType="Button">
                <Setter Property="TextColor" Value="{x:Static statics:Palette._001}" />
                <Setter Property="FontSize" Value="Medium" />
                <Setter Property="HorizontalOptions" Value="FillAndExpand" />
                <Setter Property="BackgroundColor" Value="Transparent" />
            </Style>
             */
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
             
            // pause for a moment before animations
            await Task.Delay(App.AnimationSpeedFast);

            // Sequentially animate the login buttons. ScaleTo() makes them "grow" from a singularity to the full button size.
            await SignInButton.ScaleTo(1, (uint) App.AnimationSpeedSlow, Easing.BounceIn);
            await NewAccountButton.ScaleTo(1, (uint) App.AnimationSpeedSlow, Easing.Linear);
             
        }
    }
}