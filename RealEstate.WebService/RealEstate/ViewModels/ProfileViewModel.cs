using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Plugin.FacebookClient;
using Plugin.GoogleClient;
using RealEstate.Core.CoreServices;
using Xamarin.Forms;

namespace RealEstate.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        public ProfileViewModel()
        {
            GetProfileCommand = new Command(() => DoGetProfileCommandAsync().ConfigureAwait(false));
        }

        string email;
        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }
        string fullname;
        public string FullName
        {
            get { return fullname; }
            set { SetProperty(ref fullname, value); }
        }
        string phoneNumber;
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { SetProperty(ref phoneNumber, value); }
        }

        string avatar = "avatar.jpg";
        public string Avatar
        {
            get { return avatar; }
            set { SetProperty(ref avatar, value); }
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

        public Command GetProfileCommand { get; }

        private async Task DoGetProfileCommandAsync()
        {
            if (IsBusy)
            {
                return;
            }
            try
            {
                IsBusy = true;

                var service = DependencyService.Resolve<IUserService>();
                var res = await service.GetAccountAsync();
                if (res.Success)
                {
                    Email = res.Email;
                    PhoneNumber = res.PhoneNumber;
                    FullName = res.FullName;
                    Notification = res.Notification;
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

        public Command LogoutCommand
        {
            get
            {
                return new Command(() => DoLogoutCommand().ConfigureAwait(false));
            }
        }

        private async Task DoLogoutCommand()
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
                    CrossFacebookClient.Current.Logout();
                }

                if (!string.IsNullOrEmpty(CrossGoogleClient.Current.AccessToken))
                {
                    CrossGoogleClient.Current.Logout();
                }

                var service = DependencyService.Get<IUserService>();
                await service.LogoutAsync();
                Xamarin.Essentials.SecureStorage.Remove("token");
                await (Application.Current as App).InitAsync(true);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
