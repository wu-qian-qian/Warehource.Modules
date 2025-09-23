using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Plc.Application.Handler.DataBase.Update.Net;
using Plc.Contracts.Request;

namespace Plc.Presentation.S7Plc.Update;

internal class UpdateS7Net : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("plc/update-s7net", [Authorize(Roles = "admin")]
            async (UpdateS7NetRequest request, ISender sender) =>
            {
                await sender.Send(new UpdateS7NetCommand
                {
                    Id = request.Id,
                    Ip = request.Ip,
                    IsUse = request.IsUse,
                    Port = request.Port,
                    Rack = request.Rack,
                    ReadHeart = request.ReadHeart,
                    ReadTimeOut = request.ReadTimeOut,
                    S7Type = request.S7Type,
                    Solt = request.Solt,
                    WriteHeart = request.WriteHeart,
                    WriteTimeOut = request.WriteTimeOut
                });
            }).WithTags(AssemblyReference.Plc);
    }
}