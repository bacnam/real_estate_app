using System;
using Newtonsoft.Json;

namespace RealEstate.Core.Responses
{
    public class BaseResponse : IResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }
        public static T FromJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
