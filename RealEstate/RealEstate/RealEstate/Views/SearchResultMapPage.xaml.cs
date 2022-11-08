using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RealEstate.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using static Xamarin.Essentials.Permissions;

namespace RealEstate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchResultMapPage : ContentPage
    {
        private ObservableCollection<RealDataModel> RealDatas;
        public SearchResultMapPage(ObservableCollection<RealDataModel> realDatas)
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
            RealDatas = realDatas;

            Init();
        }

        protected override void OnAppearing()
        {
            if (Application.Current.MainPage is NavigationPage)
            {
                (Application.Current.MainPage as NavigationPage).BarTextColor = Color.Black;
            }
            base.OnAppearing();
        }

        private void Init()
        {
            foreach (var item in RealDatas)
            {
                this.maps.Pins.Add(new Pin()
                {
                    Address = item.Address,
                    Type = PinType.SavedPin,
                    Label = item.Address,
                    Position = new Position(item.Latitude, item.Longitude),
                });
            }
            var pin = maps.Pins.FirstOrDefault(p => p.Position.Latitude > 0 && p.Position.Longitude > 0);
            if (pin != null)
            {
                MapSpan mapSpan = MapSpan.FromCenterAndRadius(pin.Position, Distance.FromKilometers(10));
                maps.MoveToRegion(mapSpan);
            }
        }

        private void onBack(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
        }
    }
}