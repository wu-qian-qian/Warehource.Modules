namespace Wcs.Domain.S7;

public interface IS7NetManager
{
    public Task<List<S7NetConfig>> GetAllNet();
}