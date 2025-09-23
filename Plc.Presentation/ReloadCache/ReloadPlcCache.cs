using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Plc.Application.Handler.ReadWrite.ReLoad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plc.Presentation.ReloadCache
{
    internal class ReloadPlcCache : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("plc/reload-plc-cache",
                    handler: [Authorize] async (ISender sender) => { await sender.Send(new ReloadCommand()); })
                .WithTags(AssemblyReference.Plc);
        }
    }
}