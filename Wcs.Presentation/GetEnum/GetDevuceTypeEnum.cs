using Common.Presentation.Endpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Shared;

namespace Wcs.Presentation.GetEnum;

internal class GetDevuceTypeEnum : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("getenum/get-devucetype", [Authorize]() =>
        {
            var res = typeof(DeviceTypeEnum).GetEnumNames();
            return res;
        }).WithTags(AssemblyReference.Enum);
    }
}