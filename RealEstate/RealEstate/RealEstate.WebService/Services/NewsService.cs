using Microsoft.EntityFrameworkCore;
using RealEstate.Core.Requests;
using RealEstate.Core.Responses;
using RealEstate.WebService.Databases;
using RealEstate.WebService.Models;

namespace RealEstate.WebService.Services
{
    public interface INewsService
    {
        Task<GetNewsResponse> GetNewsAsync(int start, bool isMarkett);
        Task<GetNewsDetailsResponse> GetNewsDetailsAsync(long newsId);
        Task<RegisterNewsResponse> RegisterNewsAsync(RegisterNewsRequest request);
    }

    public class NewsService : BaseService, INewsService
    {
        public NewsService(LibraryContext context) : base(context)
        {
        }

        public async Task<GetNewsResponse> GetNewsAsync(int start, bool isMarket)
        {
            GetNewsResponse response = new GetNewsResponse();
            try
            {
                IQueryable<NewsModel> news = null;
                if (isMarket)
                {
                    news = Context.News.Where(n => n.NewsType == "1");
                }
                else
                {
                    news = Context.News.Where(n => n.NewsType != "1");
                }
                response.News = await news.OrderBy(n => n.Sort)
                    .Skip(start).Take(10)
                    .Select(n => new NewsData()
                    {
                        Id = n.Id,
                        ReadTime = n.ReadTime,
                        Created = n.Created,
                        Description = n.Description,
                        Thumbnail = n.Thumbnail,
                        Title = n.Title
                    }).ToArrayAsync();
                response.Success = true;
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.Message);
                Log.WriteLine(ex.StackTrace);
            }
            return response;
        }

        public async Task<GetNewsDetailsResponse> GetNewsDetailsAsync(long newsId)
        {
            GetNewsDetailsResponse response = new GetNewsDetailsResponse();
            try
            {
                var news = await Context.News.FirstOrDefaultAsync(n => n.Id == newsId);

                if (news != null)
                {
                    news.ReadTime++;
                    response.NewsDetails = new NewsDetailsData()
                    {
                        Content = news.Content,
                        Created = news.Created,
                        Description = news.Description,
                        Id = news.Id,
                        Thumbnail = news.Thumbnail,
                        Title = news.Title
                    };
                    response.Success = true;
                    Context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.Message);
                Log.WriteLine(ex.StackTrace);
            }
            return response;
        }

        public async Task<RegisterNewsResponse> RegisterNewsAsync(RegisterNewsRequest request)
        {
            return new RegisterNewsResponse()
            {
                Success = true
            };
        }
    }
}
