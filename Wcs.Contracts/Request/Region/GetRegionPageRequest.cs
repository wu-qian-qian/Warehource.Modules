using Common.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wcs.Contracts.Request.Region
{
    public class GetRegionPageRequest:PagingQuery
    {
        public string? Code { get; set; }
    }
}
