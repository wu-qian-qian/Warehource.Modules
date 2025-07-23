using System.Reflection;

namespace Wcs.Presentation;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;

    public static readonly string Job = "Job";
}