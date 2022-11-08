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
    public partial class RegisterPage : ContentPage
    {
        RegisterViewModel viewModel;
        public RegisterPage()
        {
            InitializeComponent();
            if (Application.Current.MainPage is NavigationPage)
            {
                (Application.Current.MainPage as NavigationPage).BarTextColor = Color.Black;
            }
            NavigationPage.SetHasNavigationBar(this, false);

            BindingContext = viewModel = new RegisterViewModel();
        }

        private void onLogin(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }

        async void onRegister(object sender, EventArgs e)
        {
            await viewModel.Register(() =>
            {
                Navigation.PushAsync(new LoginPage());
            }, message =>
            {
                DisplayAlert("THÔNG BÁO!", message, "OK");
            });
        }

        private void onBack(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}