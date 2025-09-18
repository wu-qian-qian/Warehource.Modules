using Common.Presentation.Endpoints;
using Identity.Application.Handler.Page.Role;
using Identity.Contrancts.Request.Page;
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

namespace Identity.Presentation.PageQuery
{
    internal class RolePage:IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("get/get-role-page",
                    [Authorize(Roles ="admin")] async (GetRolePageRequest request, ISender sender) =>
                    {
                        return await sender.Send(new GetRolePageCommand
                        {
                            RoleName = request.RoleName,
                            PageIndex = request.PageIndex,
                            Total = request.Total,
                            StartTime = request.StartTime,
                            EndTime = request.EndTime
                        });
                    })
                .WithTags(AssemblyReference.User);
        }
    }
}
