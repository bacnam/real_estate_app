using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Requests
{
    public class ChangeRealEstateRequest : BaseRequest
    {
        public long RealEstateId { get; set; }
    }
}
