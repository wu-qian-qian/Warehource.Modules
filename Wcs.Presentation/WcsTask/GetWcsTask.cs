using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.DBHandler.WcsTask.Get;

namespace Wcs.Presentation.WcsTask;

public class GetWcsTask : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("wcstask/get-wcstask/{serialNumber}", [Authorize] async (
            int request,
            ISender sender) =>
        {
            return await sender.Send(new GetWcsTaskQuery
            {
                SerialNumber = request
            });
        }).WithTags(AssemblyReference.WcsTask);
    }
}