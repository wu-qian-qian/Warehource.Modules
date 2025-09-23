using Common.Application.Log;
using Common.Application.MediatR.Message;
using Plc.Application.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plc.Application.Handler.ReadWrite.ReLoad
{
    internal class ReloadCommandHandler : ICommandHandler<ReloadCommand>
    {
        public Task Handle(ReloadCommand request, CancellationToken cancellationToken)
        {
            var ex = PlcReadWriteDtoHelper.ReloadRead();
            if (ex == null)
            {
                ex = PlcReadWriteDtoHelper.ReloadWrite();
            }
            else
            {
                Serilog.Log.Logger.ForCategory(Common.Shared.LogCategory.Net).Information("PLC缓存重新加载");
            }

            return Task.CompletedTask;
        }
    }
}