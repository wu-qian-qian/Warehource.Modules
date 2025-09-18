using Common.Application.MediatR.Message.PageQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wcs.Contracts.Respon.Job;

namespace Wcs.Application.Handler.DataBase.Job.Page
{
    public class GetJobPageCommand : PageQuery<JobDto>
    {
        public string? Name { get; set; }

        public string? JobType { get; set; }
    }
}
