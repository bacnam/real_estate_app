using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Responses
{
    public class GetRealDataDetailsResponse : BaseResponse
    {
        public RealDataDetails DataDetails { get; set; }
    }
}
