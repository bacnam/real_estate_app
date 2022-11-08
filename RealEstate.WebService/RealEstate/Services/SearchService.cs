using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Core.Requests;
using RealEstate.Core.Responses;

namespace RealEstate.Services
{
    public interface ISearchService
    {
        Task<SearchResponse> SearchAsync(SearchRequest request);
        Task<SearchAddressResponse> SearchAddressAsync(string address);
    }

    public class SearchService : ISearchService
    {
        public Task<SearchAddressResponse> SearchAddressAsync(string address)
        {
            SearchAddressRequest request = new SearchAddressRequest() { Address = address };
            var service = Xamarin.Forms.DependencyService.Get<IAPIService>();
            return service.PostAsync<SearchAddressResponse>("search/address", request.ToJson());
        }

        public Task<SearchResponse> SearchAsync(SearchRequest request)
        {
            var service = Xamarin.Forms.DependencyService.Get<IAPIService>();
            return service.PostAsync<SearchResponse>("search", request.ToJson());
        }
    }
}
