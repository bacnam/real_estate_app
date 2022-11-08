using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Responses
{
    public class GetNotificationResponse : BaseResponse
    {
        public GetNotificationResponse()
        {
            Notifications = new NotificationData[0];
        }

        public NotificationData[] Notifications { get; set; }
    }

    public class NotificationData
    {
        public long Id { get; set; }

        public long Source { get; set; }

        public string Title { get; set; }

        public string NotificationType { get; set; }

        public bool IsRead { get; set; }

        public DateTime Time { get; set; }
    }
}
