using Common.Application.MediatR.Message.PageQuery;
using Plc.Contracts.Respon;
using Plc.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plc.Application.Handler.DataBase.Page.S7Net
{
    public class GetS7NetPageCommand : PageQuery<S7NetDto>
    {
        public string? Ip { get; set; }

        public S7TypeEnum? S7Type { get; set; }
    }
}