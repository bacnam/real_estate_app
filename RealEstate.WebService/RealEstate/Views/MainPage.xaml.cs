using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RealEstate.Models;

namespace RealEstate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<MenuItemType, NavigationPage> MenuPages = new Dictionary<MenuItemType, NavigationPage>();
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.Home, (NavigationPage)Detail);
        }

        public void NavigateFromMenu(MenuItemType menuItemType)
        {
            if (!MenuPages.ContainsKey(menuItemType))
            {
                switch (menuItemType)
                {
                    case MenuItemType.Home:
                        MenuPages.Add(menuItemType, new NavigationPage(new HomePage()));
                        break;
                    case MenuItemType.Sale:
                    case MenuItemType.Lease:
                        MenuPages.Add(menuItemType, new NavigationPage(new SearchPage(menuItemType)));
                        break;
                    case MenuItemType.News:
                    case MenuItemType.MyNews:
                    case MenuItemType.Project:
                    case MenuItemType.PhongThuy:
                        MenuPages.Add(menuItemType, new NavigationPage(new NewsPage()));
                        break;
                    case MenuItemType.About:
                        MenuPages.Add(menuItemType, new NavigationPage(new AboutPage()));
                        break;
                    case MenuItemType.DuTinh:
                        MenuPages.Add(menuItemType, new NavigationPage(new CalculatePricePage()));
                        break;
                    default:
                        MenuPages.Add(menuItemType, new NavigationPage(new AboutPage()));
                        break;
                }
            }

            var newPage = MenuPages[menuItemType];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}