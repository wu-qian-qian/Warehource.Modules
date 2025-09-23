using Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Plc.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plc.Presentation.GetEnum
{
    internal class GetDataTypeEnum : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("plc/get-s7datatype-Enum",
                    [Authorize](ISender sender) =>
                    {
                        var res = typeof(S7DataTypeEnum).GetEnumNames();
                        return res;
                    })
                .WithTags(AssemblyReference.Enum);
        }
    }
}