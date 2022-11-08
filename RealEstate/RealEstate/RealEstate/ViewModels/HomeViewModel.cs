using System.Collections.ObjectModel;
using System.Threading.Tasks;
using RealEstate.Services;

namespace RealEstate.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public HomeViewModel()
        {
            Banners = new ObservableCollection<string>()
            {
                "banner1.jpg",
                "banner2.jpg",
                "banner3.jpg",
            };

            Init().ConfigureAwait(false);
        }

        private async Task Init()
        {
            IUserService accountService = Xamarin.Forms.DependencyService.Get<IUserService>();
            var account = await accountService.GetAccountAsync();
            if (account.Success)
            {
                FullName = account.FullName;
                Notification = account.Notification;
            }
        }

        public ObservableCollection<string> Banners { get; }

        private string fullname;
        public string FullName
        {
            get
            {
                return fullname;
            }
            set
            {
                SetProperty(ref fullname, value);
            }
        }

        private int notification;
        public int Notification
        {
            get
            {
                return notification;
            }
            set
            {
                SetProperty(ref notification, value);
            }
        }
    }
}
