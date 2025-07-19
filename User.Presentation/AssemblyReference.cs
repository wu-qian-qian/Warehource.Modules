using System.Reflection;

namespace User.Presentation;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;

    public static readonly string User = "User";
}