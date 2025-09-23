using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wcs.Application.Handler.DataBase.ExecueNode.Get;
using Wcs.Contracts.Request.ExecuteNode;

namespace Wcs.Presentation.ExecuteNode
{
    internal class GetExecuteNodeGroupCode : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("executenode/get-group-code", [Authorize] async (ISender sender) =>
            {
                var result = await sender.Send(new GetExecuteNodeQuery());
                var code = result?.Value.Select(p => { return $"{p.PahtNodeGroup}--{p.TaskType}"; });
                return code.Distinct();
            }).WithTags(AssemblyReference.ExecuteNode);
        }
    }
}