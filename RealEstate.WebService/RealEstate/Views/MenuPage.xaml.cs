using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealEstate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();
            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Home, Title="Trang chủ" },
                new HomeMenuItem {Id = MenuItemType.Sale, Title="Nhà đất bán" },
                new HomeMenuItem {Id = MenuItemType.Lease, Title="Nhà đất cho thuê" },
                new HomeMenuItem {Id = MenuItemType.MyNews, Title="Tin dành cho bạn" },
                new HomeMenuItem {Id = MenuItemType.News, Title="Tin tức" },
                new HomeMenuItem {Id = MenuItemType.Project, Title="Dự án" },
                new HomeMenuItem {Id = MenuItemType.PhongThuy, Title="Phong thủy" },
                new HomeMenuItem {Id = MenuItemType.DuTinh, Title="Dự tính chi phí" },
                new HomeMenuItem {Id = MenuItemType.About, Title="Về chúng tôi" }
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                MenuItemType menuItemType = ((HomeMenuItem)e.SelectedItem).Id;
                RootPage.NavigateFromMenu(menuItemType);
            };
        }
    }
}