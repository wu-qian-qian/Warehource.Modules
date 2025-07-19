using MediatR;

namespace Common.Application.MediatR.Message;

public interface ICommand<TResult> : IRequest<TResult>, IBaseCommand;

public interface ICommand : IRequest, IBaseCommand;

public interface IBaseCommand;