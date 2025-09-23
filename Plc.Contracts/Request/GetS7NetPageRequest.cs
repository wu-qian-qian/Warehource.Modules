using Common.Shared;
using Plc.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plc.Contracts.Request
{
    public class GetS7NetPageRequest : PagingQuery
    {
        public string? Ip { get; set; }

        public S7TypeEnum? S7Type { get; set; }
    }
}