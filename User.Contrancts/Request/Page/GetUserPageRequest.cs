using Common.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Contrancts.Request.Page
{
    public class GetUserPageRequest : PagingQuery
    {
        public string? Name { get; set; }
    }
}
