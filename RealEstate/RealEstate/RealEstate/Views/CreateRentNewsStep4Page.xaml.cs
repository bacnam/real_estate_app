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
    public partial class CreateRentNewsStep4Page : ContentPage
    {
        ViewModels.CreateRentNewsStep4ViewModel viewModel;
        public CreateRentNewsStep4Page(Models.CreateRentNewsModel rentNews)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            BindingContext = viewModel = new CreateRentNewsStep4ViewModel(rentNews);
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

        private async void onDone(object sender, EventArgs e)
        {
            var success = await viewModel.DoDone();

            if (success)
            {
                await DisplayAlert("THÔNG BÁO!", "Tạo tin thành công!", "OK");
                await Navigation.PopToRootAsync();
            }
            else
            {
                await DisplayAlert("THÔNG BÁO!", "Không thể tạo tin...", "OK");
            }
        }
    }
}