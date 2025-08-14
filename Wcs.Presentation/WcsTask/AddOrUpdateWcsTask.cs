using AutoMapper;
using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Wcs.Application.DBHandler.WcsTask.AddOrUpdate;
using Wcs.Contracts.Request.WcsTask;

namespace Wcs.Presentation.WcsTask;

public class AddOrUpdateWcsTask : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("wcstask/add-or-update-wcstask", [Authorize]
            async (InsertWcsTaskRequest request, ISender sender, IMapper mapper) =>
            {
                var command = mapper.Map<AddOrUpdateWcsTaskEvent>(request);
                return await sender.Send(command);
            }).WithTags(AssemblyReference.WcsTask);
    }
}