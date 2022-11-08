using System;
using System.Collections.Generic;
using System.Text;
using RealEstate.Models;
using System.Threading.Tasks;

namespace RealEstate.Services
{
    public interface IAddressService
    {
        Task<IEnumerable<CityModel>> GetCitiesAsync();
        Task<IEnumerable<DistrictModel>> GetDistrictsAsync();
        Task<IEnumerable<DirectionModel>> GetDirectionsAsync();
    }

    public class AddressService : IAddressService
    {
        public Task<IEnumerable<CityModel>> GetCitiesAsync()
        {
            var apiService = Xamarin.Forms.DependencyService.Get<IAPIService>();
            return apiService.GetAsync<IEnumerable<CityModel>>("addresses/cities");
        }

        public Task<IEnumerable<DistrictModel>> GetDistrictsAsync()
        {
            var apiService = Xamarin.Forms.DependencyService.Get<IAPIService>();
            return apiService.GetAsync<IEnumerable<DistrictModel>>("addresses/districts");
        }

        public Task<IEnumerable<DirectionModel>> GetDirectionsAsync()
        {
            var apiService = Xamarin.Forms.DependencyService.Get<IAPIService>();
            return apiService.GetAsync<IEnumerable<DirectionModel>>("addresses/directions");
        }
    }
}
