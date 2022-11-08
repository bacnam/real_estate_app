using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Requests
{
    public interface IRequest
    {
        string ToJson();
    }
}
