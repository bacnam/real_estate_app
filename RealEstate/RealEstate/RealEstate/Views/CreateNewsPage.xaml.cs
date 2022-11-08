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
    public partial class CreateNewsPage : ContentPage
    {
        public CreateNewsPage()
        {
            InitializeComponent();


            if (Application.Current.MainPage is NavigationPage)
            {
                (Application.Current.MainPage as NavigationPage).BarTextColor = Color.Black;
            }
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void onBack(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void onSaved(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SavedPage(1));
        }

        private void onSale(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateRentNewsPage(true));
        }

        private void onRent(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateRentNewsPage(false));
        }
    }
}