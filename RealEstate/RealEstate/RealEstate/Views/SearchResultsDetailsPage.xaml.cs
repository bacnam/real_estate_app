using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RealEstate.Models;
using RealEstate.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealEstate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchResultsDetailsPage : ContentPage
    {
        SearchResultDetailsViewModel viewModel;
        public SearchResultsDetailsPage(long realEstateId)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            try
            {
                BindingContext = viewModel = new SearchResultDetailsViewModel(realEstateId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            AutoSlide();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.LoadDetailsCommand.Execute(null);
            if (Application.Current.MainPage is NavigationPage)
            {
                (Application.Current.MainPage as NavigationPage).BarTextColor = Color.Black;
            }
        }

        void onCurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {
            if (count <= 0)
            {
                count = 5;
                AutoSlide();
            }
            else
            {
                count = 5;
            }
        }

        int count = 5;

        private void AutoSlide()
        {
            Task.Run(() =>
            {
                while (count > 0)
                {
                    Thread.Sleep(1000);
                    count--;
                }
                slide();
            });
        }

        private void slide()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                int index = 0;
                if (carouselView.CurrentItem != null)
                {
                    index = viewModel.RealData.ImageUrls.IndexOf(carouselView.CurrentItem.ToString());
                }
                if (index == viewModel.RealData.ImageUrls.Count - 1)
                {
                    index = -1;
                }
                carouselView.ScrollTo(index + 1);
            });
        }

        private void onBack(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        async void onSave(object sender, EventArgs e)
        {
            await viewModel.Save();
        }

        void onShare(object sender, EventArgs e)
        {
            string text = string.Format("Hoa Phuong: {0}", viewModel.RealData.Address);
            Xamarin.Essentials.Share.RequestAsync(text);
        }

        private void onViewOnMaps(object sender, EventArgs e)
        {
            var realDatas = new ObservableCollection<RealDataModel>();
            realDatas.Add(viewModel.RealData);
            SearchResultMapPage searchResultMapPage = new SearchResultMapPage(realDatas);
            Navigation.PushModalAsync(searchResultMapPage);
        }
    }
}