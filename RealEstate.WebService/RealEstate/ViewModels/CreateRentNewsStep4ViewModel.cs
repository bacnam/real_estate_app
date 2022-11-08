using System;
using System.Collections.Generic;
using System.Text;
using RealEstate.Core.Requests;
using RealEstate.Models;
using RealEstate.Services;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

namespace RealEstate.ViewModels
{
    public class CreateRentNewsStep4ViewModel : BaseViewModel
    {
        public CreateRentNewsStep4ViewModel(CreateRentNewsModel rentNews)
        {
            RentNews = rentNews;
        }

        CreateRentNewsModel rentNews;
        public CreateRentNewsModel RentNews { get { return rentNews; } set { SetProperty(ref rentNews, value); } }

        public async Task<bool> DoDone()
        {
            bool success = false;
            if (IsBusy)
            {
                return success;
            }
            IsBusy = true;
            try
            {
                IRealEstateService estateService = DependencyService.Get<IRealEstateService>();
                CreateEstateRequest request = new CreateEstateRequest()
                {
                    EndAt = RentNews.EndDate,
                    StartAt = RentNews.StartDate,
                    Title = RentNews.Title,
                    Acreage = RentNews.Acreage,
                    Address = RentNews.Address,
                    ContactName = RentNews.ContactName,
                    ContactPhone = RentNews.ContactPhone,
                    Description = RentNews.Desciption,
                    DirectionId = RentNews.Direction.Id,
                    Latitude = RentNews.Latitude,
                    Longitude = RentNews.Longitude,
                    Price = RentNews.GetPrice(),
                    ProjectId = RentNews.Project.Id,
                    WardId = RentNews.Ward.Id,
                    RealNewsTypeId = RentNews.RealType.Id,
                    PriceType = RentNews.UnitPrice.Name,
                    Images = RentNews.Images.Select(i => new Core.Requests.ImageSource()
                    {
                        Id = i.Id,
                        Source = i.Source
                    }).ToArray()
                };
                var res = await estateService.CreateEstateAsync(request);
                success = res.Success;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                IsBusy = false;
            }
            return success;
        }
    }
}
