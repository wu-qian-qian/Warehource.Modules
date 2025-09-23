using Common.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plc.Contracts.Request
{
    public class GetEntityItemPageRequest : PagingQuery
    {
        public string? Ip { get; set; }

        public string? DeviceName { get; set; }

        public string? Name { get; set; }
    }
}