using GoogleApi.Entities.Common.Enums;
using GoogleApi.Entities.Maps.Geocoding.Address.Request;
using Microsoft.EntityFrameworkCore;
using RealEstate.Core.Responses;
using RealEstate.WebService.Databases;
using RealEstate.WebService.Models;

namespace RealEstate.WebService.Services
{
    public interface ISearchService
    {
        Task<SearchResponse> SearchAsync(long accountId,
            string city,
            string district,
            string ward,
            string realType,
            int room,
            double fromPrice,
            double toPrice,
            double fromAcreage,
            double toAcreage,
            bool isSale = false,
            int start = 0);
        Task<SearchResponse> SearchByProjectAsync(long accountId, long projectId, int start = 0);
        Task<SearchAddressResponse> SearchAddressAsync(string address);
    }

    public class SearchService : BaseService, ISearchService
    {
        public SearchService(LibraryContext context) : base(context)
        {
        }

        public async Task<SearchResponse> SearchAsync(long accountId, string city,
            string district, string ward, string realType,
            int room, double fromPrice,
            double toPrice,
            double fromAcreage,
            double toAcreage, bool isSale = false, int start = 0)
        {
            SearchResponse response = new SearchResponse() { Success = true };
            var data = Context.RealEstates.Include(r => r.RealNewsType)
                            .Include(r => r.Images)
                            .Include(r => r.Direction)
                            .Where(r => r.IsSale == isSale);
            if (room > 0)
            {
                data = data.Where(r => r.Room == room);
            }
            if (fromPrice > 0 && toPrice > 0)
            {
                data = data.Where(r => r.Price >= fromPrice && r.Price <= toPrice);
            }
            else if (fromPrice > 0 && toPrice < 0)
            {
                data = data.Where(r => r.Price >= fromPrice);
            }
            if (fromAcreage > 0 && toAcreage > 0)
            {
                data = data.Where(r => r.Acreage >= fromAcreage && r.Acreage <= toAcreage);
            }
            var cities = city.Split(",").Select(c => int.Parse(c));
            var districts = district.Split(",").Select(c => int.Parse(c));
            var wards = ward.Split(",").Select(c => int.Parse(c));
            if (wards.Count() > 0 && wards.First() != 0)
            {
                data = from d1 in data
                       from d2 in wards
                       where d1.Ward.Id == d2
                       select d1;
            }
            else if (districts.Count() > 0 && districts.First() != 0)
            {
                data = from d1 in data
                       from d2 in districts
                       where d1.Ward.District.Id == d2
                       select d1;
            }
            else if (cities.Count() > 0 && cities.First() != 0)
            {
                data = from d1 in data
                       from d2 in cities
                       where d1.Ward.District.City.Id == d2
                       select d1;
            }
            var datas = await data.OrderBy(s => s.Id).Skip(start).Take(10)
                .Select(r => new RealData()
                {
                    Id = r.Id,
                    Acreage = r.Acreage,
                    Address = r.Address,
                    FullAddress = r.GetFullAddress(),
                    Price = r.Price,
                    PriceType = r.PriceType,
                    Images = r.Images.Select(img => img.FileName).ToArray(),
                    RealType = r.RealNewsType.Name,
                    Direction = r.Direction.Name,
                    StartAt = r.StartAt,
                    EndAt = r.EndAt,
                    Latitude = r.Latitude,
                    Longitude = r.Longitude,
                    IsSaved = 0
                }).ToListAsync();
            var saved = await (from r1 in Context.AccountRealEstate
                               from r2 in datas
                               where r1.RealEstateId == r2.Id && r1.AccountId == accountId
                               select r2).ToListAsync();
            foreach (var item in saved)
            {
                item.IsSaved = 1;
            }
            await GetLocationOnGoogle(datas);
            response.Data = datas.ToArray();
            return response;
        }

