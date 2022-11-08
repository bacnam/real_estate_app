using System;
using System.Collections.Generic;
using System.Text;
using Plugin.FacebookClient;
using Plugin.GoogleClient;
using RealEstate.ViewModels;
using RealEstate.Views;
using System.Threading.Tasks;
using Xamarin.Forms;
using RealEstate.Core.CoreServices;

namespace RealEstate.Models
{
    public class WelcomeViewModel : BaseViewModel
    {
        public WelcomeViewModel()
        {
        }

        public Command LoginFacebookCommand
        {
            get
            {
                return new Command(() => DoLoginFacebook().ConfigureAwait(false));
            }
        }

        public Command LoginGoogleCommand
        {
            get
            {
                return new Command(() => DoLoginGoogle().ConfigureAwait(false));
            }
        }

        private async Task DoLoginFacebook()
        {
            if (IsBusy)
            {
                return;
            }
            try
            {
                IsBusy = true;

                if (CrossFacebookClient.Current.IsLoggedIn)
                {
                    var service = DependencyService.Get<IUserService>();
                    string token = CrossFacebookClient.Current.ActiveToken;
                    var loginRes = await service.DoLoginFacebookAsync(token);
                    if (loginRes.Success)
                    {
                        await Xamarin.Essentials.SecureStorage.SetAsync("token", loginRes.Message);
                        await Application.Current.MainPage.Navigation.PushAsync(new HomePage());
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("THÔNG BÁO!", loginRes.Message, "OK");
                    }
                }
                else
                {
                    string[] fbPermisions = { "email" };
                    var res = await CrossFacebookClient.Current.LoginAsync(fbPermisions);

                    if (res.Status == FacebookActionStatus.Completed)
                    {
                        IsBusy = false;
                        await DoLoginFacebook();
                    }
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task DoLoginGoogle()
        {
            if (IsBusy)
            {
                return;
            }
            try
            {
                IsBusy = true;
                if (!string.IsNullOrEmpty(CrossGoogleClient.Current.AccessToken))
                {
                    var service = DependencyService.Get<IUserService>();
                    string token = CrossGoogleClient.Current.AccessToken;
                    var loginRes = await service.DoLoginGoogleAsync(token);
                    if (loginRes.Success)
                    {
                        await Xamarin.Essentials.SecureStorage.SetAsync("token", loginRes.Message);
                        await Application.Current.MainPage.Navigation.PushAsync(new HomePage());
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("THÔNG BÁO!", loginRes.Message, "OK");
                    }
                }
                else
                {
                    var res = await CrossGoogleClient.Current.LoginAsync();
                    if (res.Status == GoogleActionStatus.Completed)
                    {
                        IsBusy = false;
                        await DoLoginGoogle();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
