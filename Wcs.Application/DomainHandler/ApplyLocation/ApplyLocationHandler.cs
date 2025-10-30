using Common.Application.Event;
using Common.Domain.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wcs.Application.Abstract;
using Wcs.Application.DeviceController.Tranship;
using Wcs.Domain.Task;

namespace Wcs.Application.DomainHandler.ApplyLocation
{
    internal class ApplyLocationHandler(IAnalysisLocation _analysisLocation)
        : IEventHandler<ApplyLocationEvent, PutLocation>
    {
        internal static SemaphoreSlim _slim = new(1, 1);

        public ValueTask<PutLocation> Handle(ApplyLocationEvent domainEvent,
            CancellationToken cancellationToken = default)
        {
            _slim.Wait(cancellationToken);
            try
            {
                var tunnle = domainEvent.Tunnle;
                //TODO 获取库位
                PutLocation? putLocation = _analysisLocation.AnalysisPutLocation("1_1_1_1_1");
                return ValueTask.FromResult(putLocation);
            }
            finally
            {
                _slim.Release();
            }
        }
    }
}