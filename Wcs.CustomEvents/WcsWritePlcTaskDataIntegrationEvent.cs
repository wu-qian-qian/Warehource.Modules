using Common.Domain.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wcs.CustomEvents
{
    public record WcsWritePlcTaskDataIntegrationEvent(string Key,bool IsSucess);
}
