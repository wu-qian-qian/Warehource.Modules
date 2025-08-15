using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Wcs.Contracts.Respon.Job;

namespace Wcs.Application.Handler.DataBase.Job.SetStatus;

public class StatusJobCommand : ICommand<Result<JobDto>>
{
    public string Name { get; set; }

    public bool Status { get; set; }
}