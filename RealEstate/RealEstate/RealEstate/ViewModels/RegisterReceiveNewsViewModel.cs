using System;
using System.Collections.Generic;
using System.Text;
using RealEstate.Models;

namespace RealEstate.ViewModels
{
    public class RegisterReceiveNewsViewModel : BaseViewModel
    {
        public RegisterReceiveNewsViewModel()
        {
            registerNews = new RegisterNewsModel();
        }

        private RegisterNewsModel registerNews;

        public RegisterNewsModel RegisterNews
        {
            get { return registerNews; }
        }
    }
}
