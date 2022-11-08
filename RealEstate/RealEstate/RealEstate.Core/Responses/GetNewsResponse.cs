using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Responses
{
    public class GetNewsResponse : BaseResponse
    {
        public GetNewsResponse()
        {
        }

        public NewsData[] News { get; set; }
    }

    public class NewsData
    {
        public long Id { get; set; }

        public int ReadTime { get; set; }

        public string Thumbnail { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }
    }

    public class GetNewsDetailsResponse : BaseResponse
    {
        public NewsDetailsData NewsDetails { get; set; }
    }

    public class NewsDetailsData : NewsData
    {
        public string Content { get; set; }
    }
}
