using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Requests
{
    public class CreateEstateRequest : BaseRequest
    {
        public CreateEstateRequest()
        {
        }

        public string Title { get; set; }
        public double Acreage { get; set; }
        public string Address { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string Description { get; set; }
        public long DirectionId { get; set; }

        public double Price { get; set; }

        public string PriceType { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public long ProjectId { get; set; }

        public long WardId { get; set; }

        public int RealNewsTypeId { get; set; }

        public bool IsSale { get; set; }

        public ImageSource[] Images { get; set; }

        public DateTime StartAt { get; set; }

        public DateTime EndAt { get; set; }
    }

    public class ImageSource
    {
        public int Id { get; set; }

        public byte[] Source { get; set; }
    }
}
