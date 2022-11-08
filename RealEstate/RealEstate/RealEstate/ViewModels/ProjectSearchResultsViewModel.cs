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
    public class ProjectSearchResultsViewModel : BaseViewModel
    {
        SearchProjectRequest currentRequest;

        public ProjectSearchResultsViewModel(SearchProjectRequest request)
        {
            currentRequest = request;
            projects = new ObservableRangeCollection<ProjectModel>();
            LoadMoreCommand.Execute(null);
        }

        private Command loadMoreCommand;
        public Command LoadMoreCommand
        {
            get { return loadMoreCommand ?? (loadMoreCommand = new Command(async () => await LoadMoreAsync())); }
        }

        private ObservableRangeCollection<ProjectModel> projects;
        public ObservableRangeCollection<ProjectModel> Projects
        {
            get
            {
                ObservableRangeCollection<ProjectModel> _projects = new ObservableRangeCollection<ProjectModel>();
                if (Sort == 0)
                {
                    _projects.AddRange(projects.OrderByDescending(s => s.Created));
                }
                else if (Sort == 1)
                {
                    _projects.AddRange(projects.OrderBy(s => s.Created));
                }
                else
                {
                    _projects.AddRange(projects);
                }
                return _projects;
            }
        }

        int sort;
        public int Sort
        {
            get { return sort; }
            set { SetProperty(ref sort, value); OnPropertyChanged("Projects"); }
        }

        public int ProjectCount
        {
            get { return projects.Count; }
        }
        public async Task Save(long id)
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
                    var project = projects.FirstOrDefault(r => r.Id == id);
                    if (project != null)
                    {
                        project.IsSaved = !project.IsSaved;
                    }
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task LoadMoreAsync()
        {
            if (IsBusy)
            {
                return;
            }
            try
            {
                IsBusy = true;

                var service = DependencyService.Get<IProjectService>();
                var res = await service.GetProjectsAsync(currentRequest);
                if (res.Success)
                {
                    currentRequest.Start += res.Data.Length;
                    var data = res.Data.Select(d => new ProjectModel()
                    {
                        Acreage = d.Acreage,
                        Created = d.Created,
                        Address = d.Address,
                        Id = d.Id,
                        IsSaved = d.IsSaved == 1,
                        Name = d.Name,
                        Thumbnail = d.Thumbnail,
                    });
                    projects.InsertRange(0, data);
                    OnPropertyChanged("ProjectCount");
                    OnPropertyChanged("Projects");
                }
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
