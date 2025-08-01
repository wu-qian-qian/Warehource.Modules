﻿using Common.Domain.Event;

namespace Common.Application.Event;

public interface ILocalEventBus : IEventBus
{
    Task PublishAsync<T>(T integrationEvent, CancellationToken cancellationToken = default)
        where T : IEventDomain;
}