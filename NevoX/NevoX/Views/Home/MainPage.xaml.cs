using NevoX.Statics;
using NevoX.ViewModels.Authentication;
using Xamarin.Forms;

namespace NevoX.Views.Home
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<LoginViewModel>(this, MessagingServiceConstants.AUTHENTICATED, async value =>
            { 
                await DisplayAlert(MessagingServiceConstants.AUTHENTICATED, "Connexion réussi : "+value.LoginModel.Email, "OK");
            });
             

        }
    }
}
