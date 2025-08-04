using MediatR;
using Plc.Application.PlcHandler.Write;
using Plc.Contracts.Input;
using Plc.Domain.S7;

namespace Plc.Application.Behaviors.Write;

public class WriteDtoInitPipelineBehavior<TRequest, TResponse>(IS7NetManager netManager)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : WritePlcEventCommand
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request.UseMemory)
        {
            var dbNames = request.DBNameToDataValue.Keys.ToArray();
            var key = request.DeviceName;
            if (!PlcReadWriteDtoHelper._WriteBufferInputs.Any(p => p._key == key))
            {
                var s7EntityItems = await netManager.GetNetWiteDeviceNameAsync(request.DeviceName);
                PlcReadWriteDtoHelper.UseMemoryInitWriteBufferInput(key, s7EntityItems.ToArray());
            }

            var memory =
                PlcReadWriteDtoHelper._WriteBufferInputs.Where(p => p._key == key && dbNames.Contains(p._dBName));
            var memoryGroup = memory.GroupBy(p => p._ipAddress);
            var writeItems = new WriteBufferInput[memoryGroup.Count()];
            var index = 0;
            foreach (var itemGroup in memoryGroup)
            {
                var input = new WriteBufferInput();
                input.Ip = itemGroup.Key;
                input.WriteBufferItemArray = itemGroup.Select(p => p._value).ToArray();
                writeItems[index] = input;
                index++;
            }

            request.writeBufferInputs = writeItems;
        }
        else
        {
            var dbName = request.DBNameToDataValue.Keys.ToArray();
            var s7EntityItems = await netManager
                .GetDeviceNameWithDBNameAsync(request.DeviceName, dbName.ToList());
            var S7EntityItemGroup = s7EntityItems.GroupBy(p => p.Ip);
            var writeItems = new WriteBufferInput[S7EntityItemGroup.Count()];
            var index = 0;
            foreach (var groupItem in S7EntityItemGroup)
            {
                var input = new WriteBufferInput();
                input.Ip = groupItem.Key;
                input.WriteBufferItemArray = PlcReadWriteDtoHelper.CreatWriteBufferInput(groupItem.ToArray()).ToArray();
                writeItems[index] = input;
                index++;
            }

            request.writeBufferInputs = writeItems;
        }

        return await next();
    }
}