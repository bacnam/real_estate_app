using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Models
{
    public class StreetModel : BaseModel
    {
        long id;
        public long Id { get { return id; } set { SetProperty(ref id, value); } }

        int order;
        public int Sort { get { return order; } set { SetProperty(ref order, value); } }

        string name;
        public string Name { get { return name; } set { SetProperty(ref name, value); } }

        DateTime? created;
        public DateTime? Created { get { return created; } set { SetProperty(ref created, value); } }

        DateTime? updated;
        public DateTime? Updated { get { return updated; } set { SetProperty(ref updated, value); } }
    }
}
