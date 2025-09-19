using Common.Application.MediatR.Message.PageQuery;
using Wcs.Contracts.Respon.Region;

namespace Wcs.Application.Handler.DataBase.Region.Page;

public class GetRegionPageCommand : PageQuery<RegionDto>
{
    public string? Code { get; set; }
}