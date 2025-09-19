using Wcs.Domain.Task;

namespace Wcs.Application.Abstract;

public interface IAnalysisLocation
{
    public string[] Analysis(string location);

    public GetLocation AnalysisGetLocation(string location);

    public PutLocation AnalysisPutLocation(string location);

    public bool CanApplyGetLocation(GetLocation location);

    public bool CanApplyPutLocation(PutLocation location);
}