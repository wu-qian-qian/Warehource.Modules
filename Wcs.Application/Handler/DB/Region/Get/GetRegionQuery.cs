using Common.Application.MediatR.Message;
using Wcs.Contracts.Respon.Region;

namespace Wcs.Application.Handler.DB.Region.Get;

public class GetRegionQuery : IQuery<IEnumerable<RegionDto>?>;