using Common.Presentation.Endpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wcs.Shared;

namespace Wcs.Presentation.GetEnum
{
    internal class GetWcsTaskTypeEnum : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("getenum/get-wcstasktype", [Authorize]
            () =>
            {
                var res = typeof(WcsTaskTypeEnum).GetEnumNames();
                return res;
            }).WithTags(AssemblyReference.Enum);
        }
    }
}
