using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Requests
{
    public class SearchAddressRequest : BaseRequest
    {
        public string Address { get; set; }
    }
}
