using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Requests
{
    public class GetProjectDetailsRequest : BaseRequest
    {
        public GetProjectDetailsRequest()
        {
        }

        public long ProjectId { get; set; }
    }
}
