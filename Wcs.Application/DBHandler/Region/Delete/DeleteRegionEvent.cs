using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Wcs.Contracts.Respon.Region;

namespace Wcs.Application.DBHandler.Region.Delete;

public class DeleteRegionEvent : ICommand<Result<RegionDto>>
{
    public Guid? Id { get; set; }
}