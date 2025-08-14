using Common.Application.MediatR.Behaviors;
using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.Handler.DB.WcsTask.Get;
using Wcs.Contracts.Respon.WcsTask;

namespace Wcs.Presentation.WcsTask;

public class GetWcsTask : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("wcstask/get-wcstask/{serialNumber}", [Authorize] async (
            int request,
            ISender sender) =>
        {
            Result<IEnumerable<WcsTaskDto>> result = new();
            var data = await sender.Send(new GetWcsTaskQuery
            {
                SerialNumber = request
            });
            result.SetValue(data);
            return result;
        }).WithTags(AssemblyReference.WcsTask);
    }
}