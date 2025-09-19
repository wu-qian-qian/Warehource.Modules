using Common.Presentation.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Wcs.Shared;

namespace Wcs.Presentation.GetEnum;

internal class GetJobTypes : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("getenum/get-jobtypes",
            (IServiceProvider service) =>
            {
                var jobTypes = service.GetKeyedService<Type[]>(Constant.JobKey);
                return jobTypes.Select(p => p.Name);
            }).WithTags(AssemblyReference.Enum);
    }
}