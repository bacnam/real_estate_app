using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Models
{
    public class SleepRoomModel : BaseModel
    {
        long id;
        public long Id { get { return id; } set { SetProperty(ref id, value); } }

        int order;
        public int Sort { get { return order; } set { SetProperty(ref order, value); } }

        string name;
        public string Name { get { return name; } set { SetProperty(ref name, value); } }
    }
}
