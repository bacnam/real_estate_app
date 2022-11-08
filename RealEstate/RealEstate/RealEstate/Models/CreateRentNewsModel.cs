using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using RealEstate.Extensions;
using Xamarin.Forms;

namespace RealEstate.Models
{
    public class CreateRentNewsModel : BaseModel
    {
        public CreateRentNewsModel()
        {
            Images = new ObservableRangeCollection<RentImageSource>();
            StartDate = DateTime.Now;
            EndDate = DateTime.Now.AddMonths(1);
        }

        string headerTitle;
        public string HeaderTitle { get { return headerTitle; } set { SetProperty(ref headerTitle, value); } }

        string address;
        public string Address { get { return address; } set { SetProperty(ref address, value); } }

        public string FullAddress
        {
            get
            {
                return string.Format("{0}, {1}, {2}, {3}", Address, Ward.Id == 0 ? "" : Ward.Name, District.Id == 0 ? "" : District.Name, City.Id == 0 ? "" : City.Name);
            }
        }

        double latitude;
        public double Latitude
        {
            get
            {
                return latitude;
            }
            set
            {
                SetProperty(ref latitude, value);
            }
        }

        double longitude;
        public double Longitude
        {
            get
            {
                return longitude;
            }
            set
            {
                SetProperty(ref longitude, value);
            }
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

        string title;
        public string Title { get { return title; } set { SetProperty(ref title, value); } }

        double acreage;
        public double Acreage { get { return acreage; } set { SetProperty(ref acreage, value); } }

        double price;
        public double Price
        {
            get
            {
                return price;
            }
            set
            {
                SetProperty(ref price, value);
            }
        }

        public double GetPrice()
        {
            return Price.ConvertPrice(UnitPrice.Name);
        }

        public string PriceString { get { return string.Format("{0:F2} {1}", Price, UnitPrice.Name); } }

        SelectionModel unitPrice;
        public SelectionModel UnitPrice { get { return unitPrice; } set { SetProperty(ref unitPrice, value); } }

        int room;
        public int Room { get { return room; } set { SetProperty(ref room, value); } }

        int floor;
        public int Floor { get { return floor; } set { SetProperty(ref floor, value); } }

        string desciption;
        public string Desciption { get { return desciption; } set { SetProperty(ref desciption, value); } }

        bool isSale;
        public bool IsSale { get { return isSale; } set { SetProperty(ref isSale, value); } }

        DirectionModel direction;
        public DirectionModel Direction { get { return direction; } set { SetProperty(ref direction, value); } }

        ObservableRangeCollection<RentImageSource> images;
        public ObservableRangeCollection<RentImageSource> Images { get { return images; } set { SetProperty(ref images, value); } }

        string contactName;
        public string ContactName { get { return contactName; } set { SetProperty(ref contactName, value); } }

        string contactPhone;
        public string ContactPhone { get { return contactPhone; } set { SetProperty(ref contactPhone, value); } }

        DateTime startDate;
        public DateTime StartDate { get { return startDate; } set { SetProperty(ref startDate, value); } }

        DateTime endDate;
        public DateTime EndDate { get { return endDate; } set { SetProperty(ref endDate, value); } }
    }

    public class RentImageSource : BaseModel
    {
        int id;
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                SetProperty(ref id, value);
            }
        }

        byte[] source;
        public byte[] Source
        {
            get
            {
                return source;
            }
            set
            {
                SetProperty(ref source, value);
            }
        }

        public ImageSource ImageSource
        {
            get
            {
                return ImageSource.FromStream(() => new MemoryStream(Source));
            }
        }
    }
}
