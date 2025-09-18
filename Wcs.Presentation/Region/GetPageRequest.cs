using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wcs.Application.Handler.DataBase.Job.Page;
using Wcs.Application.Handler.DataBase.Region.Get;
using Wcs.Application.Handler.DataBase.Region.Page;
using Wcs.Contracts.Request.Region;

namespace Wcs.Presentation.Region
{
    internal class GetPageRequest : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("region/get-regionpage", [Authorize]
            async (GetRegionPageRequest request,ISender sender) =>
            {
                return await sender.Send(new GetRegionPageCommand
                {
                    Code = request.Code,
                    Total = request.Total,
                    PageIndex = request.PageIndex,
                });
            }).WithTags(AssemblyReference.Region);
        }
    }
}
