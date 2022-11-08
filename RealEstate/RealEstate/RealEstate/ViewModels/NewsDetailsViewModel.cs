using System;
using System.Collections.Generic;
using System.Text;
using RealEstate.Models;
using RealEstate.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RealEstate.ViewModels
{
    public class NewsDetailsViewModel : BaseViewModel
    {
        public NewsDetailsViewModel(long newsId)
        {
            GetNewsDetails(newsId).ConfigureAwait(false);
        }

        private async Task GetNewsDetails(long newsId)
        {
            INewsService newsService = DependencyService.Get<INewsService>();
            var res = await newsService.GetNewsDetailsAsync(new Core.Requests.GetNewsDetailsRequest() { Id = newsId });
            if (res.Success)
            {
                News = new NewsDetailsModel()
                {
                    Content = res.NewsDetails.Content,
                    Created = res.NewsDetails.Created,
                    Description = res.NewsDetails.Description,
                    Id = res.NewsDetails.Id,
                    Thumbnail = res.NewsDetails.Thumbnail,
                    Title = res.NewsDetails.Title
                };
            }
        }

        NewsDetailsModel _news;
        public NewsDetailsModel News { get { return _news; } set { SetProperty(ref _news, value); } }
    }
}
