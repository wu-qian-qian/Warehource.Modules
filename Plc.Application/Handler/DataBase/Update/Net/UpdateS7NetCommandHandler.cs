using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Plc.Application.Abstract;
using Plc.Domain.S7;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plc.Application.Handler.DataBase.Update.Net
{
    internal class UpdateS7NetCommandHandler(IS7NetManager _netManager, IUnitOfWork _unitOfWork)
        : ICommandHandler<UpdateS7NetCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(UpdateS7NetCommand request, CancellationToken cancellationToken)
        {
            Result<bool> result = new Result<bool>();
            var s7Net = await _netManager.GetNetWiteIdAsync(request.Id);
            s7Net.UpdateIp(request.Ip);
            foreach (var item in s7Net.S7EntityItems)
            {
                item.Ip = request.Ip;
            }

            s7Net.ReadHeart = request.ReadHeart;
            s7Net.WriteHeart = request.WriteHeart;
            s7Net.S7Type = request.S7Type.Value;
            s7Net.IsUse = request.IsUse.Value;
            s7Net.UpdatePort(request.Port.Value);
            s7Net.WriteTimeOut = request.WriteTimeOut.Value;
            s7Net.ReadTimeOut = request.ReadTimeOut.Value;
            s7Net.Solt = request.Solt.Value;
            s7Net.Rack = request.Rack.Value;
            _netManager.UpdateS7Net([s7Net]);
            result.SetValue(true);
            await _unitOfWork.SaveChangesAsync();
            return result;
        }
    }
}