using Common.Application.MediatR.Message;
using Wcs.Contracts.Respon.Job;

namespace Wcs.Application.Job.SetStatus;

public class StatusJobEvent : ICommand<JobDto>
{
    public string Name { get; set; }

    public bool Status { get; set; }
}