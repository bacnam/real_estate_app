using Microsoft.EntityFrameworkCore;
using RealEstate.Core.Requests;
using RealEstate.WebService.Databases;

namespace RealEstate.WebService.Services
{
    public interface IImageService
    {
        Task CreateImageAsync(ImageSource[] data, long realEstateId);
        Task<Stream> GetImage(string image);
    }

    public class ImageService : BaseService, IImageService
    {
        private string ImagePath;

        public ImageService(LibraryContext context)
            : base(context)
        {
            //ImagePath = Path.Combine(hostingEnvironment.ContentRootPath, "images");
            ImagePath = "images";
            if (!Directory.Exists(ImagePath))
            {
                Directory.CreateDirectory(ImagePath);
            }
        }

        public async Task CreateImageAsync(ImageSource[] data, long realEstateId)
        {
            try
            {
                var realEstate = Context.RealEstates.FirstOrDefault(r => r.Id == realEstateId);
                foreach (var item in data)
                {
                    var filename = Convert.ToString(Guid.NewGuid());
                    string filepath = Path.Combine(ImagePath, filename);
                    using (FileStream fs = File.Create(filepath))
                    {
                        fs.Write(item.Source, 0, item.Source.Length);
                        fs.Flush();
                    }
                    Context.Images.Add(new Models.ImageModel()
                    {
                        FileName = filename,
                        RealEstate = realEstate,
                        Sort = 0
                    });
                }

                await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.Message);
                Log.WriteLine(ex.StackTrace);
            }
        }

        public async Task<Stream> GetImage(string image)
        {
            try
            {
                var img = await Context.Images.FirstOrDefaultAsync(i => i.FileName == image);
                if (img != null)
                {
                    string filePath = Path.Combine(ImagePath, img.FileName);
                    if (File.Exists(filePath))
                    {
                        return File.OpenRead(filePath);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Log.WriteLine(ex.Message);
                Log.WriteLine(ex.StackTrace);
                return null;
            }
        }
    }
}
