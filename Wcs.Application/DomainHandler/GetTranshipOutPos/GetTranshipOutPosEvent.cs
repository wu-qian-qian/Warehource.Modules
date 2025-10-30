using Common.Domain.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wcs.Domain.Task;

namespace Wcs.Application.DomainHandler.GetTranshipOutPos
{
    internal class GetTranshipOutPosEvent : IEvent<PutLocation>
    {
        public string Tunnle { get; set; }
    }
}