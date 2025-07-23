using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Common.Reflection;

internal class AssemblyEquality : EqualityComparer<Assembly>
{
    public override bool Equals(Assembly? x, Assembly? y)
    {
        if (x == null && y == null) return true;
        if (x == null || y == null) return false;
        return AssemblyName.ReferenceMatchesDefinition(x.GetName(), y.GetName());
    }

    public override int GetHashCode([DisallowNull] Assembly obj)
    {
        return obj.GetName().FullName.GetHashCode();
    }
}