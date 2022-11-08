using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Responses
{
    public class ProjectResponse
    {
        public long Id { get; set; }

        public string Name { get; set; }
    }

    public class SearchProjectResponse : BaseResponse
    {
        public SearchProjectResponse()
        {
        }

        public ProjectData[] Data { get; set; }
    }

    public class ProjectData
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Thumbnail { get; set; }

        public string Address { get; set; }

        public double Acreage { get; set; }

        public ushort IsSaved { get; set; }

        public DateTime Created { get; set; }
    }

    public class ProjectDetailsData : ProjectData
    {
        public double AcreageFloor { get; set; }

        public int ApartmentNumber { get; set; }

        public string Description { get; set; }

        public int Floor { get; set; }

        public string HandedOver { get; set; }

        public string Investor { get; set; }
    }
}
