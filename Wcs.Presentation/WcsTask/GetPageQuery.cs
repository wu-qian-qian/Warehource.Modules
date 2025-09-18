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
using Wcs.Application.Handler.DataBase.WcsTask.Page;
using Wcs.Contracts.Request.WcsTask;

namespace Wcs.Presentation.WcsTask
{
    internal class GetPageQuery : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            //获取执行中的任务
            app.MapPost("wcstask/get-wcstask-pagequery", [Authorize]
             async (GetWcsTaskPageQueryRequest request, ISender sender) =>
            {
               return await sender.Send(new GetWcsTaskPageCommand
                {
                    SerialNumber=request.SerialNumber,
                    Container = request.Container,
                    CreatorSystemType = request.CreatorSystemType,
                    EndTime = request.EndTime,
                    StartTime = request.StartTime,
                    TaskCode = request.TaskCode,
                    TaskStatus = request.TaskStatus,
                    Total= request.Total,
                    PageIndex = request.PageIndex,
                    GetLocation = request.GetLocation,
                    PutLocation=request.PutLocation,
                    WcsTaskType=request.WcsTaskType
                });
            }).WithTags(AssemblyReference.WcsTask);
        }
    }
}
