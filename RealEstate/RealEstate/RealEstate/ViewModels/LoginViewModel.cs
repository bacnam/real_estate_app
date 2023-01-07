using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Services;
using Xamarin.Forms;

namespace RealEstate.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
            Email = "baonguyen@gmail.com";
            Password = "baonguyen";
        }

        public Action LoginSuccess;
        public Action<string> LoginFail;

        private string email;
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                SetProperty(ref email, value);
            }
        }

        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                SetProperty(ref password, value);
            }
        }

        public Command LoginCommand
        {
            get
            {
                return new Command(() => DoLogin().ConfigureAwait(false));
            }
        }

        async Task DoLogin()
        {
            if (string.IsNullOrEmpty(Email)
                || string.IsNullOrEmpty(Password))
            {
                LoginFail?.Invoke("Bạn phải nhập đầy đủ các trường.");
                return;
            }
            IUserService service = DependencyService.Get<IUserService>();
            var response = await service.DoLoginAsync(Email, Password);

            if (response.Success)
            {
                await Xamarin.Essentials.SecureStorage.SetAsync("token", response.Message);
                LoginSuccess?.Invoke();
            }
            else
            {
                LoginFail?.Invoke(response.Message);
            }
        }
    }
}
