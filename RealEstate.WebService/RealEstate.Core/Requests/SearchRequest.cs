using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Requests
{
    public class SearchRequest : BaseRequest
    {
        public int Start { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        public string Ward { get; set; }

        public int Room { get; set; }

        public string RealTypes { get; set; }

        public long Project { get; set; }

        public bool IsSale { get; set; }

        public double FromPrice { get; set; }

        public double ToPrice { get; set; }

        public double FromAcreage { get; set; }

        public double ToAcreage { get; set; }
    }
}
