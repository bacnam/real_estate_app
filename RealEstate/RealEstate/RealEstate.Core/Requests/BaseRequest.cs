using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RealEstate.Core.Requests
{
    public class BaseRequest : IRequest
    {
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
