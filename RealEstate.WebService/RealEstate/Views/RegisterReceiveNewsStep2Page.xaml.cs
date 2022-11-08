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
    public partial class RegisterReceiveNewsStep2Page : ContentPage
    {
        RegisterReceiveNewsStep2ViewModel viewModel;
        public RegisterReceiveNewsStep2Page(Models.RegisterNewsModel registerNews)
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            BindingContext = viewModel = new RegisterReceiveNewsStep2ViewModel(registerNews);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Application.Current.MainPage is NavigationPage)
            {
                (Application.Current.MainPage as NavigationPage).BarTextColor = Color.Black;
            }
        }

        private void onBack(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        void onRegister(object sender, EventArgs e)
        {
            DoRegister().ConfigureAwait(false);
        }

        private async Task DoRegister()
        {
            var success = await viewModel.RegisterAsync();
            if (success)
            {
                await Navigation.PushAsync(new RegisterReceiveNewsSuccessPage());
            }
            else
            {
                await DisplayAlert("THÔNG BÁO", "Không thể đăng ký nhận tin", "OK");
            }
        }
    }
}