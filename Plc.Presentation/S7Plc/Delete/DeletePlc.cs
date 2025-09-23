using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Plc.Application.Handler.DataBase.Delete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plc.Presentation.S7Plc.Delete
{
    internal class DeletePlc : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("plc/delete-plc/{id}",
                    [Authorize] async (Guid id, ISender sender) =>
                    {
                        await sender.Send(new DeletePlcCommand { Id = id });
                    })
                .WithTags(AssemblyReference.Plc);
        }
    }
}