using Common.Application.MediatR.Message;
using Wcs.Contracts.Respon.Job;

namespace Wcs.Application.DBHandler.Job.Insert;

public class AddJobEvent : ICommand<JobDto>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string JobType { get; set; }
    public int TimeOut { get; set; }
    public int Timer { get; set; }
    public bool IsStart { get; set; }
}