using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wcs.Application.Handler.Business.CreatDeviceData;
using Wcs.Device.Device.Stacker;
using Wcs.Device.Device.Tranship;
using Wcs.Shared;

namespace Wcs.Application.Abstract.Device
{
    public abstract class AbstractStackerTranshipInController : IStackerTranshipController
    {
        protected readonly IServiceScopeFactory _scopeFactory;

        protected AbstractStackerTranshipInController(IServiceScopeFactory serviceScopeFactory)
        {
            DeviceType = DeviceTypeEnum.StackerInTranShip;
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

        public virtual int[] GetReCommendTranship()
        {
            int tunnle = 0;
            var tempDevices = Devices.Where(p => p.Enable);
            return tempDevices.Select(p => p.Config.Tunnle).ToArray();
        }

        public virtual string TargetPostion(int tunnel)
        {
            return Devices.First(p => p.Config.Tunnle == tunnel).Config.PipelinCode;
        }
    }
}