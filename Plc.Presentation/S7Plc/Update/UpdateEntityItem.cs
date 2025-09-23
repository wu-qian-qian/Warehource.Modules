using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Plc.Application.Handler.DataBase.Update.Entity;
using Plc.Contracts.Request;

namespace Plc.Presentation.S7Plc.Update;

internal class UpdateEntityItem : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("plc/update-entityitem", [Authorize(Roles = "admin")]
            async (UpdateEntityItemRequest request, ISender sender) =>
            {
                await sender.Send(new UpateEntityItemCommand
                {
                    Id = request.Id,
                    DataOffset = request.DataOffset,
                    ArrayLength = request.ArrayLength,
                    BitOffset = request.BitOffset,
                    DBAddress = request.DBAddress,
                    Description = request.Description,
                    DeviceName = request.DeviceName,
                    Index = request.Index,
                    IsUse = request.IsUse,
                    Name = request.Name,
                    S7BlockType = request.S7BlockType,
                    S7DataType = request.S7DataType
                });
            }).WithTags(AssemblyReference.Plc);
    }
}