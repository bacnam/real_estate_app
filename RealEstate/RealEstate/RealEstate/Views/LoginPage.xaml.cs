using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealEstate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        LoginViewModel viewModel;
        public LoginPage()
        {
            InitializeComponent();

            if (Application.Current.MainPage is NavigationPage)
            {
                (Application.Current.MainPage as NavigationPage).BarTextColor = Color.Black;
            }
            NavigationPage.SetHasNavigationBar(this, false);

            BindingContext = viewModel = new LoginViewModel();
            viewModel.LoginSuccess = LoginSuccess;
            viewModel.LoginFail = LoginFail;
        }

        private void LoginSuccess()
        {
            Navigation.PushAsync(new HomePage());
        }

        private void LoginFail(string message)
        {
            DisplayAlert("THÔNG BÁO!", message, "OK");
        }

        private void onRegister(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }

        private void onBack(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}