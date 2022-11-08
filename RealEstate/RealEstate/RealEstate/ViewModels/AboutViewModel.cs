using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RealEstate.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(() => Xamarin.Essentials.Browser.OpenAsync("https://xamarin.com/platform"));
        }

        public ICommand OpenWebCommand { get; }
    }
}
