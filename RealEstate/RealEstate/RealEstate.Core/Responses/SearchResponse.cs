using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Responses
{
    public class SearchResponse : BaseResponse
    {
        public RealData[] Data { get; set; }
    }

    public class RealData
    {
        public long Id { get; set; }

        public string[] Images { get; set; }

        public string Address { get; set; }

        public string FullAddress { get; set; }

        public double Acreage { get; set; }

        public double Price { get; set; }

        public string PriceType { get; set; }

        public int Room { get; set; }

        public int Floor { get; set; }

        public string RealType { get; set; }

        public string Direction { get; set; }

        public ushort IsSaved { get; set; }

        public DateTime StartAt { get; set; }

        public DateTime EndAt { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public string ImageSave
        {
            get
            {
                if (IsSaved == 1)
                {
                    return "saved";
                }
                return "save";
            }
        }
    }

    public class RealDataDetails : RealData
    {

        public string Description { get; set; }

        public string Project { get; set; }

        public string Contact { get; set; }

        public string ContactPhone { get; set; }

        public string ContactAvatar { get; set; }
    }
}
