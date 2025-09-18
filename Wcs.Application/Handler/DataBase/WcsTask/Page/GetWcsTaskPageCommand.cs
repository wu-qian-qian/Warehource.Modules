using Common.Application.MediatR.Message.PageQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wcs.Contracts.Respon.WcsTask;
using Wcs.Shared;

namespace Wcs.Application.Handler.DataBase.WcsTask.Page
{
    public class GetWcsTaskPageCommand : PageQuery<WcsTaskDto>
    {
        public WcsTaskStatusEnum? TaskStatus { get; set; }

        public CreatorSystemTypeEnum? CreatorSystemType { get; set; }

        public WcsTaskTypeEnum? WcsTaskType { get; set; }
        public string? Container { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string? TaskCode { get; set; }

        public int? SerialNumber { get; set; }

        public string? GetLocation { get; set; }

        public string? PutLocation { get; set; }
    }
}
