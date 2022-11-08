using RealEstate.WebService.Databases;

namespace RealEstate.WebService.Services
{
    public abstract class BaseService
    {
        protected string Token
        {
            get { return ""; }
        }

        protected LibraryContext Context { get; set; }

        public BaseService(LibraryContext context)
        {
            Context = context;
        }
    }
}
