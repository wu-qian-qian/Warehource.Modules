using AutoMapper;
using Common.Application.MediatR.Message;
using Microsoft.Extensions.Options;
using Wcs.Contracts.Options;
using Wcs.Contracts.Respon.Region;
using Wcs.Domain.Region;
using static Microsoft.IO.RecyclableMemoryStreamManager;

namespace Wcs.Application.Handler.DataBase.Region.Get;

public class GetRegionQueryHandler(
    IRegionRepository _regionRepository,
    IMapper _mapper,
    IOptions<StackerMapOptions> options)
    : IQueryHandler<GetRegionQuery, IEnumerable<RegionDto>>
{
    public Task<IEnumerable<RegionDto>> Handle(GetRegionQuery request, CancellationToken cancellationToken)
    {
        var res = options.Value;
        var regions = _regionRepository.GetAllRegions();
        return Task.FromResult(_mapper.Map<IEnumerable<RegionDto>>(regions));
    }
}