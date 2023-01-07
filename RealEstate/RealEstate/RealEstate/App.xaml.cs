using System;
using System.Threading.Tasks;
using RealEstate.Services;
using RealEstate.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealEstate
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<IUserService, UserService>();
            DependencyService.Register<IAPIService, APIService>();
            DependencyService.Register<IAddressService, AddressService>();
            DependencyService.Register<INewsService, NewsService>();
            DependencyService.Register<ISearchService, SearchService>();
            DependencyService.Register<IRealEstateService, RealEstateService>();
            DependencyService.Register<IProjectService, ProjectService>();

            InitAsync();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public async Task InitAsync(bool isStart = false)
        {
            Console.WriteLine("Init async");
            string token = await SecureStorage.GetAsync("token");
            Page page = null;
            if (!string.IsNullOrEmpty(token))
            {
                page = new HomePage();
            }
            else
            {
                if (isStart)
                {
                    page = new WelcomePage();
                }
                else
                {
                    page = new LoadingPage();
                }
            }
            if (MainPage == null)
            {
                MainPage = new NavigationPage(page);
            }
            else
            {
                await MainPage.Navigation.PushAsync(page);
            }
        }

    }
}
