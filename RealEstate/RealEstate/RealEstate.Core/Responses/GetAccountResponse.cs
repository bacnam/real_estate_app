using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Responses
{
    public class GetAccountResponse : BaseResponse
    {
        public GetAccountResponse()
        {
        }

        public string Email { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public int Notification { get; set; }
    }
}
