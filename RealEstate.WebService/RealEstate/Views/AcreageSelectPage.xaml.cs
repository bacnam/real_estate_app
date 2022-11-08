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
    public partial class AcreageSelectPage : ContentPage
    {
        public AcreageSelectPage()
        {
            InitializeComponent();
            if (Application.Current.MainPage is NavigationPage)
            {
                (Application.Current.MainPage as NavigationPage).BarTextColor = Color.Black;
            }
            NavigationPage.SetHasNavigationBar(this, false);
        }

        public Action<int, int> SelectionDone;

        private void onDone(object sender, EventArgs e)
        {
            int toAcreageValue = 50;
            int fromAcreageValue = 0;
            int.TryParse(fromAcreage.Text, out fromAcreageValue);
            int.TryParse(toAcreage.Text, out toAcreageValue);

            if (toAcreageValue < fromAcreageValue)
            {
                DisplayAlert("THÔNG BÁO", "Dữ liệu nhập vào không chính xác.", "OK");
            }
            else
            {
                SelectionDone?.Invoke(fromAcreageValue, toAcreageValue);
                Navigation.PopAsync();
            }
        }

        private void onBack(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}