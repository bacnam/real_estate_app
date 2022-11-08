using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Requests
{
    public class LoginRequest : BaseRequest
    {
        public LoginRequest(string email, string password)
        {
            Password = password;
            Email = email;
        }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
