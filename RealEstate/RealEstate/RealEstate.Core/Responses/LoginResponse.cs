using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Responses
{
    public class LoginResponse : BaseResponse
    {
        public string Token { get; set; }
    }
}
