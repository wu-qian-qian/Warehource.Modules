using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Wcs.Contracts.Respon.Region;

namespace Wcs.Application.Handler.DataBase.Region.AddOrUpdate;

public class AddOrUpdateRegionCommand : ICommand<Result<RegionDto>>
{
    public string Code { get; set; }

    public string? Description { get; set; }

    public Guid? Id { get; set; }


    //最大流量
    public int MaxNum { get; set; }
}