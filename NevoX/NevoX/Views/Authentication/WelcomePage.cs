using NevoX.Helpers.Boxs;
using NevoX.Localization;
using NevoX.Statics;
using NevoX.ViewModels.Splash;
using NevoX.Views.Base;
using Xamarin.Forms;

namespace NevoX.Views.Authentication
{
    public class WelcomePage : ModelBoundContentPage<SplashViewModel>
    {
        public WelcomePage()
        {
            BindingContext = new SplashViewModel();

            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                {
                    new RowDefinition {Height = new GridLength(1, GridUnitType.Star)},
                    new RowDefinition {Height = new GridLength(100, GridUnitType.Absolute)}
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition {Width = GridLength.Auto}
                },
                BackgroundColor = Color.Red
            };

            grid.Children.Add(new Label
            {
                Text = "EIXEM",
                TextColor = Color.White,
                FontAttributes = FontAttributes.Bold,
                FontSize = 70,
                BackgroundColor = Color.Aqua,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            }, 0, 2, 0, 3);

            var buttonStackLayout = new StackLayout
            {
                Spacing = 20,
                Padding = Thicknesses.StackLayout2Buttons,
                BackgroundColor = Color.Purple
            };

            var roundedBoxViewTransparent = new RoundedBox
            {
                FontSize = 20,
                TextColor = Palette._001,
                CornerRadius = 20,
                BorderWidth = 10,
                BorderColor = Color.White,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            var roundedBoxViewWhite = new RoundedBox
            {
                TextColor = Palette._002,
                FontSize = 20,
                CornerRadius = 20,
                BorderColor = Color.White,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            roundedBoxViewTransparent.Command = ViewModel.SignInCommand;
            roundedBoxViewTransparent.Text = TextResources.Login_SignIn;

            roundedBoxViewWhite.Text = TextResources.Splash_NewAccount;
            roundedBoxViewWhite.Command = ViewModel.NewAccountCommand;

            buttonStackLayout.Children.Add(roundedBoxViewTransparent);
            buttonStackLayout.Children.Add(roundedBoxViewWhite);

            grid.Children.Add(buttonStackLayout, 0, 2, 3, 4);

            // Build the page.
            Content = grid;
        }
    }
}