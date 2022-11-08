using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Requests
{
    public class RegisterNewsRequest : BaseRequest
    {
        public long RealTypeId { get; set; }

        public long CityId { get; set; }

        public long DistrictId { get; set; }

        public long WardId { get; set; }

        public long ProjectId { get; set; }

        public long DirectionId { get; set; }

        public double FromPrice { get; set; }

        public double ToPrice { get; set; }

        public string Email { get; set; }
    }
}
