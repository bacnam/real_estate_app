using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RealEstate.ViewModels;
using RealEstate.Models;

namespace RealEstate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        HomeViewModel viewModel;
        public HomePage()
        {
            InitializeComponent();
            BindingContext = viewModel = new HomeViewModel();
            NavigationPage.SetHasNavigationBar(this, false);

            AutoSlide();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Application.Current.MainPage is NavigationPage)
            {
                (Application.Current.MainPage as NavigationPage).BarTextColor = Color.Black;
            }
        }

        void onCurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {
            if (count <= 0)
            {
                count = 5;
                AutoSlide();
            }
            else
            {
                count = 5;
            }
        }

        int count = 5;

        private void AutoSlide()
        {
            Task.Run(() =>
            {
                while (count > 0)
                {
                    Thread.Sleep(1000);
                    count--;
                }
                slide();
            });
        }

        private void slide()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                int index = 0;
                if (carouselView.CurrentItem != null)
                {
                    index = viewModel.Banners.IndexOf(carouselView.CurrentItem.ToString());
                }
                if (index == viewModel.Banners.Count - 1)
                {
                    index = -1;
                }
                carouselView.ScrollTo(index + 1);
            });
        }

        private void SaleButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SearchPage(Models.MenuItemType.Sale));
        }

        private void LeaseButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SearchPage(Models.MenuItemType.Lease));
        }

        private void NewsButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewsPage());
        }

        private void ProjectButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProjectsPage());
        }

        private void CreateNewsButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateNewsPage());
        }

        private void SavedButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SavedPage(1));
        }

        private void UtilitiesButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UtilityPage());
        }

        private void onAccountButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProfilePage());
        }

        private void bannerTapped(object sender, EventArgs e)
        {
            int index = 0;
            if (carouselView.CurrentItem != null)
            {
                index = viewModel.Banners.IndexOf(carouselView.CurrentItem.ToString());
            }
            if (index == 0)
            {
                Navigation.PushAsync(new RegisterReceiveNewsPage());
            }
            else if (index == 1)
            {
                Navigation.PushAsync(new SearchPage(Models.MenuItemType.Sale));
            }
            else
            {
                Navigation.PushAsync(new CalculatePricePage());
            }
        }

        private void notificationTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NotificationPage());
        }
    }
}