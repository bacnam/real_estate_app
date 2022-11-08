using System;
using System.Collections.Generic;
using System.Text;
using RealEstate.Models;
using RealEstate.Services;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace RealEstate.ViewModels
{
    public class CreateRentNewsViewModel : BaseViewModel
    {
        public CreateRentNewsViewModel(bool isSale)
        {
            RentNews = new CreateRentNewsModel();
            RentNews.City = new CityModel()
            {
                Name = "Chọn tỉnh thành"
            };
            RentNews.District = new DistrictModel()
            {
                Name = "Chọn quận huyện"
            };
            RentNews.Ward = new WardModel()
            {
                Name = "Chọn xã phường"
            };
            RentNews.Project = new ProjectModel()
            {
                Name = "Chọn dự án"
            };

            RentNews.RealType = new RealTypeModel()
            {
                Title = "Chọn loại bất động sản"
            };
            RentNews.Direction = new DirectionModel()
            {
                Name = "Chọn hướng nhà"
            };

            RentNews.IsSale = isSale;
            if (isSale)
            {
                RentNews.HeaderTitle = "Đăng tin bán";
            }
            else
            {
                RentNews.HeaderTitle = "Đăng tin cho thuê";
            }
        }

        CreateRentNewsModel rentNews;
        public CreateRentNewsModel RentNews { get { return rentNews; } set { SetProperty(ref rentNews, value); } }

        public Action<Position> SearchAddressDone;

        public Command SearchAddressCommand
        {
            get
            {
                return new Command(() => DoSearchAddressCommand().ConfigureAwait(false));
            }
        }

        private async Task DoSearchAddressCommand()
        {
            if (IsBusy)
            {
                return;
            }
            IsBusy = true;
            try
            {
                ISearchService service = DependencyService.Get<ISearchService>();
                var response = await service.SearchAddressAsync(RentNews.FullAddress);

                RentNews.Longitude = response.Longitude;
                RentNews.Latitude = response.Latitude;
                Position position = new Position(response.Latitude, response.Longitude);
                SearchAddressDone?.Invoke(position);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
