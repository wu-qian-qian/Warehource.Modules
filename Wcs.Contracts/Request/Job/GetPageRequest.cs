using Common.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wcs.Contracts.Request.Job
{
    public class GetPageRequest: PagingQuery
    {
        public string? Name { get; set; }

        public string? JobType { get; set; }
    }
}
