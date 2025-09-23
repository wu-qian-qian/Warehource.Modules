using Common.Application.MediatR.Behaviors;
using Common.Helper;
using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Plc.Application.Handler.DataBase.Insert;
using Plc.Contracts.Request;
using Plc.Contracts.Respon;
using System.Collections.Generic;

namespace Plc.Presentation.S7Plc;

internal class AddS7NetConfig : IEndpoint
{
    /// <summary>
    ///     nimiApi限制IformFile必须要防伪
    /// </summary>
    /// <param name="app"></param>
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("plc/add-allplc-config",
            async (IFormFile file, ISender sender) =>
            {
                var stream = file.OpenReadStream();
                var dicConfig =
                    ExcelHelper.CreateObjectFromList(stream, [typeof(S7NetRequest), typeof(S7NetEntityItemRequest)]);
                var s7NetRequests = dicConfig["S7NetRequest"].Cast<S7NetRequest>();
                var s7NetEntityITEMRequests = dicConfig["S7NetEntityItemRequest"].Cast<S7NetEntityItemRequest>();
                var group = s7NetEntityITEMRequests
                    .GroupBy(p => new { p.Ip, p.DeviceName, p.Name });
                Result<IEnumerable<S7NetDto>> result = default;
                if (group.Any(p => p.Count() > 1))
                {
                    result = new Result<IEnumerable<S7NetDto>>();
                    result.SetMessage("存在重复数据");
                }
                else
                {
                    result = await sender.Send(
                        new InsertS7NetConfigCommand
                        {
                            S7NetRequests = s7NetRequests,
                            S7NetEntityItemRequests = s7NetEntityITEMRequests
                        });
                }

                return result;
            }
        ).DisableAntiforgery().WithTags(AssemblyReference.Plc);
    }
}