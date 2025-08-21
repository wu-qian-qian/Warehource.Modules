using Wcs.Contracts.Business;
using Wcs.Shared;

namespace Wcs.Application.Abstract;

public interface IDeviceService
{
    /// <summary>
    ///     获取推荐巷道
    /// </summary>
    /// <returns></returns>
    Task<int[]?> GetCanExecuteTunnleAsync(DeviceTypeEnum deviceType);

    Task<RecommendTunnle> GetRecommendTunnleAsync(DeviceTypeEnum deviceType, int tullne);
    Task<string> GetTargetPipelinAsync(DeviceTypeEnum deviceType, string deviceName);
}