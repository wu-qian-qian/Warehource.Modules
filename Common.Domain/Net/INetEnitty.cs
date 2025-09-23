using Common.Domain.EF;

namespace Common.Domain.Net;

/// <summary>
///     网络基础类
/// </summary>
public abstract class INetEnitty : IEntity
{
    protected INetEnitty(Guid id) : base(id)
    {
    }

    public string Ip { get; protected set; }

    public int Port { get; protected set; }
}