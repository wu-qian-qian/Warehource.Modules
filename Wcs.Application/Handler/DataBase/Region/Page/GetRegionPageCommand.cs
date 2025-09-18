using Common.Application.MediatR.Message.PageQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wcs.Contracts.Respon.Region;

namespace Wcs.Application.Handler.DataBase.Region.Page
{
    public class GetRegionPageCommand:PageQuery<RegionDto>
    {
        public string? Code { get; set; }
    }
}
