using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Responses
{
    public class SearchAddressResponse : BaseResponse
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
