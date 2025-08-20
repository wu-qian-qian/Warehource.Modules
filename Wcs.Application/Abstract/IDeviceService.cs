using Wcs.Contracts.Business;
using Wcs.Shared;

namespace Wcs.Application.Abstract;

public interface IDeviceService
{
    /// <summary>
    ///     获取推荐巷道
    /// </summary>
    /// <returns></returns>
    public Task<RecommendTunnle> GerRecommendTunnleAsync(DeviceTypeEnum deviceType);
}