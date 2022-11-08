using Microsoft.EntityFrameworkCore;
using RealEstate.WebService.Databases;
using RealEstate.WebService.Models;

namespace RealEstate.WebService.Services
{
    public interface IAddressService
    {
        Task<IEnumerable<CityModel>> GetCitiesAsync();
        Task<IEnumerable<DistrictModel>> GetDistrictsAsync(params int[] cities);
        Task<IEnumerable<WardModel>> GetWardsAsync(params int[] districts);
        Task<IEnumerable<StreetModel>> GetStreetsAsync(int wardId);
        Task<IEnumerable<DirectionModel>> GetDirectionsAsync();
        Task<IEnumerable<RoomModel>> GetRoomsAsync();
    }

    public class AddressService : SearchService, IAddressService
    {
        public AddressService(LibraryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<CityModel>> GetCitiesAsync()
        {
            try
            {
                return await Context.Cities.OrderBy(c => c.Sort).ToListAsync();
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.Message);
                Log.WriteLine(ex.StackTrace);
                return new List<CityModel>();
            }
        }

        public async Task<IEnumerable<DistrictModel>> GetDistrictsAsync(params int[] cities)
        {
            try
            {
                var districts = from d1 in Context.Districts.Include(d => d.City)
                                from d2 in cities
                                where d1.City.Id == d2
                                select d1;
                return await districts.OrderBy(c => c.Sort).ToListAsync();
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.Message);
                Log.WriteLine(ex.StackTrace);
                return new List<DistrictModel>();
            }
        }

        public async Task<IEnumerable<DirectionModel>> GetDirectionsAsync()
        {
            try
            {
                return await Context.Directions.ToListAsync();
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.Message);
                Log.WriteLine(ex.StackTrace);
                return new List<DirectionModel>();
            }
        }

        public Task<IEnumerable<RoomModel>> GetRoomsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<WardModel>> GetWardsAsync(params int[] districts)
        {
            try
            {
                var wards = from w1 in Context.Wards.Include(w => w.District)
                            from w2 in districts
                            where w1.District.Id == w2
                            select w1;
                return await wards.OrderBy(c => c.Sort).ToListAsync();
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.Message);
                Log.WriteLine(ex.StackTrace);
                return new List<WardModel>();
            }
        }

        public async Task<IEnumerable<StreetModel>> GetStreetsAsync(int wardId)
        {
            try
            {
                return await Context.Streets.Include(d => d.Ward).Where(d => d.Ward.Id == wardId).ToListAsync();
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.Message);
                Log.WriteLine(ex.StackTrace);
                return new List<StreetModel>();
            }
        }
    }
}
