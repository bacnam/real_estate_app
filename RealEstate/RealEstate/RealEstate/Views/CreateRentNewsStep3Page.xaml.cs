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
    public partial class CreateRentNewsStep3Page : ContentPage
    {
        CreateRentNewsStep3ViewModel viewModel;
        public CreateRentNewsStep3Page(CreateRentNewsModel rentNews)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            BindingContext = viewModel = new CreateRentNewsStep3ViewModel(rentNews);
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

        private void onContinute(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(viewModel.RentNews.ContactName)
                || string.IsNullOrEmpty(viewModel.RentNews.ContactPhone))
            {
                this.DisplayAlert("THÔNG BÁO", "Vui lòng nhập đầy đủ thông tin...", "OK");
            }
            else
            {
                Navigation.PushAsync(new CreateRentNewsStep4Page(viewModel.RentNews));
            }
        }
    }
}