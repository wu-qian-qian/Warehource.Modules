using AutoMapper;
using Common.Application.MediatR.Message.PageQuery;
using Common.Helper;
using Common.Shared;
using Wcs.Contracts.Respon.Region;
using Wcs.Domain.Region;

namespace Wcs.Application.Handler.DataBase.Region.Page;

internal class GetRegionPageCommandHandler(IRegionRepository _regionRepository, IMapper _mapper)
    : IPageHandler<GetRegionPageCommand, RegionDto>
{
    public Task<PageResult<RegionDto>> Handle(GetRegionPageCommand request, CancellationToken cancellationToken)
    {
        var query = _regionRepository.GetQuery()
            .WhereIf(request.Code != null, p => p.Code.Contains(request.Code));
        var count = query.Count();
        var data = query.ToPageBySortAsc(request.SkipCount, count, p => p.CreationTime).ToArray();
        var list = _mapper.Map<List<RegionDto>>(data);
        return Task.FromResult(new PageResult<RegionDto>(count, list));
    }
}