using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealEstate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalculatePriceResultPage : ContentPage
    {
        ViewModels.CalculatorResultViewModel viewModel;
        public CalculatePriceResultPage(Models.CalculatorModel calculator)
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            BindingContext = viewModel = new ViewModels.CalculatorResultViewModel(calculator);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Application.Current.MainPage is NavigationPage)
            {
                (Application.Current.MainPage as NavigationPage).BarTextColor = Color.White;
            }
        }

        private void onBack(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void onSave(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void onHome(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}