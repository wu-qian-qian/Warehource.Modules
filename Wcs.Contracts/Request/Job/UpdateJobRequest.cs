namespace Wcs.Contracts.Request.Job;

public record UpdateJobRequest(string Name, bool? IsStart, int? Timer, int? TimerOut, string? Description);