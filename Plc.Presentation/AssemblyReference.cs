using System.Reflection;

namespace Plc.Presentation;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;


    public static readonly string Plc = "Plc";

    public static readonly string Enum = "Enum";
}