using Common.Application.MediatR.Message;
using Wcs.Device.Abstract;
using Wcs.Shared;

namespace Wcs.Application.Handler.Business.ReadPlcBlock;

public class GetPlcDBQuery : IQuery<BaseDBEntity>
{
    public string DeviceName { get; set; }

    public DeviceTypeEnum DeviceType { get; set; }

    public string Key { get; set; }

    /// <summary>
    ///     可重复利用实例，这样可以有效的减少对象的生成
    /// </summary>
    public BaseDBEntity DBEntity { get; set; }

    public bool UseMemory { get; set; } = true;
}