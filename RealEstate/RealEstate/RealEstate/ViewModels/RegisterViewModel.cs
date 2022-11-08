using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Services;
using Xamarin.Forms;

namespace RealEstate.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        #region Propeties

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

        private string rePassword;
        public string RePassword
        {
            get
            {
                return rePassword;
            }
            set
            {
                SetProperty(ref rePassword, value);
            }
        }

        private string phoneNumber;
        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                SetProperty(ref phoneNumber, value);
            }
        }

        #endregion

        #region Methods

        public async Task Register(Action actionDone, Action<string> actionFail)
        {
            if (string.IsNullOrEmpty(Email)
                || string.IsNullOrEmpty(Password)
                || string.IsNullOrEmpty(PhoneNumber)
                || string.IsNullOrEmpty(FullName))
            {
                actionFail?.Invoke("Bạn phải nhập đầy đủ các trường.");
                return;
            }
            IUserService service = DependencyService.Get<IUserService>();
            var response = await service.DoRegisterAsync(Email, Password, FullName, PhoneNumber);

            if (response.Success)
            {
                actionDone?.Invoke();
            }
            else
            {
                actionFail?.Invoke(response.Message);
            }
        }

        #endregion
    }
}
