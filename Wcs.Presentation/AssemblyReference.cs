using System.Reflection;

namespace Wcs.Presentation;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;

    public static readonly string Job = "Job";

    public static readonly string Region = "Region";

    public static readonly string WcsTask = "WcsTask";

    public static readonly string ExecuteNode = "ExecuteNode";

    public static readonly string Decive = "Decive";

    public static readonly string Enum = "Enum";
}