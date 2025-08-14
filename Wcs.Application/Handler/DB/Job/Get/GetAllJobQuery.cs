using Common.Application.MediatR.Message;
using Wcs.Contracts.Respon.Job;

namespace Wcs.Application.Handler.DB.Job.Get;

public sealed record GetAllJobQuery : IQuery<IEnumerable<JobDto>>;