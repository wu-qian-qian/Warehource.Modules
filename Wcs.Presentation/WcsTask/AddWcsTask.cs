using AutoMapper;
using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.Abstract;
using Wcs.Application.Handler.DataBase.WcsTask.AddOrUpdate;
using Wcs.Contracts.Request.WcsTask;

namespace Wcs.Presentation.WcsTask;

public class AddWcsTask : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("wcstask/add-or-update-wcstask", [Authorize]
            async (InsertWcsTaskRequest request, ISender sender, IMapper mapper, IAnalysisLocation locationService) =>
            {
                var getLocation = locationService.Analysis(request.GetLocation);
                var putLocation = locationService.Analysis(request.Putlocation);
                if (getLocation.Length > 4)
                {
                    request.GetTunnel = int.Parse(getLocation[0]);
                    request.GetRow = int.Parse(getLocation[1]);
                    request.GetColumn = int.Parse(getLocation[2]);
                    request.GetFloor = int.Parse(getLocation[3]);
                    request.GetDepth = int.Parse(getLocation[4]);
                }

                if (putLocation.Length > 4)
                {
                    request.PutTunnel = int.Parse(putLocation[0]);
                    request.PutRow = int.Parse(putLocation[1]);
                    request.PutColumn = int.Parse(putLocation[2]);
                    request.PutFloor = int.Parse(putLocation[3]);
                    request.PutDepth = int.Parse(putLocation[4]);
                }

                var command = mapper.Map<AddOrUpdateWcsTaskCommand>(request);
                return await sender.Send(command);
            }).WithTags(AssemblyReference.WcsTask);
    }
}