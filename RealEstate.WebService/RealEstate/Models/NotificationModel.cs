using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RealEstate.Models
{
    public class NotificationModel : BaseModel
    {
        long id;
        public long Id { get { return id; } set { SetProperty(ref id, value); } }

        long source;
        public long Source { get { return source; } set { SetProperty(ref source, value); } }

        string title;
        public string Title { get { return title; } set { SetProperty(ref title, value); } }

        string type;
        public string NotificationType { get { return type; } set { SetProperty(ref type, value); } }

        public Color NotificationTypeColor
        {
            get
            {
                if (type == "Tin tức")
                {
                    return Color.FromHex("#1A8DFF");
                }
                return Color.FromHex("#FC9261");
            }
        }

        bool isRead;
        public bool IsRead { get { return isRead; } set { SetProperty(ref isRead, value); } }

        DateTime time;
        public DateTime Time { get { return time; } set { SetProperty(ref time, value); } }
    }
}
