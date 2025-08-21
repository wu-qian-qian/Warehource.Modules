using Common.Application.MediatR.Behaviors;
using Common.Application.MediatR.Message;
using Wcs.Contracts.Business;

namespace Wcs.Application.Handler.Http.ApplyTunnle;

public class ApplyTunnleCommand : ICommand<Result<RecommendTunnle>>
{
    public string WcsTaskCode { get; set; }

    public IEnumerable<int> Tunnles { get; set; }
}