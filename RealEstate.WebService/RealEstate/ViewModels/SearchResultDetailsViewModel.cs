using System;
using System.Collections.Generic;
using System.Text;
using RealEstate.Models;
using RealEstate.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RealEstate.ViewModels
{
    public class SearchResultDetailsViewModel : BaseViewModel
    {
        private long RealEstateId;

        public SearchResultDetailsViewModel(long realEstateId)
        {
            RealEstateId = realEstateId;
            realData = new RealDataModel();
            LoadDetailsCommand = new Command(() => DoLoadDetailsCommandAsync().ConfigureAwait(false));
        }

        public Command LoadDetailsCommand
        {
            get;
        }

        private RealDataModel realData;
        public RealDataModel RealData
        {
            get
            {
                return realData;
            }
            set
            {
                SetProperty(ref realData, value);
            }
        }

        public async Task Save()
        {
            if (IsBusy)
            {
                return;
            }
            try
            {
                IsBusy = true;

                var service = DependencyService.Get<IRealEstateService>();
                var success = await service.ChangeRealEstateAsync(RealData.Id);
                if (success)
                {
                    RealData.IsSaved = !RealData.IsSaved;
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task DoLoadDetailsCommandAsync()
        {
            if (IsBusy)
            {
                return;
            }
            try
            {
                IsBusy = true;

                var service = DependencyService.Get<IRealEstateService>();
                var res = await service.GetRealEstate(RealEstateId);
                if (res.Success)
                {
                    var data = res.DataDetails;
                    if (data != null)
                    {
                        RealData = new RealDataDetailsModel()
                        {
                            Acreage = data.Acreage,
                            Address = data.Address,
                            Direction = data.Direction,
                            Floor = data.Floor,
                            Id = data.Id,
                            IsSaved = data.IsSaved == 1,
                            Price = data.Price,
                            PriceType = data.PriceType,
                            RealType = data.RealType,
                            Room = data.Room,
                            Longitude = data.Longitude,
                            Latitude = data.Latitude,
                            Images = data.Images,
                            StartAt = data.StartAt,
                            EndAt = data.EndAt,
                            Contact = data.Contact,
                            ContactAvatar = data.ContactAvatar,
                            ContactPhone = data.ContactPhone,
                            Description = data.Description,
                            Project = data.Project,
                        };
                    }
                }
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
