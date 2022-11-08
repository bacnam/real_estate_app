using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Core.Requests;
using RealEstate.Core.Responses;

namespace RealEstate.Services
{
    public interface INewsService
    {
        Task<GetNewsResponse> GetNewsAsync(GetNewsRequest request);
        Task<GetNewsDetailsResponse> GetNewsDetailsAsync(GetNewsDetailsRequest request);
    }

    public class NewsService : INewsService
    {
        public Task<GetNewsResponse> GetNewsAsync(GetNewsRequest request)
        {
            var apiService = Xamarin.Forms.DependencyService.Get<IAPIService>();
            return apiService.PostAsync<GetNewsResponse>("news", request.ToJson());
        }

        public Task<GetNewsDetailsResponse> GetNewsDetailsAsync(GetNewsDetailsRequest request)
        {
            var apiService = Xamarin.Forms.DependencyService.Get<IAPIService>();
            return apiService.GetAsync<GetNewsDetailsResponse>("news/" + request.Id.ToString());
        }
    }
}
