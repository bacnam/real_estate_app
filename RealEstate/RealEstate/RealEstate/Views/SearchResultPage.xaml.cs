using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using RealEstate.ViewModels;
using RealEstate.Models;
using RealEstate.Core.Requests;
using RealEstate.Renderers;

namespace RealEstate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchResultPage : ContentPage
    {
        SearchResultViewModel viewModel;
        public SearchResultPage(SearchRequest request)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            BindingContext = viewModel = new SearchResultViewModel(request);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Application.Current.MainPage is NavigationPage)
            {
                (Application.Current.MainPage as NavigationPage).BarTextColor = Color.Black;
            }
        }

        private void onSelected(object sender, ItemTappedEventArgs e)
        {
            var realEstate = e.Item as RealDataModel;
            Navigation.PushAsync(new SearchResultsDetailsPage(realEstate.Id));
        }

        private void onBack(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void onSaved(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SavedPage(1));
        }

        async void onSave(object sender, EventArgs e)
        {
            var btn = sender as CustomImageButton;
            await viewModel.Save(btn.Tag);
        }

        private void onViewOnMaps(object sender, EventArgs e)
        {
            SearchResultMapPage searchResultMapPage = new SearchResultMapPage(viewModel.RealDatas);
            Navigation.PushModalAsync(searchResultMapPage);
        }

        List<string> sorts = new List<string>()
        {
            "Tin mới nhất", "Giá thấp nhất", "Giá cao nhất", "Diện tích lớn nhất", "Diện tích nhỏ nhất"
        };

        async void onSort(object sender, EventArgs e)
        {
            string sort = await DisplayActionSheet("Sắp xếp theo", "Hủy", sorts[viewModel.Sort], sorts.Where(s => s != sorts[viewModel.Sort]).ToArray());
            viewModel.Sort = sorts.IndexOf(sort);
        }
    }
}