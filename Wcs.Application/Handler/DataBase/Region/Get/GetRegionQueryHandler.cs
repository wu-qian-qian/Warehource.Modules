using AutoMapper;
using Common.Application.MediatR.Message;
using Wcs.Contracts.Respon.Region;
using Wcs.Domain.Region;

namespace Wcs.Application.Handler.DataBase.Region.Get;

public class GetRegionQueryHandler(IRegionRepository _regionRepository, IMapper _mapper)
    : IQueryHandler<GetRegionQuery, IEnumerable<RegionDto>>
{
    public Task<IEnumerable<RegionDto>> Handle(GetRegionQuery request, CancellationToken cancellationToken)
    {
        var regions = _regionRepository.GetAllRegions();
        return Task.FromResult(_mapper.Map<IEnumerable<RegionDto>>(regions));
    }
}