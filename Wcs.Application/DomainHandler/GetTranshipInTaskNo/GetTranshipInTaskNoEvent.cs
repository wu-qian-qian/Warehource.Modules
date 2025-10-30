using Common.Domain.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wcs.Application.DomainHandler.GetTranshipInTaskNo
{
    internal class GetTranshipInTaskNoEvent : IEvent<string>
    {
        public string Tunnle { get; set; }
    }
}