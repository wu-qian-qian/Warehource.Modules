using Common.Application.MediatR.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wcs.Contracts.Respon.WcsTask;


namespace Wcs.Application.Handler.DataBase.WcsTask.GetData
{
    public class GetDataCommand:ICommand<IEnumerable<GetWcsDataDto>>
    {
        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset EndTime { get; set; }
    }
}
