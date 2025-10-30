using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wcs.Shared;

namespace Wcs.Application.Handler.Business.GetNextExecuteNode
{
    public class GetNextExecuteNodeQuery : IQuery<Result<DeviceTypeEnum>>
    {
        public string PathNodeGroup { get; set; }

        public WcsTaskTypeEnum WcsTaskType { get; set; }

        public string RegionCode { get; set; }

        public DeviceTypeEnum DeviceType { get; set; }
    }
}