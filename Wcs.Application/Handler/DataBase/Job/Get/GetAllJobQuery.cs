using Common.Application.MediatR.Message;
using Wcs.Contracts.Respon.Job;

namespace Wcs.Application.Handler.DataBase.Job.Get;

public sealed record GetAllJobQuery : IQuery<IEnumerable<JobDto>>;