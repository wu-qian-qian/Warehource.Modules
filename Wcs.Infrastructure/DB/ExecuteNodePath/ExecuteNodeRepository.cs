using Common.Infrastructure.EF;
using Wcs.Domain.ExecuteNode;
using Wcs.Infrastructure.Database;

namespace Wcs.Infrastructure.DB.ExecuteNodePath;

public class ExecuteNodeRepository(WCSDBContext _db) : IExecuteNodeRepository
{
    public void Insert(IEnumerable<Domain.ExecuteNode.ExecuteNodePath> nodes)
    {
        _db.ExecuteNodes.AddRange(nodes);
    }

    public Domain.ExecuteNode.ExecuteNodePath Get(Guid id)
    {
        return _db.ExecuteNodes.FirstOrDefault(p => p.Id == id);
    }

    public Domain.ExecuteNode.ExecuteNodePath Get(string pahtNodeGroup)
    {
        return _db.ExecuteNodes.FirstOrDefault(p => p.PahtNodeGroup == pahtNodeGroup);
    }


    public IQueryable<Domain.ExecuteNode.ExecuteNodePath> GetQuerys()
    {
        return _db.Query<Domain.ExecuteNode.ExecuteNodePath>();
    }

    public void Update(Domain.ExecuteNode.ExecuteNodePath nodes)
    {
        _db.ExecuteNodes.Update(nodes);
    }
}