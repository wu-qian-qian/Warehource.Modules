namespace Common.Application.Net;

public interface INet
{
    public void Connect();

    public void ReConnect();

    public void Close();
}