using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wcs.Shared;

namespace Wcs.Domain.Event
{
    public class Event
    {
        public Guid Id { get; set; }

        public EventStatus EventStatus { get; set; }

        public string EventName { get; set; }

        public string Content { get; set; }

        public string? Errored { get; set; }

        public DateTime Created { get; set; }
    }
}