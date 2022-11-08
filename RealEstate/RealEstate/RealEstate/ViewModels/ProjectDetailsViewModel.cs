using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using RealEstate.Core.Requests;
using RealEstate.Models;
using RealEstate.Services;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

namespace RealEstate.ViewModels
{
    public class ProjectDetailsViewModel : BaseViewModel
    {
        private SearchRequest currentRequest;
        public ProjectDetailsViewModel(long projectId)
        {
            currentRequest = new SearchRequest()
            {
                Project = projectId,
                Start = 0
            };
            IsDetails = true;
            reals = new ObservableRangeCollection<RealDataModel>();
            project = new ProjectDetailsModel();
        }

        bool isDetails;
        public bool IsDetails
        {
            get
            {
                return isDetails;
            }
            set
            {
                SetProperty(ref isDetails, value);
                OnPropertyChanged("IsReal");
                if (value)
                {
                    GetDetails();
                }
                else
                {
                    LoadMoreCommand.Execute(null);
                }
            }
        }

        public Command LoadMoreCommand
        {
            get
            {
                return new Command(async () => await GetReals());
            }
        }

        public bool IsReal { get { return !IsDetails; } }

        ProjectDetailsModel project;
        public ProjectDetailsModel Project { get { return project; } }

        public int RealCount
        {
            get { return reals.Count(); }
        }

        private ObservableRangeCollection<RealDataModel> reals;
        public ObservableRangeCollection<RealDataModel> Reals
        {
            get
            {
                return reals;
            }
        }

        private async Task GetReals()
        {
            if (IsBusy)
            {
                return;
            }
            try
            {
                IsBusy = true;

                var service = Xamarin.Forms.DependencyService.Get<IRealEstateService>();
                var res = await service.GetRealEstateByProject(currentRequest);
                if (res.Success)
                {
                    currentRequest.Start += res.Data.Length;
                    var data = res.Data.Select(d => new RealDataModel()
                    {
                        Acreage = d.Acreage,
                        Address = d.Address,
                        Direction = d.Direction,
                        Floor = d.Floor,
                        Id = d.Id,
                        IsSaved = d.IsSaved == 1,
                        Price = d.Price,
                        PriceType = d.PriceType,
                        RealType = d.RealType,
                        Room = d.Room,
                        Longitude = d.Longitude,
                        Latitude = d.Latitude,
                        Images = d.Images,
                        StartAt = d.StartAt,
                        EndAt = d.EndAt
                    });
                    reals.InsertRange(0, data);
                    OnPropertyChanged("RealCount");
                    OnPropertyChanged("Reals");
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task GetDetails()
        {
            if (IsBusy)
            {
                return;
            }
            try
            {
                IsBusy = true;

                var service = Xamarin.Forms.DependencyService.Get<IProjectService>();
                GetProjectDetailsRequest request = new GetProjectDetailsRequest() { ProjectId = currentRequest.Project };
                var res = await service.GetProjectDetailsAsync(request);
                if (res.Success)
                {
                    Project.Acreage = res.DetailsData.Acreage;
                    Project.AcreageFloor = res.DetailsData.Acreage;
                    Project.ApartmentNumber = res.DetailsData.ApartmentNumber;
                    Project.Address = res.DetailsData.Address;
                    Project.Description = res.DetailsData.Description;
                    Project.Floor = res.DetailsData.Floor;
                    Project.Name = res.DetailsData.Name;
                    project.HandedOver = res.DetailsData.HandedOver;
                    project.Investor = res.DetailsData.Investor;
                    project.Thumbnail = res.DetailsData.Thumbnail;
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task SaveReal(long id)
        {
            if (IsBusy)
            {
                return;
            }
            try
            {
                IsBusy = true;

                var service = DependencyService.Get<IRealEstateService>();
                var success = await service.ChangeRealEstateAsync(id);
                if (success)
                {
                    var real = reals.FirstOrDefault(r => r.Id == id);
                    if (real != null)
                    {
                        real.IsSaved = !real.IsSaved;
                    }
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task SaveProject()
        {
            if (IsBusy)
            {
                return;
            }
            try
            {
                IsBusy = true;

                var service = DependencyService.Get<IProjectService>();
                var success = await service.ChangeSaveStateProjectAsync(currentRequest.Project);
                if (success)
                {
                    project.IsSaved = !project.IsSaved;
                }
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
