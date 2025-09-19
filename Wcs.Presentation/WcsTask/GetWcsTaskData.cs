using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.Handler.DataBase.WcsTask.GetData;

namespace Wcs.Presentation.WcsTask;

internal class GetWcsTaskData : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("wcstask/get-TaskData/{time}", async (DateTime time, ISender sender) =>
        {
            var dateTime = time.Date;
            return await sender.Send(new GetDataCommand
            {
                StartTime = dateTime,
                EndTime = dateTime.AddDays(1)
            });
        }).WithTags(AssemblyReference.WcsTask);
    }
}