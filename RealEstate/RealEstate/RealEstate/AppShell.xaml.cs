using System;
using System.Collections.Generic;
using RealEstate.ViewModels;
using RealEstate.Views;
using Xamarin.Forms;

namespace RealEstate
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(SearchPage), typeof(SearchPage));
        }

    }
}
