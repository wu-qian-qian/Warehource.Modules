using Common.Application.MediatR.Message;
using Plc.Contracts.Respon;

namespace Plc.Application.S7Plc.Insert;

public class InsertS7NetConfigCommand : ICommand<S7NetDto>
{
    public Stream Stream { get; set; }
}