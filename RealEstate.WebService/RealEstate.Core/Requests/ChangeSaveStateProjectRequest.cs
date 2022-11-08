using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.Core.Requests
{
    public class ChangeSaveStateProjectRequest : BaseRequest
    {
        public long ProjectId { get; set; }
    }
}
