using Common.Shared;
using MediatR;

namespace Common.Application.MediatR.Message.PageQuery;

public interface IPageHandler<in TQuery, TResponse> : IRequestHandler<TQuery, PageResult<TResponse>>
    where TQuery : PageQuery<TResponse>;