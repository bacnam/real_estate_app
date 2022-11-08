using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Responses
{
    public class GetProjectDetailsResponse : BaseResponse
    {
        public GetProjectDetailsResponse()
        {
        }

        public ProjectDetailsData DetailsData { get; set; }
    }
}
