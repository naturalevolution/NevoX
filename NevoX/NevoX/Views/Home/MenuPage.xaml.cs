using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NevoX.ViewModels.Base;
using NevoX.Views.Base;
using Xamarin.Forms;

namespace NevoX.Views.Home
{
    public partial class MenuPage : ContentPage
    {
        RootPage root;
        List<HomeMenuItem> menuItems;
        public MenuPage(RootPage root)
        {
            Title = "Menu page";
            this.root = root;
            InitializeComponent();
            BindingContext = new BaseViewModel(Navigation)
            {
                Title = "XamarinCRM",
                Subtitle = "XamarinCRM",
                Icon = "slideout.png"
            };

            ListViewMenu.ItemsSource = menuItems = new List<HomeMenuItem>
                {
                    new HomeMenuItem { Title = "Home", MenuType = MenuType.Home, Icon ="customers.png" }

                };

            ListViewMenu.SelectedItem = menuItems[0];

            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (ListViewMenu.SelectedItem == null)
                    return;

                await this.root.NavigateAsync(((HomeMenuItem)e.SelectedItem).MenuType);
            };
        }
    }
}
