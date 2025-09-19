using Common.Application.MediatR.Message.PageQuery;
using Wcs.Contracts.Respon.Job;

namespace Wcs.Application.Handler.DataBase.Job.Page;

public class GetJobPageCommand : PageQuery<JobDto>
{
    public string? Name { get; set; }

    public string? JobType { get; set; }
}