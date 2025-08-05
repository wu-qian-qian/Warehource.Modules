using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Wcs.Contracts.Respon.Job;

namespace Wcs.Application.DBHandler.Job.SetStatus;

public class StatusJobEvent : ICommand<Result<JobDto>>
{
    public string Name { get; set; }

    public bool Status { get; set; }
}