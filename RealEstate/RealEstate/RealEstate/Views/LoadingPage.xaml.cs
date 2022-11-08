using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealEstate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingPage : ContentPage
    {
        public LoadingPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            AutoContinue();
        }

        bool isBusy = false;

        Task AutoContinue()
        {
            return Task.Run(() =>
            {
                Thread.Sleep(5000);
                Device.BeginInvokeOnMainThread(() => onContinue(null, null));
            });
        }

        private void onContinue(object sender, EventArgs e)
        {
            if (isBusy)
            {
                return;
            }
            isBusy = true;
            Navigation.PushAsync(new WelcomePage());
        }
    }
}