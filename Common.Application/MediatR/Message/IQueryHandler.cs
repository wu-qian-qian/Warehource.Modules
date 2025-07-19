using MediatR;

namespace Common.Application.MediatR.Message;

/// <summary>
///     业务逻辑
/// </summary>
/// <typeparam name="TQuery">查询实体</typeparam>
/// <typeparam name="TResponse">返回实体</typeparam>
public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>;