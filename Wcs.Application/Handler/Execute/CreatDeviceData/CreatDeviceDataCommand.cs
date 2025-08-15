using Common.Application.MediatR.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wcs.Device.Abstract;
using Wcs.Shared;

namespace Wcs.Application.Handler.Execute.CreatDeviceData
{
    public class CreatDeviceDataCommand : ICommand<IDevice[]>
    {
        public DeviceTypeEnum DeviceType { get; set; }
    }
}