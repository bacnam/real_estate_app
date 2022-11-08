using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Requests
{
    public class GetNewsRequest : BaseRequest
    {
        public int Start { get; set; }

        public bool IsMarket { get; set; }
    }

    public class GetNewsDetailsRequest : BaseRequest
    {
        public long Id { get; set; }
    }
}
