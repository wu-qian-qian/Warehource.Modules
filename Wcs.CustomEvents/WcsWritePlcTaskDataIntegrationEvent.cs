namespace Wcs.CustomEvents;

public record WcsWritePlcTaskDataIntegrationEvent(string DeviceName, string CacheKey, bool IsSucess);