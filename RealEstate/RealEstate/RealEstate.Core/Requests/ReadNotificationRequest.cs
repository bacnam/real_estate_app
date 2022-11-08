using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Requests
{
    public class ReadNotificationRequest : BaseRequest
    {
        public ReadNotificationRequest()
        {
        }

        public long Id { get; set; }
    }
}
