using Common.Application.MediatR.Behaviors;
using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.DBHandler.WcsTask.Get;
using Wcs.Application.DBHandler.WcsTask.Insert;
using Wcs.Contracts.Request.WcsTask;
using Wcs.Contracts.Respon.WcsTask;

namespace Wcs.Presentation.WcsTask;

public class GetWcsTask:IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("wcstask/get-wcstask/{serialNumber}", [Authorize] async (
            int request,
            ISender sender) =>
        {
            Result<IEnumerable<WcsTaskDto>> reslut = new();
            var data= await sender.Send(new GetWcsTaskQuery
            {
                SerialNumber = request
            });
            reslut.SetValue(data);
            return reslut;
        }).WithTags(AssemblyReference.WcsTask);
    }
}