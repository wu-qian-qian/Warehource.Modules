using Common.Application.MediatR.Message;
using Wcs.Contracts.Respon.Job;

namespace Wcs.Application.DBHandler.Job.Get;

public sealed record GetJobQuery(Guid Id) : IQuery<JobDto?>;