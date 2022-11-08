using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RealEstate.ViewModels;
using RealEstate.Models;

namespace RealEstate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        ProfileViewModel viewModel;
        public ProfilePage()
        {
            InitializeComponent();
            if (Application.Current.MainPage is NavigationPage)
            {
                (Application.Current.MainPage as NavigationPage).BarTextColor = Color.Black;
            }
            NavigationPage.SetHasNavigationBar(this, false);

            BindingContext = viewModel = new ProfileViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.GetProfileCommand.Execute(null);
        }

        private void onBack(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void onManagerNews(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MnanagerNewsPage());
        }

        private void onManagerSaved(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SavedPage(1));
        }

        private void onNotification(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NotificationPage());
        }
    }
}