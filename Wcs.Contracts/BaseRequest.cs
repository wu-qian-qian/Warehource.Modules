namespace Wcs.Contracts;

public class BaseRequest
{
    public Guid Id { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public int PageIndex { get; set; }

    public int PageCount { get; set; }
}