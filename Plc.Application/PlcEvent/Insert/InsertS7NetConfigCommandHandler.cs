﻿using Common.Application.MediatR.Message;
using Plc.Application.Abstract;
using Plc.Contracts.Respon;
using Plc.Domain.S7;

namespace Plc.Application.PlcEvent.Insert;

/// <summary>
///     excel导入只能一块插入
/// </summary>
public class InsertS7NetConfigCommandHandler(IS7NetManager netManager, IUnitOfWork unitOfWork)
    : ICommandHandler<InsertS7NetConfigCommand, IEnumerable<S7NetDto>>
{
    public async Task<IEnumerable<S7NetDto>> Handle(InsertS7NetConfigCommand request,
        CancellationToken cancellationToken)
    {
        var s7NetConfigs = new List<S7NetConfig>();
        foreach (var netDto in request.S7NetRequests)
        {
            var config = new S7NetConfig
            {
                Ip = netDto.Ip,
                Port = netDto.Port,
                S7Type = netDto.S7Type,
                Solt = netDto.Solt,
                Rack = netDto.Rack,
                ReadTimeOut = netDto.ReadTimeOut,
                WriteTimeOut = netDto.WriteTimeOut,
                IsUse=false,
            };
            var s7entityItems = request.S7NetEntityItemRequests
                .Where(p => p.Ip == netDto.Ip);
            var s7EntityItemsList = s7entityItems
                .Select(p => new S7EntityItem
                {
                    Name = p.Name,
                    Ip = netDto.Ip,
                    DBAddress = p.DBAddress,
                    S7DataType = p.S7DataType,
                    DataOffset = p.DataOffset,
                    BitOffset = p.BitOffset,
                    S7BlockType = p.S7BlockType,
                    Description = p.Description,
                    Index = p.Index,
                    ArrtypeLength = p.ArrtypeLength,
                    DeviceName = p.DeviceName
                });
            config.S7EntityItems = s7EntityItemsList.ToList();
            s7NetConfigs.Add(config);
        }

        await netManager.InsertS7NetAsync(s7NetConfigs);
        await unitOfWork.SaveChangesAsync();
        return s7NetConfigs.Select(p =>
        {
            return new S7NetDto
            {
                Id = p.Id,
                Ip = p.Ip,
                Port = p.Port,
                S7Type = p.S7Type,
                Solt = p.Solt,
                Rack = p.Rack,
                ReadTimeOut = p.ReadTimeOut,
                WriteTimeOut = p.WriteTimeOut
            };
        });
    }
}