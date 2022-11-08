using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Services;
using RealEstate.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RealEstate
{
    public class App : Application
    {
        public App()
        {
            DependencyService.Register<IUserService, UserService>();
            DependencyService.Register<IAPIService, APIService>();
            DependencyService.Register<IAddressService, AddressService>();
            DependencyService.Register<INewsService, NewsService>();
            DependencyService.Register<ISearchService, SearchService>();
            DependencyService.Register<IRealEstateService, RealEstateService>();
            DependencyService.Register<IProjectService, ProjectService>();

            InitAsync().Wait();
        }

        public async Task InitAsync(bool isStart = false)
        {
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

        protected override void OnStart()
        {
            base.OnStart();
        }

        protected override void OnSleep()
        {
            base.OnSleep();
        }

        protected override void OnResume()
        {
            base.OnResume();
        }
    }
}
