using Common.Application.MediatR.Message;
using Wcs.Contracts.Job;

namespace Wcs.Application.JobHandler.SetStatusCommand;

public class StatusJobEvent : ICommand<JobDto>
{
    public string Name { get; set; }

    public bool Status { get; set; }
}