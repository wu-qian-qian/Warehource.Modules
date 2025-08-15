using Common.Helper;
using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Plc.Application.Handler.DataBase.Insert;
using Plc.Contracts.Request;

namespace Plc.Presentation.S7Plc;

internal class AddS7NetConfig : IEndpoint
{
    /// <summary>
    ///     nimiApi限制IformFile必须要防伪
    /// </summary>
    /// <param name="app"></param>
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("plc/add-allplc-config", [Authorize(Roles = "admin")] async (
                IFormFile file, ISender sender) =>
            {
                var stream = file.OpenReadStream();
                var dicConfig =
                    ExcelHelper.CreateObjectFromList(stream, [typeof(S7NetRequest), typeof(S7NetEntityItemRequest)]);
                var s7NetRequests = dicConfig["S7NetRequest"].Cast<S7NetRequest>();
                var s7NetEntityITEMRequests = dicConfig["S7NetEntityItemRequest"].Cast<S7NetEntityItemRequest>();
                return await sender.Send(
                    new InsertS7NetConfigCommand
                    {
                        S7NetRequests = s7NetRequests,
                        S7NetEntityItemRequests = s7NetEntityITEMRequests
                    }
                );
            }
        ).DisableAntiforgery().WithTags(AssemblyReference.Plc);
    }
}