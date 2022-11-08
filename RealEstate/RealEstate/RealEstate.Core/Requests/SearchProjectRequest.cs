using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Requests
{
    public class SearchProjectRequest : BaseRequest
    {
        public int Start { get; set; }

        public string City { get; set; }

        public string District { get; set; }
    }
}
