namespace Wcs.Domain.ExecuteNode;

public interface IExecuteNodeRepository
{
    public void Insert(IEnumerable<ExecuteNodePath> nodes);

    public ExecuteNodePath Get(Guid id);

    public ExecuteNodePath Get(string pahtNodeGroup);

    /// <summary>
    /// 自带include
    /// </summary>
    /// <returns></returns>
    public IQueryable<ExecuteNodePath> GetQuerys();

    public void Update(ExecuteNodePath nodes);
}