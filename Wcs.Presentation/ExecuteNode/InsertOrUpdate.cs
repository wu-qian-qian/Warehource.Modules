using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.DBHandler.ExecueNode.AddOrUpdate;
using Wcs.Application.DBHandler.ExecueNode.Get;
using Wcs.Contracts.Request.ExecuteNode;

namespace Wcs.Presentation.ExecuteNode;

public class InsertOrUpdate : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("executenode/add-or-update", [Authorize(Roles = "admin")] async (
            ExecuteNodeRequest request,
            ISender sender) =>
        {
            return await sender.Send(new AddOrUpdateExecuteNodeEvent
            {
                Id = request.Id??Guid.NewGuid(),
                PahtNodeGroup = request.PahtNodeGroup,
                CurrentDeviceName = request.CurrentDeviceName,
                CurrentDeviceType = request.CurrentDeviceType,
                RegionCode = request.RegionCode,
                TaskType = request.TaskType,
                NextDeviceName=request.NextDeviceName
            });
        }).WithTags(AssemblyReference.ExecuteNode);
    }
}