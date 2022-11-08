using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using RealEstate.Services;

namespace RealEstate.Models
{
    public class RealDataModel : BaseModel
    {
        public RealDataModel()
        {
            images = new string[0];
        }

        private long id;
        public long Id { get { return id; } set { SetProperty(ref id, value); } }

        private string[] images;
        public string[] Images { get { return images; } set { SetProperty(ref images, value); } }

        public ObservableCollection<string> ImageUrls
        {
            get
            {
                if (Images.Length <= 0)
                {
                    return new ObservableCollection<string> { "default-img" };
                }
                return new ObservableCollection<string>(Images.Select(img => string.Format("{0}images/{1}", APIService.URL, img)));
            }
        }

        public string Thumbnail
        {
            get
            {
                return Images.Length <= 0 ? "default-img" : string.Format("{0}images/{1}", APIService.URL, Images.First());
            }
        }

        private string address;
        public string Address { get { return address; } set { SetProperty(ref address, value); } }

        double acreage;
        public double Acreage { get { return acreage; } set { SetProperty(ref acreage, value); } }

        double price;
        public double Price { get { return price; } set { SetProperty(ref price, value); } }

        public string PriceFull { get { return Extensions.PriceExtension.GetFullPrice(Price, PriceType); } }

        public string PriceUnit { get { return Extensions.PriceExtension.GetPriceUnit(Price, PriceType); } }

        public string PriceString { get { return Extensions.PriceExtension.GetPriceString(Price, PriceType); } }

        string priceValue;
        public string PriceValue { get { return priceValue; } set { SetProperty(ref priceValue, value); } }

        string priceType;
        public string PriceType { get { return priceType; } set { SetProperty(ref priceType, value); } }

        int room;
        public int Room { get { return room; } set { SetProperty(ref room, value); } }

        int floor;
        public int Floor { get { return floor; } set { SetProperty(ref floor, value); } }

        string realType;
        public string RealType { get { return realType; } set { SetProperty(ref realType, value); } }

        string direction;
        public string Direction { get { return direction; } set { SetProperty(ref direction, value); } }

        double longitude;
        public double Longitude { get { return longitude; } set { SetProperty(ref longitude, value); } }

        double latitude;
        public double Latitude { get { return latitude; } set { SetProperty(ref latitude, value); } }

        DateTime startAt;
        public DateTime StartAt { get { return startAt; } set { SetProperty(ref startAt, value); } }

        DateTime endAt;
        public DateTime EndAt { get { return endAt; } set { SetProperty(ref endAt, value); } }

        bool isSaved;
        public bool IsSaved { get { return isSaved; } set { SetProperty(ref isSaved, value); OnPropertyChanged("ImageSave"); } }

        public string ImageSave
        {
            get
            {
                if (IsSaved)
                {
                    return "saved";
                }
                return "save";
            }
        }
    }



    public class PriceData
    {
        public PriceData(double price, string type)
        {
            PriceValue = price;
            double ty = price / 1000000000;
            double tr = (price % 1000000000) / 1000000;
            double ngan = ((price % 1000000000) % 1000000) / 1000;
            double dong = ((price % 1000000000) % 1000000) % 1000;
            PriceType = "";

            if (ty > 100)
            {
                PriceString = string.Format("{0:F0}", ty);
                PriceType = "tỷ";
            }
            else if (ty >= 1)
            {
                PriceString = string.Format("{0:F1}", ty);
                PriceType = "tỷ";
            }
            else if (tr > 100)
            {
                PriceString = string.Format("{0:F0}", tr);
                PriceType = "triệu";
            }
            else if (tr >= 1)
            {
                PriceString = string.Format("{0:F1}", tr);
                PriceType = "triệu";
            }
            else if (ngan > 100)
            {
                PriceString = string.Format("{0:F0}", ngan);
                PriceType = "ngàn";
            }
            else if (ngan >= 1)
            {
                PriceString = string.Format("{0:F1}", ngan);
                PriceType = "ngàn";
            }
            else if (dong > 100)
            {
                PriceString = string.Format("{0:F0}", dong);
                PriceType = "đồng";
            }
            else
            {
                PriceString = string.Format("{0:F1}", dong);
                PriceType = "đồng";
            }
            if (type.Equals("Liên hệ", StringComparison.OrdinalIgnoreCase))
            {
                PriceString = "Liên hệ";
                PriceType = "";
            }
            else if (type.Equals("tháng"))
            {
                PriceType += "/th";
            }
            else if (type.Equals("m2"))
            {
                PriceType += "/m2";
            }
        }

        public PriceData()
        {

        }

        public double PriceValue { get; set; }

        public string PriceString { get; set; }

        public string PriceType { get; set; }
    }
}
