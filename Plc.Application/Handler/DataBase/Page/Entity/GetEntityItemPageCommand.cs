using Common.Application.MediatR.Message.PageQuery;
using Plc.Contracts.Respon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plc.Application.Handler.DataBase.Page.Entity
{
    public class GetEntityItemPageCommand : PageQuery<S7EntityItemDto>
    {
        public string? Ip { get; set; }

        public string? DeviceName { get; set; }

        public string? Name { get; set; }
    }
}