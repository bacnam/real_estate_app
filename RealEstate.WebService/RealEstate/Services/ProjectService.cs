using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Core.Requests;
using RealEstate.Core.Responses;

namespace RealEstate.Services
{
    public interface IProjectService
    {
        Task<SearchProjectResponse> GetProjectsAsync(SearchProjectRequest request);
        Task<bool> ChangeSaveStateProjectAsync(long id);
        Task<GetProjectDetailsResponse> GetProjectDetailsAsync(GetProjectDetailsRequest request);
        Task<SearchProjectResponse> GetSavedProjectsAsync(int start = 0);
    }

    public class ProjectService : IProjectService
    {

        public async Task<bool> ChangeSaveStateProjectAsync(long id)
        {
            var service = Xamarin.Forms.DependencyService.Get<IAPIService>();
            ChangeSaveStateProjectRequest request = new ChangeSaveStateProjectRequest();
            request.ProjectId = id;
            var res = await service.PutAsync<ChangeSaveStateProjectResponse>("projects/save", request.ToJson());
            return res.Success;
        }

        public Task<GetProjectDetailsResponse> GetProjectDetailsAsync(GetProjectDetailsRequest request)
        {
            var service = Xamarin.Forms.DependencyService.Get<IAPIService>();
            return service.GetAsync<GetProjectDetailsResponse>("projects/" + request.ProjectId.ToString());
        }

        public Task<SearchProjectResponse> GetProjectsAsync(SearchProjectRequest request)
        {
            var service = Xamarin.Forms.DependencyService.Get<IAPIService>();
            return service.PostAsync<SearchProjectResponse>("projects", request.ToJson());
        }

        public Task<SearchProjectResponse> GetSavedProjectsAsync(int start = 0)
        {
            var service = Xamarin.Forms.DependencyService.Get<IAPIService>();
            return service.GetAsync<SearchProjectResponse>(string.Format("projects/saved/?start={0}", start));
        }
    }
}
