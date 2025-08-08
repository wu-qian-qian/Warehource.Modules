using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Plc.Application.DBHandler.Get.Entity;

namespace Plc.Presentation.S7Plc.Entity;

internal class GetS7EntityItem : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("plc/get-allplc-entityitem", [Authorize] async (ISender sender) =>
            {
                //var map = new Dictionary<string, string>();
                //map["rTask"] = "123";
                //map["rLoad"] = "0";
                //map["rTarget"] = "9";
                //await sender.Send(new WritePlcEventCommand
                //{
                //    DeviceName = "堆垛机01",
                //    DBNameToDataValue = map
                //});
                //var buffer = await sender.Send(new ReadPlcEventCommand
                //{
                //    Ip = "127.0.0.1",
                //    IsBath = true
                //});
                return await sender.Send(new GetS7EntityItemQuery());
            })
            .WithTags(AssemblyReference.Plc);
    }
}