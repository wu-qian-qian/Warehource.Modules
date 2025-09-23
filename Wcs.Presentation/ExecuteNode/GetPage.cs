using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.Handler.DataBase.ExecueNode.GetPage;
using Wcs.Contracts.Request.ExecuteNode;

namespace Wcs.Presentation.ExecuteNode;

internal class GetPage : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("executenode/get-page", [Authorize] async (GetExecuteNodePathPage request, ISender sender) =>
        {
            return await sender.Send(new GetExecuteNodePageCommand
            {
                PahtNodeGroup = request.PahtNodeGroup,
                TaskType = request.TaskType,
                Total = request.Total,
                PageIndex = request.PageIndex
            });
        }).WithTags(AssemblyReference.ExecuteNode);
    }
}