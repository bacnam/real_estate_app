using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Requests
{
    public class FacebookLoginRequest : BaseRequest
    {
        public FacebookLoginRequest(string token)
        {
            Token = token;
        }

        public string Token { get; set; }
    }
}
