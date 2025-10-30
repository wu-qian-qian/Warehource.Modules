using Common.Domain.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wcs.Application.DomainEvent.WritePlcData
{
    public class WritePlcDataEvent : IEvent
    {
        public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;

        public string CacheKey { get; set; }

        public bool IsSuccess { get; set; }
    }
}