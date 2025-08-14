using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Wcs.Contracts.Respon.Region;

namespace Wcs.Application.Handler.DB.Region.Delete;

public class DeleteRegionCommand : ICommand<Result<RegionDto>>
{
    public Guid? Id { get; set; }
}