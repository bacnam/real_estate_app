using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Requests
{
    public class GoogleLoginRequest : BaseRequest
    {
        public GoogleLoginRequest(string token)
        {
            Token = token;
        }

        public string Token { get; set; }
    }
}
