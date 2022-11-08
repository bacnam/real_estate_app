using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Core.Requests;
using RealEstate.Core.Responses;

namespace RealEstate.Services
{
    public interface IRealEstateService
    {
        Task<bool> ChangeRealEstateAsync(long id);
        Task<GetRealDataDetailsResponse> GetRealEstate(long id);
        Task<SearchResponse> GetRealEstateByProject(SearchRequest request);
        Task<SearchResponse> GetSavedRealEstates(int start = 0);
        Task<CreateEstateResponse> CreateEstateAsync(CreateEstateRequest request);
        Task<SearchResponse> GetMyRealEstatesAsync(bool isSale);
    }

    public class RealEstateService : IRealEstateService
    {
        public async Task<bool> ChangeRealEstateAsync(long id)
        {
            var service = Xamarin.Forms.DependencyService.Get<IAPIService>();
            ChangeRealEstateRequest request = new ChangeRealEstateRequest();
            request.RealEstateId = id;
            var res = await service.PutAsync<ChangeRealEstateResponse>("realestates", request.ToJson());
            return res.Success;
        }

        public Task<CreateEstateResponse> CreateEstateAsync(CreateEstateRequest request)
        {
            var service = Xamarin.Forms.DependencyService.Get<IAPIService>();
            return service.PostAsync<CreateEstateResponse>("realestates", request.ToJson());
        }

        public Task<SearchResponse> GetMyRealEstatesAsync(bool isSale)
        {
            var service = Xamarin.Forms.DependencyService.Get<IAPIService>();
            return service.GetAsync<SearchResponse>("realestates/?sale=" + (isSale ? "1" : "0"));
        }

        public Task<GetRealDataDetailsResponse> GetRealEstate(long id)
        {
            var service = Xamarin.Forms.DependencyService.Get<IAPIService>();
            return service.GetAsync<GetRealDataDetailsResponse>("realestates/" + id);
        }

        public Task<SearchResponse> GetRealEstateByProject(SearchRequest request)
        {
            var service = Xamarin.Forms.DependencyService.Get<IAPIService>();
            return service.PostAsync<SearchResponse>("search/project", request.ToJson());
        }

        public Task<SearchResponse> GetSavedRealEstates(int start = 0)
        {
            var service = Xamarin.Forms.DependencyService.Get<IAPIService>();
            return service.GetAsync<SearchResponse>(string.Format("realestates/saved/?start={0}", start));
        }
    }
}
