using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wcs.Application.Handler.Business.CreatDeviceData;
using Wcs.Device.Device.Tranship;
using Wcs.Shared;

namespace Wcs.Application.Abstract.Device
{
    public abstract class AbstractStackerTranshipOutController : IStackerTranshipController
    {
        protected readonly IServiceScopeFactory _scopeFactory;

        protected AbstractStackerTranshipOutController(IServiceScopeFactory serviceScopeFactory)
        {
            DeviceType = DeviceTypeEnum.StackerOutTranShip;
        }

        public AbstractStackerTranship[] Devices { get; private set; }

        public DeviceTypeEnum DeviceType { get; init; }

        public virtual async Task ExecuteAsync(CancellationToken token = default)
        {
            if (Devices == null || Devices.Length == 0)
            {
                using var scope = _scopeFactory.CreateScope();
                var sender = scope.ServiceProvider.GetService<ISender>();
                Devices = (AbstractStackerTranship[])await sender.Send(new CreatDeviceDataCommand
                {
                    DeviceType = DeviceType
                });
            }
        }
    }
}