        public async Task<SearchResponse> SearchByProjectAsync(long accountId, long projectId, int start = 0)
        {
            SearchResponse response = new SearchResponse() { Success = true };
            var data = Context.RealEstates.Include(r => r.RealNewsType)
                            .Include(r => r.AccountRealNews)
                            .Include(r => r.Direction)
                            .Include(r => r.Images)
                            .Where(r => r.ProjectId == projectId);
            var saved = from d1 in data
                        from d2 in Context.AccountRealEstate
                        where d1.Id == d2.RealEstateId && d2.AccountId == accountId
                        select new RealData()
                        {
                            Id = d1.Id,
                            Acreage = d1.Acreage,
                            Address = d1.Address,
                            Price = d1.Price,
                            PriceType = d1.PriceType,
                            Images = d1.Images.Select(img => img.FileName).ToArray(),
                            RealType = d1.RealNewsType.Name,
                            Direction = d1.Direction.Name,
                            StartAt = d1.StartAt,
                            EndAt = d1.EndAt,
                            Latitude = d1.Latitude,
                            Longitude = d1.Longitude,
                            IsSaved = 1
                        };
            var notSaved = data.Where(d1 => saved.Count(d2 => d2.Id == d1.Id) <= 0)
                .Select(d => new RealData()
                {
                    Id = d.Id,
                    Acreage = d.Acreage,
                    Address = d.Address,
                    Price = d.Price,
                    PriceType = d.PriceType,
                    Images = d.Images.Select(img => img.FileName).ToArray(),
                    RealType = d.RealNewsType.Name,
                    Direction = d.Direction.Name,
                    StartAt = d.StartAt,
                    EndAt = d.EndAt,
                    Latitude = d.Latitude,
                    Longitude = d.Longitude,
                    IsSaved = 0
                });
            response.Data = await notSaved.Concat(saved).OrderBy(s => s.Id).Skip(start).Take(10).ToArrayAsync();
            return response;
        }

        public async Task<SearchAddressResponse> SearchAddressAsync(string address)
        {
            SearchAddressResponse response = new SearchAddressResponse();
            AddressGeocodeRequest request = new AddressGeocodeRequest() { Address = address, Key = Key };
            var res = await GoogleApi.GoogleMaps.Geocode.AddressGeocode.QueryAsync(request);
            if (res.Status == GoogleApi.Entities.Common.Enums.Status.Ok)
            {
                var result = res.Results.FirstOrDefault();
                if (result != null)
                {
                    response.Success = true;
                    response.Latitude = result.Geometry.Location.Latitude;
                    response.Longitude = result.Geometry.Location.Longitude;
                }
            }
            return response;
        }

        const string Key = "AIzaSyAkA9LkurNJ8-0dE2CLzxEaXmGpW-zp8mc";
        private async Task GetLocationOnGoogle(IEnumerable<RealData> source)
        {
            foreach (var readlData in source)
            {
                if (readlData.Latitude <= 0 && readlData.Longitude <= 0)
                {

                    AddressGeocodeRequest request = new AddressGeocodeRequest() { Address = readlData.Address, Key = Key };
                    
                    var res = await GoogleApi.GoogleMaps.Geocode.AddressGeocode.QueryAsync(request);
                    if (res.Status == GoogleApi.Entities.Common.Enums.Status.Ok)
                    {
                        var result = res.Results.FirstOrDefault();
                        if (result != null)
                        {
                            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                            if (result.AddressComponents.Count() > 1)
                            {
                                var cityAddress = result.AddressComponents.ElementAt(1);
                            }
                            if (result.AddressComponents.Count() > 2)
                            {
                                var districtAddress = result.AddressComponents.ElementAt(2);
                            }
                            if (result.AddressComponents.Count() > 3)
                            {
                                var wardAddress = result.AddressComponents.ElementAt(3);
                            }
                            foreach (var addressComponent in result.AddressComponents)
                            {
                                WardModel ward = null;
                                DistrictModel district = null;
                                CityModel city = null;
                                if (addressComponent.Types.Count(t => t >= AddressComponentType.Sublocality && t <= AddressComponentType.Sublocality_Level_5) > 0)
                                {
                                    string wardName = addressComponent.LongName;
                                    ward = Context.Wards.FirstOrDefault(w => w.Name.Contains(wardName));
                                }
                                if (addressComponent.Types.Count(t => t == AddressComponentType.Administrative_Area_Level_1) > 0)
                                {
                                    string cityName = addressComponent.LongName;
                                    district = Context.Districts.FirstOrDefault(w => w.Name.Contains(cityName));
                                }
                            }
                            Console.WriteLine(string.Format("{0}/{1}", result.Geometry.Location.Latitude, result.Geometry.Location.Longitude));
                            var realEstate = await Context.RealEstates.FirstOrDefaultAsync(r => r.Id == readlData.Id);
                            realEstate.Latitude = result.Geometry.Location.Latitude;
                            realEstate.Longitude = result.Geometry.Location.Longitude;
                            await Context.SaveChangesAsync();

                            readlData.Latitude = result.Geometry.Location.Latitude;
                            readlData.Longitude = result.Geometry.Location.Longitude;
                        }
                    }

                    Thread.Sleep(1000);
                }
            }
        }
    }
}
