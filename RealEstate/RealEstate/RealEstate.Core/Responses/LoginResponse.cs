using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Responses
{
    public class LoginResponse : BaseResponse
    {
        public LoginResponse()
        {
            Message = "Email or password incorrect!";
        }
        public string Token { get; set; }
    }
}
