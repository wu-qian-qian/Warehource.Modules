using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.Handler.DB.ExecueNode.AddOrUpdate;
using Wcs.Contracts.Request.ExecuteNode;

namespace Wcs.Presentation.ExecuteNode;

public class InsertOrUpdate : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("executenode/add-or-update", [Authorize(Roles = "admin")]
            async (ExecuteNodeRequest request, ISender sender) =>
            {
                return await sender.Send(new AddOrUpdateExecuteNodeCommand
                {
                    Id = request.Id ?? Guid.NewGuid(),
                    PahtNodeGroup = request.PahtNodeGroup,
                    CurrentDeviceType = request.CurrentDeviceType,
                    TaskType = request.TaskType,
                    RegionCode = request.RegionCode,
                    Index = request.Index
                });
            }).WithTags(AssemblyReference.ExecuteNode);
    }
}