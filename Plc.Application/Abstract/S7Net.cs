using Common.Application.Net;
using Plc.Contracts.Input;

namespace Plc.Application.Abstract;

public abstract class S7Net : INet
{
    public S7.Net.Plc _plc { get; protected set; }

    public abstract void Connect();

    public abstract void ReConnect();
    public abstract void Close();

    public virtual void UsePlc(bool isUse)
    {
        if (isUse)
        {
            if (_plc.IsConnected == false)
                Connect();
        }
        else
        {
            Close();
        }
    }

    /// <summary>
    ///     读取数据以byte数组返回
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public abstract Task<byte[]> ReadAsync(ReadBufferInput input);

    /// <summary>
    ///     写入数据
    /// </summary>
    /// <param name="bulkItem"></param>
    /// <returns></returns>
    public abstract Task WriteAsync(WriteBufferItemInput[] bulkItem);

    /// <summary>
    ///     读取数据以对应的类型返回
    /// </summary>
    /// <param name="input"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public abstract Task<string> ReadTResultAsync(ReadBufferInput input);


    /// <summary>
    ///     检查写入数据写入后在检查数据一致
    /// </summary>
    /// <param name="bulkItems"></param>
    /// <returns></returns>
    public abstract Task<bool> CheckWriteToBytesAsync(WriteBufferItemInput[] bulkItems);
}