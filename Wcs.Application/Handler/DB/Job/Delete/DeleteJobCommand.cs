using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Wcs.Contracts.Respon.Job;

namespace Wcs.Application.Handler.DB.Job.Delete;

public class DeleteJobCommand : ICommand<Result<JobDto>>
{
    public string Name { get; set; }
}