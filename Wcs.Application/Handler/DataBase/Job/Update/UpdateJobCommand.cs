using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Wcs.Contracts.Respon.Job;

namespace Wcs.Application.Handler.DataBase.Job.Update;

public record UpdateJobCommand(string Name, bool? IsStart, int? Timer, int? TimerOut, string? Description)
    : ICommand<Result<JobDto>>;