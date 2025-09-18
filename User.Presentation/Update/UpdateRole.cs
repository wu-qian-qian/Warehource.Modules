using Common.Presentation.Endpoints;
using Identity.Application.Handler.Add.Role;
using Identity.Contrancts.Request;
using Identity.Contrancts.Request.Update;
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
using Identity.Application.Handler.Update.Role;

namespace Identity.Presentation.Update
{
    internal class UpdateRole : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("user/update-role", [Authorize(Roles = "admin")] async (UpdateRoleRequest request, ISender sender) =>
            {
                return await sender.Send(new UpdateRoleCommand
                {
                    Id=request.Id,
                    Description = request.Description,
                    RoleName = request.RoleName
                });
            }).WithTags(AssemblyReference.User);
        }
    }
}
