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
using Wcs.Contracts.Request.Job;

namespace Wcs.Presentation.Job.Page
{
    internal class GetPageQuery : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("job/get-jobpage-query", [Authorize] async (GetPageRequest request,ISender sender) =>
            {
                return await sender.Send(new GetJobPageCommand
                {
                    Name=request.Name,
                    JobType=request.JobType,
                    Total=request.Total,
                    PageIndex=request.PageIndex,
                });
            }).WithTags(AssemblyReference.Job);
        }
    }
}
