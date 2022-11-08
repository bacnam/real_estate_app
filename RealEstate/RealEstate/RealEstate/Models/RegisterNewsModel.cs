using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RealEstate.Models
{
    public class RegisterNewsModel : BaseModel
    {
        public RegisterNewsModel()
        {
            City = new CityModel();
            Direction = new DirectionModel();
            District = new DistrictModel();
            FromPrice = 0;
            Project = new ProjectModel();
            RealType = new RealTypeModel();
            ToPrice = 0;
            Ward = new WardModel();
        }

        RealTypeModel realType;
        public RealTypeModel RealType { get { return realType; } set { SetProperty(ref realType, value); } }

        CityModel city;
        public CityModel City { get { return city; } set { SetProperty(ref city, value); } }

        DistrictModel district;
        public DistrictModel District { get { return district; } set { SetProperty(ref district, value); } }

        WardModel ward;
        public WardModel Ward { get { return ward; } set { SetProperty(ref ward, value); } }

        ProjectModel project;
        public ProjectModel Project { get { return project; } set { SetProperty(ref project, value); } }

        DirectionModel direction;
        public DirectionModel Direction { get { return direction; } set { SetProperty(ref direction, value); } }

        SelectionModel price;
        public SelectionModel Price { get { return price; } set { SetProperty(ref price, value); } }

        double fromPrice;
        public double FromPrice { get { return fromPrice; } set { SetProperty(ref fromPrice, value); } }

        double toPrice;
        public double ToPrice { get { return toPrice; } set { SetProperty(ref toPrice, value); } }

        string email;
        public string Email { get { return email; } set { SetProperty(ref email, value); } }
    }
}
