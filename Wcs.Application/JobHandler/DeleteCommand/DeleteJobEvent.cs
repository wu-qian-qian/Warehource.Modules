using Common.Application.MediatR.Message;
using Wcs.Contracts.Job;

namespace Wcs.Application.JobHandler.DeleteCommand;

public class DeleteJobEvent : ICommand<JobDto>
{
    public string Name { get; set; }
}