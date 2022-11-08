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
    public partial class RegisterReceiveNewsSuccessPage : ContentPage
    {
        public RegisterReceiveNewsSuccessPage()
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

        private void onGoHome(object sender, EventArgs e)
        {
            Navigation.PopToRootAsync();
        }

        private void onManagerNews(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MnanagerNewsPage());
        }
    }
}