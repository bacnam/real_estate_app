using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Responses
{
    public interface IResponse
    {
        bool Success { get; }
        string Message { get; }
    }
}
