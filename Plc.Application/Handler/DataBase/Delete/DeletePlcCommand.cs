using Common.Application.MediatR.Message;

namespace Plc.Application.Handler.DataBase.Delete;

public class DeletePlcCommand : ICommand
{
    public Guid Id { get; set; }
}