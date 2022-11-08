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
    public partial class MnanagerNewsPage : ContentPage
    {
        ManagerNewsViewModel viewModel;
        public MnanagerNewsPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = viewModel = new ManagerNewsViewModel(true);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Application.Current.MainPage is NavigationPage)
            {
                (Application.Current.MainPage as NavigationPage).BarTextColor = Color.Black;
            }

            if (viewModel.IsSale)
            {
                SetSale();
            }
            else
            {
                SetLease();
            }
        }

        void onLease(object sender, EventArgs e)
        {
            viewModel.IsSale = false;
            SetLease();
            viewModel.LoadMoreCommand.Execute(null);
        }

        void onSale(object sender, EventArgs e)
        {
            viewModel.IsSale = true;
            SetSale();
            viewModel.LoadMoreCommand.Execute(null);
        }

        private void SetLease()
        {
            var colorLease = lblLease.TextColor;
            var colorSale = lblSale.TextColor;

            lblLease.TextColor = colorSale;
            lineLease.IsVisible = true;

            lblSale.TextColor = colorLease;
            lineSale.IsVisible = false;
        }

        private void SetSale()
        {
            var colorLease = lblLease.TextColor;
            var colorSale = lblSale.TextColor;

            lblLease.TextColor = colorSale;
            lineLease.IsVisible = false;

            lblSale.TextColor = colorLease;
            lineSale.IsVisible = true;
        }

        private void onBack(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void onSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as RealDataModel;
            if (item != null)
            {
                Navigation.PushAsync(new SearchResultsDetailsPage(item.Id));
            }
        }
    }
}