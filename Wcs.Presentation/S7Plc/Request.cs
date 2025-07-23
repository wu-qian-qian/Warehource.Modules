using Wcs.Shared;

namespace Wcs.Presentation.S7Plc;

internal record S7NetRequest(Guid Id,string Ip, S7TypeEnum S7Type);


internal record S7NetEntityItemRequest(Guid Id,string Name);