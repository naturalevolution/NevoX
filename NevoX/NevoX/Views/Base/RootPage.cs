using System.Collections.Generic;
using System.Threading.Tasks;
using NevoX.Statics;
using NevoX.ViewModels.Base;
using NevoX.Views.Home;
using Xamarin.Forms;

namespace NevoX.Views.Base
{

    public class HomeMenuItem
    {
        public HomeMenuItem()
        {
            MenuType = MenuType.Home;
        }

        public string Icon { get; set; }

        public MenuType MenuType { get; set; }

        public string Title { get; set; }

        public string Details { get; set; }

        public int Id { get; set; }
    }
    public enum MenuType
    {
        Home
    }

    public class CRMNavigationPage : NavigationPage
    {
        public CRMNavigationPage(Page root)
            : base(root)
        {
            Init();
        }

        public CRMNavigationPage()
        {
            Init();
        }

        void Init()
        {

            BarBackgroundColor = Palette._001;
            BarTextColor = Palette._002;
        }
    }

    public class RootPage : MasterDetailPage
    {
        Dictionary<MenuType, NavigationPage> Pages { get; set; }

        public RootPage()
        {
            Pages = new Dictionary<MenuType, NavigationPage>();
            Master = new MenuPage(this);
            BindingContext = new BaseViewModel(Navigation)
            {
                Title = "Xamarin CRM",
                Icon = "slideout.png"
            };
            //setup home page
            NavigateAsync(MenuType.Home);
        }

        void SetDetailIfNull(Page page)
        {
            if (Detail == null && page != null)
                Detail = page;
        }

        public async Task NavigateAsync(MenuType id)
        {
            Page newPage;
            if (!Pages.ContainsKey(id))
            { 
                switch (id)
                {
                   default :
                         var page = new CRMNavigationPage(new MainPage()
                         {
                             Title = "Main page From root",
                             Icon = new FileImageSource { File = "sales.png" }
                         });
                         SetDetailIfNull(page);
                         Pages.Add(id, page);
                         break;
                        /* case MenuType.Customers:
                             page = new CRMNavigationPage(new CustomersPage
                             {
                                 BindingContext = new CustomersViewModel() { Navigation = this.Navigation },
                                 Title = TextResources.MainTabs_Customers,
                                 Icon = new FileImageSource { File = "customers.png" }
                             });
                             SetDetailIfNull(page);
                             Pages.Add(id, page);
                             break;
                         case MenuType.Products:
                             page = new CRMNavigationPage(new CategoryListPage
                             {
                                 BindingContext = new CategoriesViewModel() { Navigation = this.Navigation },
                                 Title = TextResources.MainTabs_Products,
                                 Icon = new FileImageSource { File = "products.png" }
                             });
                             SetDetailIfNull(page);
                             Pages.Add(id, page);
                             break;
                         case MenuType.About:
                             page = new CRMNavigationPage(new AboutItemListPage
                             {
                                 Title = TextResources.MainTabs_Products,
                                 Icon = new FileImageSource { File = "about.png" },
                                 BindingContext = new AboutItemListViewModel() { Navigation = this.Navigation }
                             });
                             SetDetailIfNull(page);
                             Pages.Add(id, page);
                             break;
                    */

                }
            }

            newPage = Pages[id];
            if (newPage == null)
                return;

            //pop to root for Windows Phone
            if (Detail != null && Device.OS == TargetPlatform.WinPhone)
            {
                await Detail.Navigation.PopToRootAsync();
            }

            Detail = newPage;

            if (Device.Idiom != TargetIdiom.Tablet)
                IsPresented = false;
        }
    }
}