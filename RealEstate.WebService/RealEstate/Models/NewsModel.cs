using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RealEstate.Models
{
    public class NewsModel : BaseModel
    {
        public long Id { get; set; }

        private string thumbnail;
        public string Thumbnail
        {
            get { return string.Format("{0}/news/{1}", Config.ImageUrl, thumbnail); }
            set { SetProperty(ref thumbnail, value); }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        int readTime;
        public int ReadTime { get { return readTime; } set { SetProperty(ref readTime, value); } }

        public HtmlWebViewSource DescriptionHtml
        {
            get
            {
                HtmlWebViewSource htmlWebViewSource = new HtmlWebViewSource();
                if (!string.IsNullOrEmpty(Description))
                {
                    var des = Description;
                    if (Description.Length >= 250)
                    {
                        des = Description.Substring(0, 250) + "...";
                    }
                    htmlWebViewSource.Html = fonts + "<p>" + des + "</p>";
                }
                return htmlWebViewSource;
            }
        }

        private DateTime created;
        public DateTime Created
        {
            get { return created; }
            set { SetProperty(ref created, value); }
        }

        public string CreatedString
        {
            get
            {
                var years = DateTime.UtcNow.Year - Created.Year;
                var dates = (DateTime.UtcNow - Created).TotalDays;
                var hours = (DateTime.UtcNow - Created).TotalHours;
                var minutes = (DateTime.UtcNow - Created).TotalMinutes;
                var seconds = (DateTime.UtcNow - Created).TotalSeconds;
                if (years >= 1)
                {
                    return string.Format("{0:F0} năm trước", years);
                }
                else if (dates >= 1)
                {
                    return string.Format("{0:F0} ngày trước", dates);
                }
                else if (hours >= 1)
                {
                    return string.Format("{0:F0} giờ trước", hours);
                }
                else if (minutes >= 1)
                {
                    return string.Format("{0:F0} phút trước", minutes);
                }
                else
                {
                    return string.Format("{0:F0} giây trước", seconds);
                }
            }
        }

        private DateTime updated;
        public DateTime Updated
        {
            get { return updated; }
            set { SetProperty(ref updated, value); }
        }

        string fonts = "<style type=\"text/css\">" +
            "p {" +
            "font-size: 14px;" +
            "font-style: normal;" +
            "font-weight: 200;" +
            "text-align: justify;" +
            "color: #363636;" +
            "line-height: 18px;" +
            "}" +
            "</style>";
    }

    public class NewsDetailsModel : NewsModel
    {
        string content;
        public string Content
        {
            get { return content; }
            set { SetProperty(ref content, value); }
        }

        public HtmlWebViewSource ContentHtml
        {
            get
            {
                HtmlWebViewSource htmlWebViewSource = new HtmlWebViewSource();
                if (!string.IsNullOrEmpty(Content))
                {
                    htmlWebViewSource.Html = "<h3>" + Title + "</h3>" + "<p style='color: #77858C; font-size: 10px;'>" + Created.ToString("HH:mm dd/MM/yyyy") + "</p>" + Content;
                }
                return htmlWebViewSource;
            }
        }

    }
}
