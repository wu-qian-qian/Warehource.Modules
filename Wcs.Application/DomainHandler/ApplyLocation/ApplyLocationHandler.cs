using Common.Application.Event;
using Wcs.Application.Abstract;
using Wcs.Domain.Task;

namespace Wcs.Application.DomainHandler.ApplyLocation;

internal class ApplyLocationHandler(IAnalysisLocation _analysisLocation)
    : IEventHandler<ApplyLocationEvent, PutLocation>
{
    internal static SemaphoreSlim _slim = new(1, 1);

    public Task<PutLocation> Handle(ApplyLocationEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        _slim.Wait(cancellationToken);
        try
        {
            var tunnle = domainEvent.Tunnle;
            //TODO 获取库位
            var putLocation = _analysisLocation.AnalysisPutLocation("1_1_1_1_1");
            return Task.FromResult(putLocation);
        }
        finally
        {
            _slim.Release();
        }
    }
}