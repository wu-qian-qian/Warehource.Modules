using Common.Domain.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wcs.Domain.Task;

namespace Wcs.Application.DomainEvent.ApplyTunnle
{
    internal class ApplyTunnleEvent : IEvent<string>
    {
        public string RegoinCode { get; set; }
        public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;
    }
}