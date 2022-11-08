using Microsoft.EntityFrameworkCore;
using RealEstate.Core.Responses;
using RealEstate.WebService.Databases;
using RealEstate.WebService.Models;

namespace RealEstate.WebService.Services
{
    public interface IProjectService
    {
        Task<SearchProjectResponse> GetProjectsByAccountAsync(long accountId, string city, string district, int start = 0);
        Task<ProjectResponse[]> GetProjectsAsync();
        Task<GetProjectDetailsResponse> GetProjectDetailsAsync(long accountId, long projectId);
        Task<bool> ChangeProjectSaveAsync(long accountId, long id);
        Task<SearchProjectResponse> GetSavedProjectsAsync(long accountId, int start = 0);
    }

    public class ProjectService : BaseService, IProjectService
    {
        public ProjectService(LibraryContext context) : base(context)
        {
        }

        public async Task<bool> ChangeProjectSaveAsync(long accountId, long id)
        {
            var project = await Context.Projects.FirstOrDefaultAsync(r => r.Id == id);
            if (project != null)
            {
                var save = Context.AccountProjects.FirstOrDefault(r => r.ProjectId == project.Id && r.AccountId == accountId);
                if (save != null)
                {
                    Context.AccountProjects.Remove(save);
                }
                else
                {
                    save = new AccountProjectModel()
                    {
                        AccountId = accountId,
                        ProjectId = project.Id
                    };
                    Context.AccountProjects.Add(save);
                }
                Context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<GetProjectDetailsResponse> GetProjectDetailsAsync(long accountId, long projectId)
        {
            GetProjectDetailsResponse response = new GetProjectDetailsResponse();

            var project = await Context.Projects.FirstOrDefaultAsync(r => r.Id == projectId);
            if (project != null)
            {
                var saved = Context.AccountProjects.Count(rs => rs.ProjectId == projectId && rs.AccountId == accountId);
                response.Success = true;
                response.DetailsData = new ProjectDetailsData()
                {
                    Acreage = project.Acreage,
                    AcreageFloor = project.AcreageFloor,
                    Address = project.Address,
                    ApartmentNumber = project.ApartmentNumber,
                    Created = project.Created,
                    Description = project.Description,
                    Floor = project.Floor,
                    Id = project.Id,
                    IsSaved = (ushort)(saved > 0 ? 1 : 0),
                    Name = project.Name,
                    Thumbnail = project.Thumbnail,
                    HandedOver = project.HandedOver,
                    Investor = project.Investor
                };
            }
            return response;
        }

        public async Task<ProjectResponse[]> GetProjectsAsync()
        {
            var projects = Context.Projects
                    .Select(d => new ProjectResponse()
                    {
                        Id = d.Id,
                        Name = d.Name,
                    });
            return await projects.OrderBy(s => s.Id).ToArrayAsync();
        }

        public async Task<SearchProjectResponse> GetProjectsByAccountAsync(long accountId, string city, string district, int start = 0)
        {
            SearchProjectResponse response = new SearchProjectResponse() { Success = true };
            var data = Context.Projects.Select(p => p);
            var cities = city.Split(",").Select(c => int.Parse(c));
            var districts = district.Split(",").Select(c => int.Parse(c));
            if (districts.Count() > 0 && districts.First() != 0)
            {
                data = from d1 in data
                       from d2 in districts
                       where d1.District.Id == d2
                       select d1;
            }
            else if (cities.Count() > 0 && cities.First() != 0)
            {
                data = from d1 in data
                       from d2 in cities
                       where d1.District.City.Id == d2
                       select d1;
            }
            var saved = from d1 in data
                        from d2 in Context.AccountProjects
                        where d1.Id == d2.ProjectId && d2.AccountId == accountId
                        select new ProjectData()
                        {
                            Id = d1.Id,
                            Acreage = d1.Acreage,
                            Address = d1.Address,
                            Name = d1.Name,
                            IsSaved = 1
                        };
            var notSaved = data.Where(d1 => saved.Count(d2 => d2.Id == d1.Id) <= 0)
                .Select(d => new ProjectData()
                {
                    Id = d.Id,
                    Acreage = d.Acreage,
                    Address = d.Address,
                    Name = d.Name,
                    IsSaved = 0,
                    Thumbnail = d.Thumbnail
                });
            response.Data = await notSaved.Concat(saved).OrderBy(s => s.Id).Skip(start).Take(10).ToArrayAsync();
            return response;
        }

        public async Task<SearchProjectResponse> GetSavedProjectsAsync(long accountId, int start = 0)
        {
            SearchProjectResponse response = new SearchProjectResponse() { Success = true };

            var saved = from d1 in Context.Projects
                        from d2 in Context.AccountProjects
                        where d1.Id == d2.ProjectId && d2.AccountId == accountId
                        select new ProjectData()
                        {
                            Id = d1.Id,
                            Acreage = d1.Acreage,
                            Address = d1.Address,
                            Name = d1.Name,
                            Thumbnail = d1.Thumbnail,
                            IsSaved = 1
                        };
            response.Success = true;
            response.Data = await saved.OrderBy(s => s.Id).Skip(start).Take(10).ToArrayAsync();
            return response;
        }
    }
}
