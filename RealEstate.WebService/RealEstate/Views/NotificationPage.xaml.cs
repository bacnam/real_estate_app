using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Models;
using RealEstate.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealEstate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationPage : ContentPage
    {
        NotificationViewModel viewModel;
        public NotificationPage()
        {
            InitializeComponent();
            if (Application.Current.MainPage is NavigationPage)
            {
                (Application.Current.MainPage as NavigationPage).BarTextColor = Color.Black;
            }
            NavigationPage.SetHasNavigationBar(this, false);

            BindingContext = viewModel = new NotificationViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.GetNotificationAsync().ConfigureAwait(false);
        }

        private void onBack(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void onItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var notification = e.SelectedItem as NotificationModel;
            SelectedAsync(notification).ConfigureAwait(false);
        }

        private async Task SelectedAsync(NotificationModel notification)
        {
            var success = await viewModel.ReadNofificationAsync(notification);
            if (success)
            {
                if (notification.NotificationType == "Tin tức")
                {
                    await Navigation.PushAsync(new NewsDetailsPage(notification.Source));
                }
                else
                {
                    await Navigation.PushAsync(new SearchResultsDetailsPage(notification.Source));
                }
            }
            else
            {
                await DisplayAlert("THÔNG BÁO", "Không thể tìm thấy tin bài.", "OK");
            }
        }
    }
}