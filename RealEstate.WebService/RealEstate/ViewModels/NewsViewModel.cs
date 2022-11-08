using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using RealEstate.Core.Requests;
using RealEstate.Models;
using RealEstate.Services;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

namespace RealEstate.ViewModels
{
    public class NewsViewModel : BaseViewModel
    {
        public NewsViewModel()
        {
            listNews = new ObservableRangeCollection<NewsModel>();

            NewsCommand.Execute(null);
        }

        public Command MarketCommand
        {
            get
            {
                IsMarket = true;
                return new Command(async () => await GetData());
            }
        }

        public Command NewsCommand
        {
            get
            {
                IsMarket = false;
                return new Command(async () => await GetData());
            }
        }

        int sort;
        public int Sort
        {
            get { return sort; }
            set { SetProperty(ref sort, value); OnPropertyChanged("ListNews"); }
        }

        private async Task GetData()
        {
            if (IsBusy)
            {
                return;
            }
            try
            {
                IsBusy = true;

                INewsService newsService = DependencyService.Get<INewsService>();
                GetNewsRequest request = new GetNewsRequest()
                {
                    Start = 0,
                    IsMarket = IsMarket
                };
                var res = await newsService.GetNewsAsync(request);
                listNews.Clear();
                if (res.Success)
                {
                    var news = res.News.Select(n => new NewsModel()
                    {
                        Id = n.Id,
                        ReadTime = n.ReadTime,
                        Created = n.Created,
                        Description = n.Description,
                        Thumbnail = n.Thumbnail,
                        Title = n.Title
                    });
                    listNews.AddRange(news.Skip(1));

                    if (listNews.Count() > 0)
                    {
                        LatestNews = news.ElementAt(0);
                    }

                    OnPropertyChanged("ListNews");
                    OnPropertyChanged("LatestNews");
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        public bool IsMarket { get; set; }

        public NewsModel latestNews;
        public NewsModel LatestNews
        {
            get { return latestNews; }
            set { SetProperty(ref latestNews, value); }
        }

        public ObservableRangeCollection<NewsModel> listNews;
        public ObservableRangeCollection<NewsModel> ListNews
        {
            get
            {
                ObservableRangeCollection<NewsModel> _listNews = new ObservableRangeCollection<NewsModel>();
                if (Sort == 0)
                {
                    _listNews.AddRange(listNews.OrderByDescending(s => s.Created));
                }
                else if (Sort == 1)
                {
                    _listNews.AddRange(listNews.OrderByDescending(s => s.ReadTime));
                }
                else if (Sort == 2)
                {
                    _listNews.AddRange(listNews.OrderBy(s => s.ReadTime));
                }
                return _listNews;
            }
        }
    }
}
