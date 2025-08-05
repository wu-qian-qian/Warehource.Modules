using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wcs.Shared;

namespace Wcs.Application.DBHandler.WcsTask.UpdateExecute
{
    public class UpdateWcsTaskExecuteStepEvent : ICommand<Result<string>>
    {
        public int SerialNumber { get; set; }

        public string DeviceName { get; set; }
    }
}
