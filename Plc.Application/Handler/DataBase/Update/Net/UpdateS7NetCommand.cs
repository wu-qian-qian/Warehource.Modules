using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Plc.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plc.Application.Handler.DataBase.Update.Net
{
    public class UpdateS7NetCommand : ICommand<Result<bool>>
    {
        public Guid Id { get; set; }

        public string? Ip { get; set; }
        public string? ReadHeart { get; set; }

        public string? WriteHeart { get; set; }

        public bool? IsUse { get; set; }

        public int? ReadTimeOut { get; set; }

        public int? WriteTimeOut { get; set; }

        public S7TypeEnum? S7Type { get; set; }

        public int? Port { get; set; }

        public short? Solt { get; set; }
        public short? Rack { get; set; }
    }
}