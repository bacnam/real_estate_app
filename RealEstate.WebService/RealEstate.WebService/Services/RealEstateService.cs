using Microsoft.EntityFrameworkCore;
using RealEstate.Core.Requests;
using RealEstate.Core.Responses;
using RealEstate.WebService.Databases;
using RealEstate.WebService.Models;

namespace RealEstate.WebService.Services
{
    public interface IRealEstateService
    {
        Task<bool> ChangeRealEstatesSave(long accountId, long id);
        Task<SearchResponse> GetSavedRealEstates(long accountId, int start = 0);
        Task<GetRealDataDetailsResponse> GetRealEstate(long accountId, long realEstateId);
        Task<CreateEstateResponse> CreateRealEstate(long accountId, CreateEstateRequest request);
        Task<SearchResponse> GetMyRealEstatesAsync(long accountId, bool isSale);
        Task<List<RealEstateModel>> GetRealEstatesAsync();
    }

    public class RealEstateService : BaseService, IRealEstateService
    {
        IImageService ImageService;
        public RealEstateService(IImageService imageService, LibraryContext context) : base(context)
        {
            ImageService = imageService;
        }

        public async Task<bool> ChangeRealEstatesSave(long accountId, long id)
        {
            var realEstates = await Context.RealEstates.FirstOrDefaultAsync(r => r.Id == id);
            if (realEstates != null)
            {
                var save = Context.AccountRealEstate.FirstOrDefault(r => r.RealEstateId == realEstates.Id && r.AccountId == accountId);
                if (save != null)
                {
                    Context.AccountRealEstate.Remove(save);
                }
                else
                {
                    save = new AccountRealEstateModel()
                    {
                        AccountId = accountId,
                        RealEstateId = realEstates.Id
                    };
                    Context.AccountRealEstate.Add(save);
                }
                Context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<CreateEstateResponse> CreateRealEstate(long accountId, CreateEstateRequest request)
        {
            CreateEstateResponse response = new CreateEstateResponse();

            try
            {
                var direction = Context.Directions.FirstOrDefault(d => d.Id == request.DirectionId);
                var ward = Context.Wards.FirstOrDefault(w => w.Id == request.WardId);
                var realType = await Context.RealEstateTypes.FirstOrDefaultAsync(rt => rt.Id == request.RealNewsTypeId);
                RealEstateModel realEstate = new RealEstateModel()
                {
                    Acreage = request.Acreage,
                    Address = request.Address,
                    Contact = request.ContactName,
                    ContactPhone = request.ContactPhone,
                    Description = request.Description,
                    Direction = direction,
                    Latitude = request.Latitude,
                    EndAt = request.EndAt,
                    Longitude = request.Longitude,
                    Price = request.Price,
                    ProjectId = request.ProjectId,
                    StartAt = request.StartAt,
                    Title = request.Title,
                    Ward = ward,
                    IsSale = request.IsSale,
                    RealNewsType = realType,
                    PriceType = request.PriceType,
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow
                };
                Context.RealEstates.Add(realEstate);
                await Context.SaveChangesAsync();

                await ImageService.CreateImageAsync(request.Images, realEstate.Id);

                response.Success = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);

                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<SearchResponse> GetMyRealEstatesAsync(long accountId, bool isSale)
        {
            SearchResponse response = new SearchResponse() { Success = true };
            var data = Context.RealEstates.Include(r => r.RealNewsType)
                            .Include(r => r.AccountRealNews)
                            .Include(r => r.Direction)
                            .Include(r => r.Images)
                            .Where(r => r.CreatorId == accountId && r.IsSale == isSale);
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
            response.Data = await notSaved.Concat(saved).OrderBy(s => s.Id).ToArrayAsync();
            return response;
        }

        public async Task<GetRealDataDetailsResponse> GetRealEstate(long accountId, long realEstateId)
        {
            GetRealDataDetailsResponse response = new GetRealDataDetailsResponse();
            try
            {
                var realEstate = await Context.RealEstates
                    .Include(r => r.RealNewsType).Include(r => r.Direction).Include(r => r.Images)
                    .FirstOrDefaultAsync(r => r.Id == realEstateId);
                if (realEstate != null)
                {
                    var saved = Context.AccountRealEstate.Count(rs => rs.RealEstateId == realEstateId && rs.AccountId == accountId);
                    var project = Context.Projects.FirstOrDefault(p => p.Id == realEstate.ProjectId);
                    response.DataDetails = new RealDataDetails()
                    {
                        Id = realEstate.Id,
                        Acreage = realEstate.Acreage,
                        Address = realEstate.Address,
                        Price = realEstate.Price,
                        PriceType = realEstate.PriceType,
                        Images = realEstate.Images.Select(img => img.FileName).ToArray(),
                        RealType = realEstate.RealNewsType.Name,
                        Direction = realEstate.Direction.Name,
                        Description = realEstate.Description,
                        Project = project == null ? "Không xác định" : project.Name,
                        StartAt = realEstate.StartAt,
                        EndAt = realEstate.EndAt,
                        Latitude = realEstate.Latitude,
                        Longitude = realEstate.Longitude,
                        IsSaved = (ushort)saved,
                        Contact = realEstate.Contact,
                        ContactPhone = realEstate.ContactPhone
                    };
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.Message);
                Log.WriteLine(ex.StackTrace);
            }
            return response;
        }

        public Task<List<RealEstateModel>> GetRealEstatesAsync()
        {
            return Context.RealEstates.ToListAsync();
        }

        public async Task<SearchResponse> GetSavedRealEstates(long accountId, int start = 0)
        {
            SearchResponse response = new SearchResponse();
            try
            {
                var realEstates = from r1 in Context.AccountRealEstate
                                  from r2 in Context.RealEstates.Include(r => r.Images)
                                  where r1.AccountId == accountId && r2.Id == r1.RealEstateId
                                  select new RealData()
                                  {
                                      Id = r2.Id,
                                      Acreage = r2.Acreage,
                                      Address = r2.Address,
                                      Price = r2.Price,
                                      PriceType = r2.PriceType,
                                      Images = r2.Images.Select(img => img.FileName).ToArray(),
                                      RealType = r2.RealNewsType.Name,
                                      Direction = r2.Direction.Name,
                                      StartAt = r2.StartAt,
                                      EndAt = r2.EndAt,
                                      Latitude = r2.Latitude,
                                      Longitude = r2.Longitude,
                                      IsSaved = 1
                                  };

                response.Success = true;
                response.Data = await realEstates.Skip(start).Take(10).ToArrayAsync();
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.Message);
                Log.WriteLine(ex.StackTrace);
            }
            return response;
        }
    }
}
