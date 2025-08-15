using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.Handler.DataBase.ExecueNode.Get;
using Wcs.Contracts.Request.ExecuteNode;

namespace Wcs.Presentation.ExecuteNode;

public class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("executenode/gets", [Authorize] async (ExecuteNodeRequest request, ISender sender) =>
        {
            return await sender.Send(new GetExecuteNodeQuery
            {
                PahtNodeGroup = request.PahtNodeGroup,
                CurrentDeviceType = request.CurrentDeviceType,
                TaskType = request.TaskType
            });
        }).WithTags(AssemblyReference.ExecuteNode);
    }
}