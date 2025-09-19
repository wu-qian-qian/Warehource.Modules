using Common.Presentation.Endpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Shared;

namespace Wcs.Presentation.GetEnum;

internal class GetWcsTaskStatusEnum : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("getenum/get-wcstaskstatus", [Authorize]() =>
        {
            var res = typeof(WcsTaskStatusEnum).GetEnumNames();
            return res;
        }).WithTags(AssemblyReference.Enum);
    }
}