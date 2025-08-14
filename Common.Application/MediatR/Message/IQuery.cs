using MediatR;

namespace Common.Application.MediatR.Message;

/// <summary>
///     查询实体
/// </summary>
/// <typeparam name="TResponse">返回实体</typeparam>
public interface IQuery<TResponse> : IRequest<TResponse>, IBaseCommand;