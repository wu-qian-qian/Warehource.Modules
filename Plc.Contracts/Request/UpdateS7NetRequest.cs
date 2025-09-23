using Plc.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plc.Contracts.Request
{
    public class UpdateS7NetRequest
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