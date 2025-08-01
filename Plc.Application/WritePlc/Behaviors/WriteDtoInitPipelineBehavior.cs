using MediatR;
using Plc.Contracts.Input;
using Plc.Domain.S7;

namespace Plc.Application.ReadPlc.Behaviors;

public class WriteDtoInitPipelineBehavior<TRequest, TResponse>(IS7NetManager netManager)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : WritePlcEventCommand
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var dbName = request.DBNameToDataValue.Keys.ToArray();
        var s7EntityItems =await netManager
            .GetDeviceNameWithDBNameAsync(request.DeviceName, dbName.ToList());
        var S7EntityItemGroup = s7EntityItems.GroupBy(p => p.Ip);
        var writeItems = new List<WriteBufferInput>();
        foreach (var groupItem in S7EntityItemGroup)
        {
            WriteBufferInput input = new WriteBufferInput();
            input.Ip = input.Ip;
            var itemInputs =new List<WriteBufferItemInput>();
            foreach (var item  in groupItem)
            {
                WriteBufferItemInput itemInput = new WriteBufferItemInput
                {
                    Value = request.DBNameToDataValue[item.Name],
                    ArratCount = item.ArrtypeLength,
                    DBBit = item.BitOffset,
                    DBAddress = item.DBAddress,
                    DBStart = item.DataOffset,
                    S7BlockType = item.S7BlockType,
                    S7DataType = item.S7DataType
                };
                itemInputs.Add(itemInput);
            }
            input.WriteBufferItemArray = itemInputs.ToArray();
            writeItems.Add(input);
        }
        request.writeBufferInputs = writeItems.ToArray();
      return  await next();
    }
}