using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using RealEstate.Models;
using RealEstate.Services;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

namespace RealEstate.ViewModels
{
    public class SavedViewModel : BaseViewModel
    {
        public SavedViewModel()
        {
            RealDatas = new ObservableRangeCollection<RealDataModel>();
            Projects = new ObservableRangeCollection<ProjectModel>();
            LoadNewsSaved = new Command(async () => await DoLoadSavedAsnnc());
            LoadProjectSaved = new Command(async () => await DoLoadProjectSavedAsnnc());
        }

        public ObservableRangeCollection<RealDataModel> RealDatas { get; }
        public ObservableRangeCollection<ProjectModel> Projects { get; }

        public Command LoadNewsSaved { get; }

        public Command LoadProjectSaved { get; }

        public int RealCount
        {
            get { return RealDatas.Count; }
        }

        public int ProjectCount
        {
            get { return Projects.Count; }
        }

        bool isReal;
        public bool IsReal
        {
            get { return isReal; }
            set { SetProperty(ref isReal, value); }
        }

        bool isProject;
        public bool IsProject
        {
            get { return isProject; }
            set { SetProperty(ref isProject, value); }
        }

        async Task DoLoadSavedAsnnc()
        {
            if (IsBusy)
            {
                return;
            }
            IsBusy = true;
            try
            {
                var service = DependencyService.Get<IRealEstateService>();
                var res = await service.GetSavedRealEstates(RealCount);
                if (res.Success)
                {
                    RealDatas.AddRange(res.Data.Select(d => new RealDataModel()
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
                        Images = d.Images,
                        Longitude = d.Longitude,
                        Latitude = d.Latitude,
                        StartAt = d.StartAt,
                        EndAt = d.EndAt
                    }));
                    OnPropertyChanged("RealCount");
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.StackTrace); }
            finally
            {
                IsBusy = false;
            }
        }

        async Task DoLoadProjectSavedAsnnc()
        {
            if (IsBusy)
            {
                return;
            }
            IsBusy = true;
            try
            {
                var service = DependencyService.Get<IProjectService>();
                var res = await service.GetSavedProjectsAsync(ProjectCount);
                if (res.Success)
                {
                    Projects.AddRange(res.Data.Select(d => new ProjectModel()
                    {
                        Acreage = d.Acreage,
                        Address = d.Address,
                        Id = d.Id,
                        Created = d.Created,
                        IsSaved = true,
                        Name = d.Name,
                        Thumbnail = d.Thumbnail
                    }));
                    OnPropertyChanged("ProjectCount");
                }
            }
            finally
            {
                IsBusy = false;
            }
        }


        public async Task SaveProject(long id)
        {
            if (IsBusy)
            {
                return;
            }
            try
            {
                IsBusy = true;

                var service = DependencyService.Get<IProjectService>();
                var success = await service.ChangeSaveStateProjectAsync(id);
                if (success)
                {
                    var project = Projects.FirstOrDefault(r => r.Id == id);
                    if (project != null)
                    {
                        project.IsSaved = !project.IsSaved;
                        Projects.Remove(project);
                        OnPropertyChanged("ProjectCount");
                    }
                }
            }
            finally
            {
                IsBusy = false;
            }
        }


        public async Task SaveNews(long id)
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
                    var real = RealDatas.FirstOrDefault(r => r.Id == id);
                    if (real != null)
                    {
                        real.IsSaved = !real.IsSaved;
                        RealDatas.Remove(real);
                        OnPropertyChanged("RealCount");
                    }
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void ChangeState(bool _isProject)
        {
            IsProject = _isProject;
            IsReal = !_isProject;
            if (_isProject && ProjectCount <= 0)
            {
                LoadProjectSaved.Execute(null);
            }
            if (_isProject && RealCount <= 0)
            {
                LoadNewsSaved.Execute(null);
            }
        }
    }
}
