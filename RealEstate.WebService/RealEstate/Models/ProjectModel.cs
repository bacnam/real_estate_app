using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Models
{
    public class ProjectModel : BaseModel
    {
        public ProjectModel()
        {
        }

        long id;
        public long Id { get { return id; } set { SetProperty(ref id, value); } }

        string name;
        public string Name { get { return name; } set { SetProperty(ref name, value); } }

        string thumbnail;
        public string Thumbnail { get { return string.Format("{0}/tintuc/{1}", Config.ImageUrl, thumbnail); } set { SetProperty(ref thumbnail, value); } }

        string address;
        public string Address { get { return address; } set { SetProperty(ref address, value); } }

        double acreage;
        public double Acreage { get { return acreage; } set { SetProperty(ref acreage, value); } }

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

        DateTime created;
        public DateTime Created { get { return created; } set { SetProperty(ref created, value); } }
    }

    public class ProjectDetailsModel : ProjectModel
    {
        string description;
        public string Description { get { return description; } set { SetProperty(ref description, value); } }

        int floor;
        public int Floor { get { return floor; } set { SetProperty(ref floor, value); } }

        int apartmentNumber;
        public int ApartmentNumber { get { return apartmentNumber; } set { SetProperty(ref apartmentNumber, value); } }

        double acreageFloor;
        public double AcreageFloor { get { return acreageFloor; } set { SetProperty(ref acreageFloor, value); } }

        string handedOver;
        public string HandedOver { get { return handedOver; } set { SetProperty(ref handedOver, value); } }

        string investor;
        public string Investor { get { return investor; } set { SetProperty(ref investor, value); } }
    }
}
