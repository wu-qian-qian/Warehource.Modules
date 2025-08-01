using Common.Application.MediatR.Message;
using Wcs.Contracts.Respon.Job;

namespace Wcs.Application.DBHandler.Job.Delete;

public class DeleteJobEvent : ICommand<JobDto>
{
    public string Name { get; set; }
}