using Common.Application.MediatR.Message;
using Wcs.Contracts.Job;

namespace Wcs.Application.JobHandler.GetQuery;

public sealed record GetJobQuery(Guid Id) : IQuery<JobDto?>;

public sealed record GetAllJobQuery : IQuery<IEnumerable<JobDto>?>;