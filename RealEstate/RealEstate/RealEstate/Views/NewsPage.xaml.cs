using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Models;
using RealEstate.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealEstate.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsPage : ContentPage
    {
        NewsViewModel viewModel;
        public NewsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new NewsViewModel();
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

        private void onSelected(object sender, SelectedItemChangedEventArgs e)
        {
            NewsModel news = e.SelectedItem as NewsModel;
            Navigation.PushAsync(new NewsDetailsPage(news.Id));
        }

        private void onHeaderSelected(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewsDetailsPage(viewModel.LatestNews.Id));
        }

        private void onNews(object sender, EventArgs e)
        {
            var colorNews = lblNews.TextColor;
            var colorMarket = lblMarket.TextColor;

            lblMarket.TextColor = colorNews;
            lineMarket.IsVisible = false;

            lblNews.TextColor = colorMarket;
            lineNews.IsVisible = true;

            viewModel.NewsCommand.Execute(null);
        }

        private void onMarket(object sender, EventArgs e)
        {
            var colorNews = lblNews.TextColor;
            var colorMarket = lblMarket.TextColor;

            lblMarket.TextColor = colorNews;
            lineMarket.IsVisible = true;

            lblNews.TextColor = colorMarket;
            lineNews.IsVisible = false;

            viewModel.MarketCommand.Execute(null);
        }

        List<string> sorts = new List<string>()
        {
            "Tin mới nhất", "Đọc nhiều nhất", "Đọc ít nhất"
        };

        async void onSort(object sender, EventArgs e)
        {
            string sort = await DisplayActionSheet("Sắp xếp theo", "Hủy", sorts[viewModel.Sort], sorts.Where(s => s != sorts[viewModel.Sort]).ToArray());
            viewModel.Sort = sorts.IndexOf(sort);
        }
    }
}