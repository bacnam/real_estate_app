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
    public partial class WelcomePage : ContentPage
    {
        WelcomeViewModel viewModel;
        public WelcomePage()
        {
            InitializeComponent();

            if (Application.Current.MainPage is NavigationPage)
            {
                (Application.Current.MainPage as NavigationPage).BarTextColor = Color.Black;
            }
            NavigationPage.SetHasNavigationBar(this, false);

            BindingContext = viewModel = new WelcomeViewModel();
        }

        private void onLogin(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }

        private void onRegister(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
    }
}