using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Requests
{
    public class RegisterRequest : BaseRequest
    {
        public RegisterRequest(string email, string password, string fullname, string phoneNumber)
        {
            Email = email;
            Password = password;
            FullName = fullname;
            PhoneNumber = phoneNumber;
        }

        public RegisterRequest() { }

        public string Email { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }
    }
}
