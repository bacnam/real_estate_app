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
    public partial class UtilityPage : ContentPage
    {
        public UtilityPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
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

        void DuToanButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CalculatePricePage());
        }

        void TuViButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ComingSoonPage());
        }
    }
}