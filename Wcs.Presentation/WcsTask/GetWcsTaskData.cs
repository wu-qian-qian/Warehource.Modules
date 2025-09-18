using Common.Application.MediatR.Behaviors;
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
using Wcs.Application.Handler.DataBase.WcsTask.Get;
using Wcs.Application.Handler.DataBase.WcsTask.GetData;
using Wcs.Contracts.Request.WcsTask;
using Wcs.Contracts.Respon.WcsTask;

namespace Wcs.Presentation.WcsTask
{
    internal class GetWcsTaskData : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("wcstask/get-TaskData/{time}",  async (DateTime time,ISender sender) =>
            {
               var dateTime = time.Date;
              return  await sender.Send(new GetDataCommand
                {
                    StartTime = dateTime,
                    EndTime=dateTime.AddDays(1)
                });

            }).WithTags(AssemblyReference.WcsTask);
        }
    }
}
