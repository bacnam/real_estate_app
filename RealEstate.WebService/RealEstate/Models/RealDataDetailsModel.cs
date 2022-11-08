using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Models
{
    public class RealDataDetailsModel : RealDataModel
    {
        string description;
        public string Description { get { return description; } set { SetProperty(ref description, value); } }

        string project;
        public string Project { get { return project; } set { SetProperty(ref project, value); } }

        string contact;
        public string Contact { get { return contact; } set { SetProperty(ref contact, value); } }

        string contactPhone;
        public string ContactPhone { get { return contactPhone; } set { SetProperty(ref contactPhone, value); } }

        string contactAvatar;
        public string ContactAvatar { get { return contactAvatar; } set { SetProperty(ref contactAvatar, value); } }
    }
}
