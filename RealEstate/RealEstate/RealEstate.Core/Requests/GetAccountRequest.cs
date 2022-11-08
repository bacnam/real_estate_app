using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Requests
{
    public class GetAccountRequest : BaseRequest
    {
        public GetAccountRequest(string token)
        {
            Token = token;
        }

        public string Token { get; set; }
    }
}
