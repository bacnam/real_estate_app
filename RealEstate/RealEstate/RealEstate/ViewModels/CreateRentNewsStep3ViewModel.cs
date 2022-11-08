using System;
using System.Collections.Generic;
using System.Text;
using RealEstate.Models;

namespace RealEstate.ViewModels
{
    public class CreateRentNewsStep3ViewModel : BaseViewModel
    {
        public CreateRentNewsStep3ViewModel(CreateRentNewsModel rentNews)
        {
            RentNews = rentNews;
        }

        CreateRentNewsModel rentNews;
        public CreateRentNewsModel RentNews { get { return rentNews; } set { SetProperty(ref rentNews, value); } }
    }
}
