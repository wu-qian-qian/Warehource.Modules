using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Wcs.Application.Abstract;
using Wcs.Domain.ExecuteNode;

namespace Wcs.Application.Handler.Business.GetStackerStation;

internal class GetStackerStationCommandHandler(
    IExecuteNodeRepository _executeNodeRepository,
    IDeviceService _deviceService,
    IAnalysisLocation analysisLocation) : ICommandHandler<GetStackerStaionCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(GetStackerStaionCommand request, CancellationToken cancellationToken)
    {
        var result = new Result<bool>();
        var nodeTypes = _executeNodeRepository.GetQuerys()
            .Where(p => p.PahtNodeGroup == request.WcsTask.TaskExecuteStep.PathNodeGroup).ToArray();
        var currentNodeIndex = nodeTypes
            .FirstOrDefault(p => p.TaskType == request.WcsTask.TaskType
                                 && p.CurrentDeviceType == request.WcsTask.TaskExecuteStep.DeviceType)?.Index;
        if (currentNodeIndex.HasValue)
        {
            var nextNode = nodeTypes.FirstOrDefault(p => p.Index == currentNodeIndex.Value + 1);
            if (nextNode != null)
            {
                if (analysisLocation.CanApplyGetLocation(request.WcsTask.GetLocation))
                {
                    var location = await _deviceService
                        .GetTranshipPositionAsync(nextNode.CurrentDeviceType, request.WcsTask.GetLocation.GetTunnel,
                            request.Region);
                    request.WcsTask.GetLocation = analysisLocation.AnalysisGetLocation(location);
                }

                if (analysisLocation.CanApplyPutLocation(request.WcsTask.PutLocation))
                {
                    var location = await _deviceService
                        .GetTranshipPositionAsync(nextNode.CurrentDeviceType, request.WcsTask.PutLocation.PutTunnel,
                            request.Region);
                    request.WcsTask.PutLocation = analysisLocation.AnalysisPutLocation(location);
                }

                result.SetValue(true);
            }
        }

        return result;
    }
}