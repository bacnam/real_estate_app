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
    public partial class SelectPricePage : ContentPage
    {
        SelectPriceViewModel viewModel;
        public SelectPricePage(bool isSale)
        {
            InitializeComponent();

            BindingContext = viewModel = new SelectPriceViewModel(isSale);
            if (Application.Current.MainPage is NavigationPage)
            {
                (Application.Current.MainPage as NavigationPage).BarTextColor = Color.Black;
            }
            NavigationPage.SetHasNavigationBar(this, false);
        }

        public Action<double, double> SelectionDone;

        private void priceItemTapped(object sender, ItemTappedEventArgs e)
        {
            PriceModel price = viewModel.Prices[e.ItemIndex];

            SelectionDone?.Invoke(price.FromPrice, price.ToPrice);
            Navigation.PopAsync();
        }

        private void onBack(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        private void onDone(object sender, EventArgs e)
        {
            double toPriceValue = 50;
            double fromPriceValue = 0;
            double.TryParse(fromPrice.Text, out fromPriceValue);
            double.TryParse(toPrice.Text, out toPriceValue);

            if (toPriceValue < fromPriceValue)
            {
                DisplayAlert("THÔNG BÁO", "Dữ liệu nhập vào không chính xác.", "OK");
            }
            else
            {
                SelectionDone?.Invoke(fromPriceValue, toPriceValue);
                Navigation.PopAsync();
            }
        }
    }
}