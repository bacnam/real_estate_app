using System;
using System.Collections.Generic;
using System.Text;
using RealEstate.Core.Requests;
using RealEstate.Core.Responses;
using RealEstate.Models;
using System.Threading.Tasks;

namespace RealEstate.ViewModels
{
    public class RegisterReceiveNewsStep2ViewModel : BaseViewModel
    {
        public RegisterReceiveNewsStep2ViewModel(RegisterNewsModel registerNews)
        {
            RegisterNews = registerNews;
        }

        RegisterNewsModel _registerNews;
        public RegisterNewsModel RegisterNews { get { return _registerNews; } set { SetProperty(ref _registerNews, value); } }

        public async Task<bool> RegisterAsync()
        {
            bool success = false;
            if (IsBusy)
            {
                return success;
            }
            IsBusy = true;
            try
            {
                var api = Xamarin.Forms.DependencyService.Get<Services.IAPIService>();
                RegisterNewsRequest request = new RegisterNewsRequest()
                {
                    CityId = RegisterNews.City.Id,
                    DirectionId = RegisterNews.Direction.Id,
                    DistrictId = RegisterNews.District.Id,
                    WardId = RegisterNews.Ward.Id,
                    Email = RegisterNews.Email,
                    FromPrice = 0,
                    ProjectId = RegisterNews.Project.Id,
                    RealTypeId = RegisterNews.RealType.Id,
                    ToPrice = 0
                };
                var res = await api.PostAsync<RegisterNewsResponse>("news/register", request.ToJson());
                success = res.Success;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
            return success;
        }
    }
}
